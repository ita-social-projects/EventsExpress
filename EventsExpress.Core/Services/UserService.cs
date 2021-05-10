using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IPhotoService _photoService;

        public UserService(
            AppDbContext context,
            IMapper mapper,
            IPhotoService photoSrv)
            : base(context, mapper)
        {
            _photoService = photoSrv;
        }

        public async Task Create(UserDto userDto)
        {
            if (Context.Users.Any(u => u.Email == userDto.Email))
            {
                throw new EventsExpressException("Email already exists in database");
            }

            var user = Mapper.Map<User>(userDto);
            var newUser = Insert(user);
            if (newUser.Email != user.Email || newUser.Id == Guid.Empty)
            {
                throw new EventsExpressException("Registration failed");
            }

            var account = Context.Accounts.Find(userDto.AccountId);
            account.UserId = newUser.Id;

            await Context.SaveChangesAsync();
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
                .Include(u => u.Events)
                /*.Include(u => u.Account)
                    .ThenInclude(a => a.AccountRoles)
                        .ThenInclude(ar => ar.Role)
                .Include(u => u.Account)
                    .ThenInclude(a => a.AuthLocal)
                .Include(u => u.Account)
                    .ThenInclude(a => a.AuthExternal)*/
                .Include(u => u.Categories)
                    .ThenInclude(c => c.Category)
                .Include(u => u.NotificationTypes)
                    .ThenInclude(n => n.NotificationType)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == userId));

            return user;
        }

        public UserDto GetByEmail(string email)
        {
            var user = Mapper.Map<UserDto>(Context.Users
                .Include(u => u.Events)
                .Include(u => u.Categories)
                    .ThenInclude(c => c.Category)
                .Include(u => u.NotificationTypes)
                    .ThenInclude(n => n.NotificationType)
                .Include(u => u.Relationships)
                .AsNoTracking()
                .FirstOrDefault(o => o.Email == email));

            return user;
        }

        public IEnumerable<UserDto> Get(UsersFilterViewModel model, out int count, Guid id)
        {
            var users = Context.Users
                .Include(u => u.Account)
                    .ThenInclude(a => a.AuthLocal)
                .Include(u => u.Account)
                    .ThenInclude(a => a.AccountRoles)
                        .ThenInclude(ar => ar.Role)
                .Include(u => u.Relationships)
                .AsNoTracking();

            users = !string.IsNullOrEmpty(model.KeyWord)
                ? users.Where(x => x.Email.Contains(model.KeyWord) ||
                    (x.Name != null && x.Name.Contains(model.KeyWord)))
                : users;

            users = !string.IsNullOrEmpty(model.Role)
              ? users.Where(x => x.Account.AccountRoles.Any(ar => ar.Role.Name == model.Role))
              : users;

            users = model.Blocked
                ? users.Where(x => x.Account.IsBlocked == model.Blocked)
                : users;

            users = model.UnBlocked
                ? users.Where(x => x.Account.IsBlocked == !model.UnBlocked)
                : users;

            users = (model.IsConfirmed != null)
                ? users.Where(x => x.Account.AuthLocal.EmailConfirmed == model.IsConfirmed)
                : users;

            count = users.Count();

            var usersList = users.Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize).ToList();

            var result = Mapper.Map<IEnumerable<UserDto>>(usersList);

            return result;
        }

        public IEnumerable<UserDto> GetUsersByRole(string role)
        {
            var users = Context.Roles
                .Include(r => r.Accounts)
                    .ThenInclude(ar => ar.Account)
                        .ThenInclude(a => a.User)
               .Where(r => r.Name == role)
               .AsNoTracking()
               .FirstOrDefault()
               .Accounts
               .Select(ar => ar.Account.User);

            return Mapper.Map<IEnumerable<UserDto>>(users);
        }

        public IEnumerable<UserDto> GetUsersByCategories(IEnumerable<CategoryDto> categories)
        {
            var categoryIds = categories.Select(x => x.Id).ToList();

            var users = Context.Users
                .Include(u => u.Categories)
                    .ThenInclude(c => c.Category)
                .Where(user => user.Categories
                    .Any(category => categoryIds.Contains(category.Category.Id)))
                .Distinct()
                .AsEnumerable();

            return Mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task ChangeAvatar(Guid userId, IFormFile avatar)
        {
            var user = Context.Users
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new EventsExpressException("User not found");
            }

            try
            {
                await _photoService.AddUserPhoto(avatar, user.Id);
            }
            catch (ArgumentException)
            {
                throw new EventsExpressException("Bad image file");
            }
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
                return;
            }

            currentAttitude.Attitude = (Attitude)attitude.Attitude;
            await Context.SaveChangesAsync();
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

        public IEnumerable<UserDto> GetUsersByNotificationTypes(NotificationChange notificationType, IEnumerable<Guid> userIds)
        {
            var users = Context.UserNotificationTypes.Include(unt => unt.User)
                                    .Where(x => x.NotificationTypeId == notificationType && userIds.Contains(x.UserId))
                                    .Select(x => x.User)
                                    .AsEnumerable();
            return Mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<Guid> EditFavoriteNotificationTypes(UserDto userDto, IEnumerable<NotificationType> notificationTypes)
        {
            var user = Context.Users
                .Include(u => u.NotificationTypes)
                .Single(user => user.Id == userDto.Id);

            var newNotificationTypes = notificationTypes
                .Select(x => new UserNotificationType { UserId = user.Id, NotificationTypeId = x.Id })
                .ToList();

            user.NotificationTypes = newNotificationTypes;

            Update(user);
            await Context.SaveChangesAsync();
            return user.Id;
        }
    }
}
