using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventsExpress.Core.Services
{
    public class UserService : IUserService
    {
        public IUnitOfWork Db { get; set; }

        private readonly IMapper _mapper;
        private IPhotoService _photoService;


        public UserService(IUnitOfWork uow, IMapper mapper, IPhotoService photoSrv)
        {
            Db = uow;
            _mapper = mapper;
            _photoService = photoSrv;
        }

        public async Task<OperationResult> Create(UserDTO userDto)
        {
            if (Db.UserRepository.Filter(filter: u => u.Email == userDto.Email).FirstOrDefault() != null)
            {
                return new OperationResult(false, "Emali is exist in database", "Email");
            }
            User user = _mapper.Map<UserDTO, User>(userDto);
            
            user.Role = Db.RoleRepository.Filter(filter: r => r.Name == "User").FirstOrDefault();
            var result =  Db.UserRepository.Insert(user);

            if (result.Email == user.Email && result.Id != null)
            {
                await Db.SaveAsync();
                return new OperationResult(true, "Registration succeeded", "");
            }

            return new OperationResult(false, "Registration is failed", "");
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
            
            var result = _mapper.Map<User>(userDTO);
            try
            {
                Db.UserRepository.Update(result);
                await Db.SaveAsync();
            }
            catch
            {
                return new OperationResult(false, "Internal error", "");
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
                includeProperties: "Role"
                ).FirstOrDefault();
            return _mapper.Map<UserDTO>(user);
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var users = Db.UserRepository.Get();

            var result = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
            return result;
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
    }
}
