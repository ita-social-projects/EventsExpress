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
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.Services
{
   public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        public IUnitOfWork Db { get; set; }

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            Db = uow;
            _mapper = mapper;
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

            if (result==user)
            {
                await Db.SaveAsync();
                return new OperationResult(true, "Registration succeeded", "");
            }

            return new OperationResult(false, "Registration is failed", "");
        }

        public async Task<OperationResult> Update(UserDTO userDTO)
        {
            if (string.IsNullOrEmpty(userDTO.Name))
                return new OperationResult(false, "Operator name cannot be empty", "");
            var check = Db.UserRepository.Filter(filter: o => o.Name == userDTO.Name).FirstOrDefault();
            if (check != null)
                return new OperationResult(false, "Operator name already exist", "");
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
