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
        
        public UserService(IUnitOfWork uow,
            IMapper mapper,
            IPhotoService photoSrv,
            IMediator mediator,
            CacheHelper cacheHelper,
            IEmailService emailService
            )
        {
            Db = uow;
            _mapper = mapper;
            _photoService = photoSrv;
            _mediator = mediator;
            _cacheHelper = cacheHelper;
            _emailService = emailService;
        }

        public async Task<OperationResult> Create(UserDTO userDto)
        {
            if (Db.UserRepository.Filter(filter: u => u.Email == userDto.Email).FirstOrDefault() != null)
            {
                return new OperationResult(false, "Emali is exist in database", "Email");
            }
            User user = _mapper.Map<UserDTO, User>(userDto);

            user.Role = Db.RoleRepository.Filter(filter: r => r.Name == "User").FirstOrDefault();
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
           // _cacheHelper.GetValue(cacheDto.UserId);

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
            var user = Db.UserRepository.Get(userDto.Id);
            if (user == null)
            {
                return new OperationResult(false, "Invalid user Id", "Id");
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
            var user = Db.UserRepository.Get(id);
            return _mapper.Map<UserDTO>(user);
        }

        public UserDTO GetByEmail(string email)
        {
            var user = Db.UserRepository.Filter(
                filter: o => o.Email == email,
                includeProperties: "Role,Categories.Category,Photo"
                ).AsNoTracking().FirstOrDefault();
            return _mapper.Map<UserDTO>(user);
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var users = Db.UserRepository.Get();

            var result = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);

            return result;
        }

        public IEnumerable<UserDTO> GetCategoriesFollowers(IEnumerable<CategoryDTO> categories)
        {
            var categoryNames = new List<string> { "Golf", "Summer" };

            var users = Db.UserRepository.Filter(includeProperties: "Categories.Category")
                .Where(user => user.Categories.Any(category => categoryNames.Contains(category.Category.Name)))
                .ToList();
           
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
        }

        public IEnumerable<UserDTO> Get(Expression<Func<User, bool>> filter)
        {
            var users = Db.UserRepository.Filter(filter: filter);

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
                .Filter(filter: u => u.Id == uId, includeProperties: "Photo")
                .FirstOrDefault();
            if (user == null)
            {
                return new OperationResult(false, "User not found", "Id");
            }

            if (user.Photo != null)
            {
                await _photoService.Delete(user.Photo.Id);
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

            user.IsBlocked = true;

            await Db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> EditFavoriteCategories(UserDTO userDTO, IEnumerable<Category> categories)
        {
            User u = Db.UserRepository.Filter(includeProperties: "Categories").Single(user => user.Id == userDTO.Id);
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
    }
}
