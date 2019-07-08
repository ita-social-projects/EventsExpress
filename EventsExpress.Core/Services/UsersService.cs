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
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.Services
{
   public class UsersService : IUsersService
    {
        private readonly IMapper _mapper;
        public IUnitOfWork Db { get; set; }

        public UsersService(IUnitOfWork uow, IMapper mapper)
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
            var oper = Db.UserRepository.Get(id);
            return _mapper.Map<UserDTO>(oper);
        }

        public UserDTO GetByEmail(string email)
        {
            var user = Db.UserRepository.Filter(filter: o => o.Email == email);
            return _mapper.Map<UserDTO>(user.FirstOrDefault());
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var users = Db.UserRepository.Get();

            var result = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
            return result;
        }
        /*  public async Task<User> GetCurrentUserAsync(HttpContext context)
          {
              User user = await Db.UserRepository.Get(context.User)..FirstOrDefault();

              return user;
          }*/
    }
}
