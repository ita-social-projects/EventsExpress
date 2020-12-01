using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Db.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IPhotoService _photoService;
        private readonly IMediator _mediator;
        private readonly ICacheHelper _cacheHelper;
        private readonly IEmailService _emailService;

        public UserService(
            AppDbContext context,
            IMapper mapper,
            IPhotoService photoSrv,
            IMediator mediator,
            ICacheHelper cacheHelper,
            IEmailService emailService)
            : base(context, mapper)
        {
            _photoService = photoSrv;
            _mediator = mediator;
            _cacheHelper = cacheHelper;
            _emailService = emailService;
        }

        public async Task<OperationResult> Create(UserDTO userDto)
        {
            if (_context.Users.Any(u => u.Email == userDto.Email))
            {
                return new OperationResult(false, "Email already exists in database", "Email");
            }

            var user = _mapper.Map<User>(userDto);

            user.Role = _context.Roles.FirstOrDefault(r => r.Name == "User");

            var result = Insert(user);
            if (result.Email != user.Email || result.Id == Guid.Empty)
            {
                return new OperationResult(false, "Registration failed", string.Empty);
            }

            await _context.SaveChangesAsync();
            userDto.Id = result.Id;
            if (!userDto.EmailConfirmed)
            {
                await _mediator.Publish(new RegisterVerificationMessage(userDto));
            }

            return new OperationResult(true, "Registration success", string.Empty);
        }

        public async Task<OperationResult> ConfirmEmail(CacheDTO cacheDto)
        {
            var user = _context.Users.Find(cacheDto.UserId);
            if (user == null)
            {
                return new OperationResult(false, "Invalid user Id", "userId");
            }

            if (string.IsNullOrEmpty(cacheDto.Token))
            {
                return new OperationResult(false, "Token is null or empty", "verification token");
            }

            if (cacheDto.Token != _cacheHelper.GetValue(cacheDto.UserId).Token)
            {
                return new OperationResult(false, "Validation failed", string.Empty);
            }

            user.EmailConfirmed = true;
            await _context.SaveChangesAsync();
            _cacheHelper.Delete(cacheDto.UserId);
            return new OperationResult(true, "Verify succeeded", string.Empty);
        }

        public async Task<OperationResult> PasswordRecover(UserDTO userDto)
        {
            var user = _context.Users.Find(userDto.Id);
            if (user == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            var newPassword = Guid.NewGuid().ToString();
            user.PasswordHash = PasswordHasher.GenerateHash(newPassword);

            try
            {
                await _context.SaveChangesAsync();
                await _emailService.SendEmailAsync(new EmailDTO
                {
                    Subject = "EventsExpress password recovery",
                    RecepientEmail = user.Email,
                    MessageText = $"Hello, {user.Email}.\nYour new Password is: {newPassword}",
                });
                return new OperationResult(true, "Password Changed", string.Empty);
            }
            catch (Exception)
            {
                return new OperationResult(false, "Something is wrong", string.Empty);
            }
        }

        public async Task<OperationResult> Update(UserDTO userDTO)
        {
            if (string.IsNullOrEmpty(userDTO.Email))
            {
                return new OperationResult(false, "EMAIL cannot be empty", "Email");
            }

            if (!_context.Users.Any(u => u.Id == userDTO.Id))
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            var result = _mapper.Map<UserDTO, User>(userDTO);
            try
            {
                Update(result);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new OperationResult(false, $"{e.Message}", string.Empty);
            }

            return new OperationResult(true);
        }

        public UserDTO GetById(Guid id)
        {
            var user = _mapper.Map<UserDTO>(
                _context.Users
                .Include(u => u.Photo)
                .Include(u => u.Events)
                .Include(u => u.Role)
                .Include(u => u.Categories)
                    .ThenInclude(c => c.Category)
                .FirstOrDefault(x => x.Id == id));

            user.Rating = GetRating(user.Id);
            return user;
        }

        public UserDTO GetByEmail(string email)
        {
            var user = _mapper.Map<UserDTO>(
                 _context.Users
                .Include(u => u.Photo)
                .Include(u => u.Events)
                .Include(u => u.Role)
                .Include(u => u.Categories)
                    .ThenInclude(c => c.Category)
                .AsNoTracking()
                .FirstOrDefault(o => o.Email == email));

            if (user != null)
            {
                user.CanChangePassword = !string.IsNullOrEmpty(user.PasswordHash);
                user.Rating = GetRating(user.Id);
            }

            return user;
        }

        public IEnumerable<UserDTO> Get(UsersFilterViewModel model, out int count, Guid id)
        {
            var users = _context.Users
                .Include(u => u.Photo)
                .Include(u => u.Role)
                .AsNoTracking()
                .AsEnumerable();

            users = !string.IsNullOrEmpty(model.KeyWord)
                ? users.Where(x => x.Email.Contains(model.KeyWord) ||
                    (x.Name != null && x.Name.Contains(model.KeyWord)))
                : users;

            users = !string.IsNullOrEmpty(model.Role)
                ? users.Where(x => x.Role.Name.Contains(model.Role))
                : users;

            users = model.Blocked
                ? users.Where(x => x.IsBlocked == model.Blocked)
                : users;

            users = model.UnBlocked
                ? users.Where(x => x.IsBlocked == !model.UnBlocked)
                : users;

            users = (model.IsConfirmed != null)
                ? users.Where(x => x.EmailConfirmed == model.IsConfirmed)
                : users;

            count = users.Count();

            users = users.Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize).ToList();

            var result = _mapper.Map<IEnumerable<UserDTO>>(users);

            foreach (var u in result)
            {
                u.Rating = GetRating(u.Id);

                var rel = _context.Relationships
                    .FirstOrDefault(x => x.UserFromId == id && x.UserToId == u.Id);

                u.Attitude = (rel != null) ? (byte)rel.Attitude : (byte)2;
            }

            return result;
        }

        public IEnumerable<UserDTO> GetUsersByRole(string role)
        {
            var users = _context.Users
               .Include(u => u.Role)
               .Where(user => user.Role.Name == role)
               .Include(user => user.RefreshTokens)
               .AsNoTracking()
               .AsEnumerable();

            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public IEnumerable<UserDTO> GetUsersByCategories(IEnumerable<CategoryDTO> categories)
        {
            var categoryIds = categories.Select(x => x.Id).ToList();

            var users = _context.Users
                .Include(u => u.Photo)
                .Include(u => u.Role)
                .Include(u => u.Categories)
                    .ThenInclude(c => c.Category)
                .Where(user => user.Categories
                    .Any(category => categoryIds.Contains(category.Category.Id)))
                .Distinct()
                .AsEnumerable();

            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public UserDTO GetUserByRefreshToken(string token)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .Include(u => u.RefreshTokens)
                .SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token.Equals(token)));

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<OperationResult> ChangeRole(Guid uId, Guid rId)
        {
            var newRole = _context.Roles.Find(rId);
            if (newRole == null)
            {
                return new OperationResult(false, "Invalid role Id", "roleId");
            }

            var user = _context.Users.Find(uId);
            if (user == null)
            {
                return new OperationResult(false, "Invalid user Id", "userId");
            }

            user.Role = newRole;
            await _context.SaveChangesAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> ChangeAvatar(Guid userId, IFormFile avatar)
        {
            var user = _context.Users
                .Include(u => u.Photo)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return new OperationResult(false, "User not found", "Id");
            }

            if (user.Photo != null)
            {
                await _photoService.Delete(user.Photo.Id);
            }

            try
            {
                user.Photo = await _photoService.AddPhoto(avatar);
                Update(user);
                await _context.SaveChangesAsync();
                return new OperationResult(true);
            }
            catch
            {
                return new OperationResult(false, "Bad image file", "Id");
            }
        }

        public async Task<OperationResult> Unblock(Guid userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return new OperationResult(false, "Invalid user Id", "userId");
            }

            user.IsBlocked = false;
            await _context.SaveChangesAsync();
            await _mediator.Publish(new UnblockedUserMessage(user.Email));
            return new OperationResult(true);
        }

        public async Task<OperationResult> Block(Guid uId)
        {
            var user = _context.Users.Find(uId);
            if (user == null)
            {
                return new OperationResult(false, "Invalid user Id", "userId");
            }

            user.IsBlocked = true;
            await _context.SaveChangesAsync();
            await _mediator.Publish(new BlockedUserMessage(user.Email));
            return new OperationResult(true);
        }

        public async Task<OperationResult> EditFavoriteCategories(UserDTO userDTO, IEnumerable<Category> categories)
        {
            var u = _context.Users
                .Include(u => u.Categories)
                .Single(user => user.Id == userDTO.Id);

            var newCategories = categories
                .Select(x => new UserCategory { UserId = u.Id, CategoryId = x.Id })
                .ToList();

            u.Categories = newCategories;

            try
            {
                Update(u);
                await _context.SaveChangesAsync();

                return new OperationResult(true);
            }
            catch (Exception)
            {
                return new OperationResult(false, "Update failing", string.Empty);
            }
        }

        public async Task<OperationResult> SetAttitude(AttitudeDTO attitude)
        {
            var currentAttitude = _context.Relationships
                .FirstOrDefault(x => x.UserFromId == attitude.UserFromId && x.UserToId == attitude.UserToId);
            if (currentAttitude == null)
            {
                var rel = _mapper.Map<AttitudeDTO, Relationship>(attitude);
                try
                {
                    _context.Relationships.Add(rel);
                    await _context.SaveChangesAsync();

                    return new OperationResult(true);
                }
                catch (Exception)
                {
                    return new OperationResult(false, "Set failing", string.Empty);
                }
            }

            currentAttitude.Attitude = (Attitude)attitude.Attitude;
            await _context.SaveChangesAsync();
            return new OperationResult(true);
        }

        public AttitudeDTO GetAttitude(AttitudeDTO attitude) =>
            _mapper.Map<Relationship, AttitudeDTO>(_context.Relationships
                .FirstOrDefault(x =>
                    x.UserFromId == attitude.UserFromId && 
                    x.UserToId == attitude.UserToId));

        public ProfileDTO GetProfileById(Guid id, Guid fromId)
        {
            var user = _mapper.Map<UserDTO, ProfileDTO>(GetById(id));

            var rel = _context.Relationships
                .FirstOrDefault(x => x.UserFromId == fromId && x.UserToId == id);
            user.Attitude = (rel != null)
                ? (byte)rel.Attitude
                : (byte)Attitude.None;

            user.Rating = GetRating(user.Id);

            return user;
        }

        public double GetRating(Guid userId)
        {
            var ownEventsIds = Db.EventOwnersRepository.Get()
                .Where(e => e.UserId == userId).Select(e => e.EventId).ToList();
            try
            {
                return _context.Rates
                    .Where(r => ownEventsIds.Contains(r.EventId))
                    .Average(r => r.Score);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
