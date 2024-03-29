﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Role = EventsExpress.Db.Enums.Role;

namespace EventsExpress.Core.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IMediator _mediator;
        private readonly IUserPhotoService _userPhotoService;
        private readonly ILocationManager _locationManager;
        private readonly ISecurityContext _securityContext;

        public UserService(
            AppDbContext context,
            IMapper mapper,
            IMediator mediator,
            IUserPhotoService photoSrv,
            ILocationManager locationManager,
            ISecurityContext securityContext)
            : base(context, mapper)
        {
            _mediator = mediator;
            _userPhotoService = photoSrv;
            _locationManager = locationManager;
            _securityContext = securityContext;
        }

        public async Task Create(UserDto userDto)
        {
            if (Context.Users.Any(u => u.Email == userDto.Email))
            {
                throw new EventsExpressException("Email already exists in database");
            }

            var user = Mapper.Map<User>(userDto);
            var newUser = Insert(user);
            if (newUser.Email != user.Email)
            {
                throw new EventsExpressException("Registration failed");
            }

            var account = await Context.Accounts.FindAsync(userDto.AccountId);

            if (account == null)
            {
                throw new EventsExpressException("Account not found");
            }

            account.UserId = newUser.Id;

            await Context.SaveChangesAsync();

            await _mediator.Publish(new CreatedUserMessage());
        }

        public async Task<int> CountUsersAsync(AccountStatus status)
        {
            var count = status switch
            {
                AccountStatus.Activated => await Entities.Where(user => !user.Account.IsBlocked).CountAsync(),
                AccountStatus.Blocked => await Entities.Where(user => user.Account.IsBlocked).CountAsync(),
                _ => await Entities.CountAsync(),
            };

            return count;
        }

        public UserDto GetCurrentUserInfo()
        {
            var userId = _securityContext.GetCurrentUserId();

            var user = Context.Users
                .Include(u => u.EventBookmarks)
                .Include(u => u.Location)
                .Include(u => u.Account)
                .ThenInclude(a => a.AccountRoles)
                        .ThenInclude(ar => ar.Role)
                .FirstOrDefault(x => x.Id == userId);

            var userDto = Mapper.Map<UserDto>(user);

            return userDto;
        }

        public IEnumerable<NotificationTypeDto> GetUserNotificationTypes()
        {
            var userId = _securityContext.GetCurrentUserId();

            return Mapper.Map<IEnumerable<NotificationTypeDto>>(Context.UserNotificationTypes
                            .Include(u => u.NotificationType)
                            .Where(u => u.UserId == userId)
                            .AsNoTracking()
                            .ToList());
        }

        public IEnumerable<CategoryDto> GetUserCategories()
        {
            var userId = _securityContext.GetCurrentUserId();

            return Mapper.Map<IEnumerable<CategoryDto>>(Context.UserCategory
                            .Include(u => u.Category)
                            .Where(uc => uc.UserId == userId)
                            .AsNoTracking()
                            .ToList());
        }

        public IEnumerable<UserDto> Get(UsersFilterViewModel model, out int count)
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

            var usersList = users
                .Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize).ToList();

            var result = Mapper.Map<IEnumerable<UserDto>>(usersList);

            return result;
        }

        public IEnumerable<UserDto> GetUsersInformationByIds(IEnumerable<Guid> ids)
        {
            var users = Context.Users.Where(user => ids.Contains(user.Id));
            return Mapper.Map<IEnumerable<UserDto>>(users.ToList());
        }

        public IEnumerable<UserDto> GetUsersByRole(Role role)
        {
            var users = Context.Roles
                .Include(r => r.Accounts)
                    .ThenInclude(ar => ar.Account)
                        .ThenInclude(a => a.User)
               .Where(r => r.Id.Equals(role))
               .AsNoTracking()
               .FirstOrDefault()
                ?.Accounts
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

        public async Task<Guid> ChangeAvatar(IFormFile avatar)
        {
            try
            {
                var userId = _securityContext.GetCurrentUserId();
                await _userPhotoService.AddUserPhoto(avatar, userId);
                return userId;
            }
            catch (ArgumentException)
            {
                throw new EventsExpressException("Bad image file");
            }
        }

        public async Task EditUserName(string name)
        {
            var user = CurrentUser();

            user.Name = name;

            Context.Update(user);
            await Context.SaveChangesAsync();
        }

        public async Task EditBirthday(DateTime birthday)
        {
            var user = CurrentUser();

            user.Birthday = birthday;

            Context.Update(user);
            await Context.SaveChangesAsync();
        }

        public async Task EditGender(Gender gender)
        {
            var user = CurrentUser();

            user.Gender = gender;

            Context.Update(user);
            await Context.SaveChangesAsync();
        }

        public async Task EditFirstName(string firstName)
        {
            var user = CurrentUser();

            user.FirstName = firstName;

            Context.Update(user);
            await Context.SaveChangesAsync();
        }

        public async Task EditLastName(string lastName)
        {
            var user = CurrentUser();

            user.LastName = lastName;

            Context.Update(user);
            await Context.SaveChangesAsync();
        }

        public async Task EditLocation(LocationDto location)
        {
            var user = CurrentUser();

            if (location.Type == LocationType.Map)
            {
                if (user.LocationId == null)
                {
                    var locationId = _locationManager.Create(location);
                    user.LocationId = locationId;
                }
                else
                {
                    location.Id = user.LocationId.Value;
                    _locationManager.EditLocation(location);
                }
            }
            else
            {
                throw new EventsExpressException("Uri-based location is not applicable to users");
            }

            Context.Update(user);
            await Context.SaveChangesAsync();
        }

        public async Task EditFavoriteCategories(IEnumerable<Category> categories)
        {
            var userId = _securityContext.GetCurrentUserId();

            var u = Context.Users
                .Include(u => u.Categories)
                .Single(user => user.Id == userId);

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

        public ProfileDto GetProfileById(Guid id)
        {
            var user = Mapper.Map<UserDto, ProfileDto>(
                Mapper.Map<UserDto>(Context.Users
                .Include(u => u.Categories)
                    .ThenInclude(c => c.Category)
                .Include(u => u.NotificationTypes)
                    .ThenInclude(n => n.NotificationType)
                .FirstOrDefault(x => x.Id == id)));

            return user;
        }

        public double GetRating(Guid userId)
        {
            var ownEventsIds = Context.EventOrganizers
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

        public async Task<Guid> EditFavoriteNotificationTypes(IEnumerable<NotificationType> notificationTypes)
        {
            var userId = _securityContext.GetCurrentUserId();

            var user = Context.Users
                .Include(u => u.NotificationTypes)
                .Single(user => user.Id == userId);

            var newNotificationTypes = notificationTypes
                .Select(x => new UserNotificationType { UserId = user.Id, NotificationTypeId = x.Id })
                .ToList();

            user.NotificationTypes = newNotificationTypes;

            Update(user);
            await Context.SaveChangesAsync();
            return user.Id;
        }

        private User CurrentUser()
        {
            var userId = _securityContext.GetCurrentUserId();

            var user = Context.Users.FirstOrDefault(x => x.Id == userId);

            return user;
        }
    }
}
