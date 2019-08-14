using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MediatR;
using EventsExpress.Core.Notifications;
using Microsoft.Extensions.Caching.Memory;
using EventsExpress.Core.NotificationHandlers;
using EventsExpress.Db.Helpers;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.Services
{
    public class UserService : IUserService
    {
        public IUnitOfWork Db { get; set; }

        private readonly IMapper _mapper;
        private IPhotoService _photoService;
        private readonly IMediator _mediator;
        private CacheHelper _cacheHelper;
        private IEmailService _emailService;            
        private IEventService _eventService;

        public UserService(IUnitOfWork uow,
            IMapper mapper,
            IPhotoService photoSrv,
            IMediator mediator,
            CacheHelper cacheHelper,
            IEmailService emailService,
            IEventService eventService
            )                                                                                                     
        {
            Db = uow;
            _mapper = mapper;
            _photoService = photoSrv;
            _eventService = eventService;
            _mediator = mediator;
            _cacheHelper = cacheHelper;
            _emailService = emailService;
        }

        public async Task<OperationResult> Create(UserDTO userDto)
        {
           

            if (Db.UserRepository.Get().FirstOrDefault(u => u.Email == userDto.Email) != null)
            {
                return new OperationResult(false, "Emali is exist in database", "Email");
            }
            User user = _mapper.Map<UserDTO, User>(userDto);

            user.Role = Db.RoleRepository.Get().FirstOrDefault(r => r.Name == "User");
            var result = Db.UserRepository.Insert(user);

            if (result.Email == user.Email && result.Id != null)
            {
                await Db.SaveAsync();
                userDto.Id = result.Id;
                await _mediator.Publish(new RegisterVerificationMessage(userDto));
                return new OperationResult(true, "Registration succeeded", "");
            }

            return new OperationResult(false, "Registration is failed", "");
        }

        public async Task<OperationResult> Verificate(CacheDTO cacheDto)
        {
            var user = Db.UserRepository.Get(cacheDto.UserId);
            if (user == null)
            {
                return new OperationResult(false, "Invalid user Id", "userId");
            }
           

            if (string.IsNullOrEmpty(cacheDto.Token))
            {
                return new OperationResult(false,"Token is null or empty","verification token");
            }

            if (cacheDto.Token == _cacheHelper.GetValue(cacheDto.UserId).Token)
            {
                user.EmailConfirmed = true;
                await Db.SaveAsync();

                _cacheHelper.Delete(cacheDto.UserId);
                return new OperationResult(true, "Verify succeeded", "");
            }

            return new OperationResult(false, "Validation failed", "");
        }

        public async Task<OperationResult> PasswordRecover(UserDTO userDto)
        {
            if (userDto == null)
            {
                return new OperationResult(false, "Not found", "");
            }
            var user = Db.UserRepository.Get(userDto.Id);
            if (user == null)
            {
                return new OperationResult(false, "Not found", "");
            }

            var newPassword = Guid.NewGuid().ToString();

            user.PasswordHash = PasswordHasher.GenerateHash(newPassword);

            try
            {
                await Db.SaveAsync();
                await _emailService.SendEmailAsync(new EmailDTO
                {
                    RecepientEmail = user.Email,
                    SenderEmail = "noreply@EventExpress.com",
                    MessageText = $"Hello, {user.Email}.\nYour new Password is: {newPassword}"

                });
                return new OperationResult(true, "Password Changed", "");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, "Something is wrong", "");
            }

            
        }

        public async Task<OperationResult> Update(UserDTO userDTO)
        {
            if (string.IsNullOrEmpty(userDTO.Email))
            {
                return new OperationResult(false, "EMAIL cannot be empty", "Email");
            }

            if (!Db.UserRepository.Get().Any(u => u.Id == userDTO.Id))
            {
                return new OperationResult(false, "Not found", "");
            }

            var result = _mapper.Map<UserDTO, User>(userDTO);
            try
            {
                Db.UserRepository.Update(result);
                await Db.SaveAsync();
            }
            catch (Exception e)
            {
                return new OperationResult(false, $"{e.Message}", "");
            }
            return new OperationResult(true);
        }

        public UserDTO GetById(Guid id)
        {
            var user = _mapper.Map<UserDTO>(Db.UserRepository.Get(includeProperties: "Photo,Categories.Category,Events,Role").Where(x => x.Id == id).FirstOrDefault());
            return user;
        }

        public UserDTO GetByEmail(string email)
        {
            var user = Db.UserRepository.Get(
                includeProperties: "Role,Categories.Category,Photo"
                ).Where(o => o.Email == email).AsNoTracking().FirstOrDefault();
            return _mapper.Map<UserDTO>(user);
        }

        public IEnumerable<UserDTO> GetAll(UsersFilterViewModel model, out int count)
        {


            IQueryable<User> users = Db.UserRepository.Get(includeProperties: "Photo,Role");
            if (model.KeyWord != null)
            {
                users = users.Where(x => x.Email.Contains(model.KeyWord) || x.Name.Contains(model.KeyWord));
            }
            if (model.Role != null)
            {
                users = users.Where(x => x.Role.Name.Contains(model.Role));
            }

            if (model.Blocked == true)
            {
                users = users.Where(x => x.IsBlocked == model.Blocked);
            }
            if (model.UnBlocked == true)
            {
                users = users.Where(x => x.IsBlocked == !(model.UnBlocked));
            }
            if (model.All == true)
            {
              var  Allusers = users;
            }
           
    
            count = users.Count();
            var IUsers = _mapper.Map<IEnumerable<UserDTO>>(users.Skip((model.Page - 1) * model.PageSize).Take(model.PageSize));

            return IUsers;
        }

        public IEnumerable<UserDTO> GetCategoriesFollowers(IEnumerable<CategoryDTO> categories)
        {
            var categoryIds = categories.Select(x => x.Id).ToList();

            var users = Db.UserRepository.Get(includeProperties: "Photo,Role,Categories.Category")
                .Where(user => user.Categories.Any(category => categoryIds.Contains(category.Category.Id))).Distinct()
                .AsEnumerable();

            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
        }

        public IEnumerable<UserDTO> Get(Expression<Func<User, bool>> filter)
        {
            var users = Db.UserRepository.Get().Where(filter);

            var result = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
            return result;
        }

        public async Task<OperationResult> ChangeRole(Guid uId, Guid rId)
        {
            var role = Db.RoleRepository.Get(rId);
            if (role == null)
            {
                return new OperationResult(false, "Invalid role Id", "roleId");
            }

            var user = Db.UserRepository.Get(uId);
            if (user == null)
            {
                return new OperationResult(false, "Invalid user Id", "userId");
            }

            user.Role = role;
            await Db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> ChangeAvatar(Guid uId, IFormFile avatar)
        {
            var user = Db.UserRepository
                .Get(includeProperties: "Photo").Where(u => u.Id == uId)
                .FirstOrDefault();
            if (user == null)
            {
                return new OperationResult(false, "User not found", "Id");
            }

            if (user.Photo != null)
            {
                try
                {
                    await _photoService.Delete(user.Photo.Id);
                }
                catch
                {
                    return new OperationResult(false, "Bad image file", "Id");
                }
            }
            user.Photo = await _photoService.AddPhoto(avatar);

            Db.UserRepository.Update(user);

            await Db.SaveAsync();
            return new OperationResult(true);
        }

        public async Task<OperationResult> Unblock(Guid uId)
        {
            var user = Db.UserRepository.Get(uId);
            if (user == null)
            {
                return new OperationResult(false, "Invalid user Id", "userId");
            }

            user.IsBlocked = false;

            await Db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> Block(Guid uId)
        {
            var user = Db.UserRepository.Get(uId);
            if (user == null)
            {
                return new OperationResult(false, "Invalid user Id", "userId");
            }

            user.IsBlocked = true;

            await Db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> EditFavoriteCategories(UserDTO userDTO, IEnumerable<Category> categories)
        {
            User u = Db.UserRepository.Get(includeProperties: "Categories").Single(user => user.Id == userDTO.Id);
            var temp = new List<UserCategory>();
            foreach (var c in categories)
            {
                temp.Add(new UserCategory
                {
                    UserId = u.Id,
                    CategoryId = c.Id
                });

            }
            u.Categories = temp;
            try
            {
                Db.UserRepository.Update(u);
                await Db.SaveAsync();

                return new OperationResult(true);
            }
            catch (Exception e)
            {
                return new OperationResult(false, "Update failing", "");
            }
        }

        public async Task<OperationResult> SetAttitude(AttitudeDTO attitude)
        {
            if (attitude.UserFromId == null || attitude.UserToId == null)
            {
                return new OperationResult(false, "Invalid user Id", "userId");
            }
            Relationship current_attitude = Db.RelationshipRepository.Get().Where(x => x.UserFromId == attitude.UserFromId).Where(y => y.UserToId == attitude.UserToId).FirstOrDefault();

            if (current_attitude == null)
            {
                Relationship rel = _mapper.Map<AttitudeDTO, Relationship>(attitude);
                try
                {
                    Db.RelationshipRepository.Insert(rel);
                    await Db.SaveAsync();

                    return new OperationResult(true);
                }
                catch (Exception e)
                {
                    return new OperationResult(false, "Set failing", "");
                }
            }
            current_attitude.Attitude = (Attitude)attitude.Attitude;
            await Db.SaveAsync();
            return new OperationResult(true);
        }

        public AttitudeDTO GetAttitude(AttitudeDTO attitude)
        {            
            AttitudeDTO rel = _mapper.Map<Relationship, AttitudeDTO>(Db.RelationshipRepository.Get()
                .Where(x => (x.UserFromId == attitude.UserFromId && x.UserToId == attitude.UserToId)).FirstOrDefault());

            return rel;
        }

        public ProfileDTO GetProfileById(Guid id, Guid FromId)
        {
            var user = _mapper.Map<UserDTO, ProfileDTO>(this.GetById(id));
            Relationship rel = Db.RelationshipRepository.Get()
                .Where(x => (x.UserFromId == FromId && x.UserToId == id)).FirstOrDefault();
            if (rel != null)
            {
                user.Attitude = (byte)rel.Attitude;
            }
            else
            {
                user.Attitude = 2;
            }
            return user;
        }
    }
}
