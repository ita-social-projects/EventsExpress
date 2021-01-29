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

        public async Task Create(UserDto userDto)
        {
            if (Context.Users.Any(u => u.Email == userDto.Email))
            {
                throw new EventsExpressException("Email already exists in database");
            }

            var user = Mapper.Map<User>(userDto);

            user.Role = Context.Roles.FirstOrDefault(r => r.Name == "User");

            var result = Insert(user);
            if (result.Email != user.Email || result.Id == Guid.Empty)
            {
                throw new EventsExpressException("Registration failed");
            }

            await Context.SaveChangesAsync();
            userDto.Id = result.Id;
            if (!userDto.EmailConfirmed)
            {
                await _mediator.Publish(new RegisterVerificationMessage(userDto));
            }
        }

        public async Task ConfirmEmail(CacheDto cacheDto)
        {
            var user = Context.Users.Find(cacheDto.UserId);
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
            await Context.SaveChangesAsync();
            _cacheHelper.Delete(cacheDto.UserId);
        }

        public async Task PasswordRecover(UserDto userDto)
        {
            var user = Context.Users.Find(userDto.Id);
            if (user == null)
            {
                throw new EventsExpressException("Not found");
            }

            var newPassword = Guid.NewGuid().ToString();
            user.PasswordHash = PasswordHasher.GenerateHash(newPassword);

            await Context.SaveChangesAsync();
            await _emailService.SendEmailAsync(new EmailDto
            {
                Subject = "EventsExpress password recovery",
                RecepientEmail = user.Email,
                MessageText = $"Hello, {user.Email}.\nYour new Password is: {newPassword}",
            });
        }

        public async Task Update(UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.Email))
            {
                throw new EventsExpressException("EMAIL cannot be empty");
            }

            if (!Context.Users.Any(u => u.Id == userDto.Id))
            {
                throw new EventsExpressException("Not found");
            }

            var result = Mapper.Map<UserDto, User>(userDto);

            Update(result);
            await Context.SaveChangesAsync();
        }

        public UserDto GetById(Guid userId)
        {
            var user = Mapper.Map<UserDto>(
                Context.Users
                .Include(u => u.Photo)
                .Include(u => u.Events)
                .Include(u => u.Role)
                .Include(u => u.Categories)
                    .ThenInclude(c => c.Category)
                .FirstOrDefault(x => x.Id == userId));

            user.Rating = GetRating(user.Id);

            return user;
        }

        public UserDto GetByEmail(string email)
        {
            var user = Mapper.Map<UserDto>(
                 Context.Users
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

        public IEnumerable<UserDto> Get(UsersFilterViewModel model, out int count, Guid id)
        {
            var users = Context.Users
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

            var result = Mapper.Map<IEnumerable<UserDto>>(users);

            foreach (var u in result)
            {
                u.Rating = GetRating(u.Id);

                var rel = Context.Relationships
                    .FirstOrDefault(x => x.UserFromId == id && x.UserToId == u.Id);

                u.Attitude = (rel != null) ? (byte)rel.Attitude : (byte)2;
            }

            return result;
        }

        public IEnumerable<UserDto> GetUsersByRole(string role)
        {
            var users = Context.Users
               .Include(u => u.Role)
               .Where(user => user.Role.Name == role)
               .Include(user => user.RefreshTokens)
               .AsNoTracking()
               .AsEnumerable();

            return Mapper.Map<IEnumerable<UserDto>>(users);
        }

        public IEnumerable<UserDto> GetUsersByCategories(IEnumerable<CategoryDto> categories)
        {
            var categoryIds = categories.Select(x => x.Id).ToList();

            var users = Context.Users
                .Include(u => u.Photo)
                .Include(u => u.Role)
                .Include(u => u.Categories)
                    .ThenInclude(c => c.Category)
                .Where(user => user.Categories
                    .Any(category => categoryIds.Contains(category.Category.Id)))
                .Distinct()
                .AsEnumerable();

            return Mapper.Map<IEnumerable<UserDto>>(users);
        }

        public UserDto GetUserByRefreshToken(string token)
        {
            var user = Context.Users
                .Include(u => u.Role)
                .Include(u => u.RefreshTokens)
                .AsNoTracking()
                .SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token.Equals(token)));

            return Mapper.Map<UserDto>(user);
        }

        public async Task ChangeRole(Guid userId, Guid roleId)
        {
            var newRole = Context.Roles.Find(roleId);
            var user = Context.Users.Find(userId);
            if (user == null)
            {
                throw new EventsExpressException("Invalid user Id");
            }

            user.Role = newRole ?? throw new EventsExpressException("Invalid role Id");
            await Context.SaveChangesAsync();
        }

        public async Task ChangeAvatar(Guid userId, IFormFile avatar)
        {
            var user = Context.Users
                .Include(u => u.Photo)
                .FirstOrDefault(u => u.Id == userId);

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
                Update(user);
                await Context.SaveChangesAsync();
            }
            catch (ArgumentException)
            {
                throw new EventsExpressException("Bad image file");
            }
        }

        public async Task Unblock(Guid userId)
        {
            var user = Context.Users.Find(userId);
            if (user == null)
            {
                throw new EventsExpressException("Invalid user Id");
            }

            user.IsBlocked = false;
            await Context.SaveChangesAsync();
            await _mediator.Publish(new UnblockedUserMessage(user.Email));
        }

        public async Task Block(Guid userId)
        {
            var user = Context.Users.Find(userId);
            if (user == null)
            {
                throw new EventsExpressException("Invalid user Id");
            }

            user.IsBlocked = true;
            await Context.SaveChangesAsync();
            await _mediator.Publish(new BlockedUserMessage(user.Email));
        }

        public async Task EditFavoriteCategories(UserDto userDto, IEnumerable<Category> categories)
        {
            var u = Context.Users
                .Include(u => u.Categories)
                .Single(user => user.Id == userDto.Id);

            var newCategories = categories
                .Select(x => new UserCategory { UserId = u.Id, CategoryId = x.Id })
                .ToList();

            u.Categories = newCategories;

            Update(u);
            await Context.SaveChangesAsync();
        }

        public async Task SetAttitude(AttitudeDto attitude)
        {
            var currentAttitude = Context.Relationships
                .FirstOrDefault(x => x.UserFromId == attitude.UserFromId && x.UserToId == attitude.UserToId);
            if (currentAttitude == null)
            {
                var rel = Mapper.Map<AttitudeDto, Relationship>(attitude);

                Context.Relationships.Add(rel);
                await Context.SaveChangesAsync();
            }

            currentAttitude.Attitude = (Attitude)attitude.Attitude;
            await Context.SaveChangesAsync();

            return;
        }

        public AttitudeDto GetAttitude(AttitudeDto attitude) =>
            Mapper.Map<Relationship, AttitudeDto>(Context.Relationships
                .FirstOrDefault(x =>
                    x.UserFromId == attitude.UserFromId &&
                    x.UserToId == attitude.UserToId));

        public ProfileDto GetProfileById(Guid userId, Guid fromId)
        {
            var user = Mapper.Map<UserDto, ProfileDto>(GetById(userId));

            var rel = Context.Relationships
                .FirstOrDefault(x => x.UserFromId == fromId && x.UserToId == userId);
            user.Attitude = (rel != null)
                ? (byte)rel.Attitude
                : (byte)Attitude.None;

            user.Rating = GetRating(user.Id);

            return user;
        }

        public double GetRating(Guid userId)
        {
            var ownEventsIds = Context.EventOwners
                .Where(e => e.UserId == userId).Select(e => e.EventId).ToList();
            try
            {
                return Context.Rates
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
