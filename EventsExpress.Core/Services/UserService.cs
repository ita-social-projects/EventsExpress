using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Db.Helpers;
using EventsExpress.Db.IRepo;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IMediator _mediator;
        private readonly ICacheHelper _cacheHelper;
        private readonly IEmailService _emailService;

        public UserService(
            IUnitOfWork uow,
            IMapper mapper,
            IPhotoService photoSrv,
            IMediator mediator,
            ICacheHelper cacheHelper,
            IEmailService emailService)
        {
            Db = uow;
            _mapper = mapper;
            _photoService = photoSrv;
            _mediator = mediator;
            _cacheHelper = cacheHelper;
            _emailService = emailService;
        }

        public IUnitOfWork Db { get; set; }

        public async Task Create(UserDTO userDto)
        {
            if (Db.UserRepository.Get().Any(u => u.Email == userDto.Email))
            {
                throw new EventsExpressException("Email already exists in database");
            }

            var user = _mapper.Map<User>(userDto);

            user.Role = Db.RoleRepository.Get().FirstOrDefault(r => r.Name == "User");

            var result = Db.UserRepository.Insert(user);
            if (result.Email != user.Email || result.Id == Guid.Empty)
            {
                throw new EventsExpressException("Registration failed");
            }

            await Db.SaveAsync();
            userDto.Id = result.Id;
            if (!userDto.EmailConfirmed)
            {
                await _mediator.Publish(new RegisterVerificationMessage(userDto));
            }
        }

        public async Task ConfirmEmail(CacheDTO cacheDto)
        {
            var user = Db.UserRepository.Get(cacheDto.UserId);
            if (user == null)
            {
                throw new EventsExpressException("Invalid user Id");
            }

            if (string.IsNullOrEmpty(cacheDto.Token))
            {
                throw new EventsExpressException("Token is null or empty");
            }

            if (cacheDto.Token != _cacheHelper.GetValue(cacheDto.UserId).Token)
            {
                throw new EventsExpressException("Validation failed");
            }

            user.EmailConfirmed = true;
            await Db.SaveAsync();
            _cacheHelper.Delete(cacheDto.UserId);
        }

        public async Task PasswordRecover(UserDTO userDto)
        {
            var user = Db.UserRepository.Get(userDto.Id);
            if (user == null)
            {
                throw new EventsExpressException("Not found");
            }

            var newPassword = Guid.NewGuid().ToString();
            user.PasswordHash = PasswordHasher.GenerateHash(newPassword);

            try
            {
                await Db.SaveAsync();
                await _emailService.SendEmailAsync(new EmailDTO
                {
                    Subject = "EventsExpress password recovery",
                    RecepientEmail = user.Email,
                    MessageText = $"Hello, {user.Email}.\nYour new Password is: {newPassword}",
                });
            }
            catch (Exception)
            {
                throw new EventsExpressException("Something is wrong");
            }
        }

        public async Task Update(UserDTO userDTO)
        {
            if (string.IsNullOrEmpty(userDTO.Email))
            {
                throw new EventsExpressException("EMAIL cannot be empty");
            }

            if (!Db.UserRepository.Get().Any(u => u.Id == userDTO.Id))
            {
                throw new EventsExpressException("Not found");
            }

            var result = _mapper.Map<UserDTO, User>(userDTO);
            try
            {
                Db.UserRepository.Update(result);
                await Db.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new EventsExpressException(ex.Message);
            }
        }

        public UserDTO GetById(Guid id)
        {
            var user = _mapper.Map<UserDTO>(Db.UserRepository
                .Get("Photo,Categories.Category,Events,Role")
                .FirstOrDefault(x => x.Id == id));

            user.Rating = GetRating(user.Id);
            return user;
        }

        public UserDTO GetByEmail(string email)
        {
            var user = _mapper.Map<UserDTO>(Db.UserRepository
                .Get("Role,Categories.Category,Photo")
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
            var users = Db.UserRepository.Get("Photo,Role").AsNoTracking().AsEnumerable();

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

                var rel = Db.RelationshipRepository.Get()
                    .FirstOrDefault(x => x.UserFromId == id && x.UserToId == u.Id);

                u.Attitude = (rel != null) ? (byte)rel.Attitude : (byte)2;
            }

            return result;
        }

        public IEnumerable<UserDTO> GetUsersByRole(string role)
        {
            var users = Db.UserRepository.Get("Role")
               .Where(user => user.Role.Name == role)
               .Include(user => user.RefreshTokens)
               .AsNoTracking()
               .AsEnumerable();

            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public IEnumerable<UserDTO> GetUsersByCategories(IEnumerable<CategoryDTO> categories)
        {
            var categoryIds = categories.Select(x => x.Id).ToList();

            var users = Db.UserRepository.Get("Photo,Role,Categories.Category")
                .Where(user => user.Categories
                .Any(category => categoryIds.Contains(category.Category.Id)))
                .Distinct()
                .AsEnumerable();

            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public UserDTO GetUserByRefreshToken(string token)
        {
            var user = Db.UserRepository.Get("Role,RefreshTokens").AsNoTracking()
                .SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token.Equals(token)));

            return _mapper.Map<UserDTO>(user);
        }

        public async Task ChangeRole(Guid uId, Guid rId)
        {
            var newRole = Db.RoleRepository.Get(rId);
            if (newRole == null)
            {
                throw new EventsExpressException("Invalid role Id");
            }

            var user = Db.UserRepository.Get(uId);
            if (user == null)
            {
                throw new EventsExpressException("Invalid user Id");
            }

            user.Role = newRole;
            await Db.SaveAsync();
        }

        public async Task ChangeAvatar(Guid uId, IFormFile avatar)
        {
            var user = Db.UserRepository.Get("Photo").FirstOrDefault(u => u.Id == uId);
            if (user == null)
            {
                throw new EventsExpressException("User not found");
            }

            if (user.Photo != null)
            {
                await _photoService.Delete(user.Photo.Id);
            }

            try
            {
                user.Photo = await _photoService.AddPhoto(avatar);
                Db.UserRepository.Update(user);
                await Db.SaveAsync();
            }
            catch
            {
                throw new EventsExpressException("Bad image file");
            }
        }

        public async Task Unblock(Guid uId)
        {
            var user = Db.UserRepository.Get(uId);
            if (user == null)
            {
                throw new EventsExpressException("Invalid user Id");
            }

            user.IsBlocked = false;
            await Db.SaveAsync();
            await _mediator.Publish(new UnblockedUserMessage(user.Email));
        }

        public async Task Block(Guid uId)
        {
            var user = Db.UserRepository.Get(uId);
            if (user == null)
            {
                throw new EventsExpressException("Invalid user Id");
            }

            user.IsBlocked = true;
            await Db.SaveAsync();
            await _mediator.Publish(new BlockedUserMessage(user.Email));
        }

        public async Task EditFavoriteCategories(UserDTO userDTO, IEnumerable<Category> categories)
        {
            var u = Db.UserRepository.Get("Categories")
                .Single(user => user.Id == userDTO.Id);

            var newCategories = categories
                .Select(x => new UserCategory { UserId = u.Id, CategoryId = x.Id })
                .ToList();

            u.Categories = newCategories;

            try
            {
                Db.UserRepository.Update(u);
                await Db.SaveAsync();
            }
            catch (Exception)
            {
                throw new EventsExpressException("Update failing");
            }
        }

        public async Task SetAttitude(AttitudeDTO attitude)
        {
            var currentAttitude = Db.RelationshipRepository.Get()
                .FirstOrDefault(x => x.UserFromId == attitude.UserFromId && x.UserToId == attitude.UserToId);
            if (currentAttitude == null)
            {
                var rel = _mapper.Map<AttitudeDTO, Relationship>(attitude);
                try
                {
                    Db.RelationshipRepository.Insert(rel);
                    await Db.SaveAsync();
                }
                catch (Exception)
                {
                    throw new EventsExpressException("Set failing");
                }
            }

            currentAttitude.Attitude = (Attitude)attitude.Attitude;
            await Db.SaveAsync();
            return;
        }

        public AttitudeDTO GetAttitude(AttitudeDTO attitude) =>
            _mapper.Map<Relationship, AttitudeDTO>(Db.RelationshipRepository.Get()
                .FirstOrDefault(x => x.UserFromId == attitude.UserFromId && x.UserToId == attitude.UserToId));

        public ProfileDTO GetProfileById(Guid id, Guid fromId)
        {
            var user = _mapper.Map<UserDTO, ProfileDTO>(GetById(id));

            var rel = Db.RelationshipRepository.Get()
                .FirstOrDefault(x => x.UserFromId == fromId && x.UserToId == id);
            user.Attitude = (rel != null)
                ? (byte)rel.Attitude
                : (byte)2;

            user.Rating = GetRating(user.Id);

            return user;
        }

        public double GetRating(Guid userId)
        {
            var ownEventsIds = Db.EventRepository.Get()
                .Where(e => e.OwnerId == userId).Select(e => e.Id).ToList();
            try
            {
                return Db.RateRepository.Get()
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
