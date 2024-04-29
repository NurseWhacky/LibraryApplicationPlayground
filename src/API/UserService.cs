using API.DTOs;
using API.Model;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    internal class UserService
    {
        private readonly IRepository<User> userRepository;

        public UserService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public User? DoLogin(UserLoginDTO credendials)
        {
            if (credendials.Username is not null || credendials.Password is not null)
            {
                // TODO : implement hash password
                User user = userRepository.FindAll()
                    .FirstOrDefault(u => (u.UserName == credendials.Username) && (u.Password == credendials.Password));
                return user;
            }
            return null;

        }

        public User? GetUserByUsername(string username)
        {
            // TODO : implement check for user role
            return userRepository.FindAll()
                .FirstOrDefault(u => (u.UserName == username));
        }
    }
}
