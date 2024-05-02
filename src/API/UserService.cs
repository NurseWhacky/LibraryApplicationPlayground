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
    public class UserService
    {
        private readonly IUserContext? userContext;
        private readonly IRepository<User> userRepository;

        //public UserService()
        //{
        //    userRepository = new XmlRepository<User>();
        //}

        public UserService(IRepository<User> userRepository, IUserContext currentUser)
        {
            this.userRepository = userRepository;
            this.userContext = currentUser;
        }


        public User? GetUserById(int id)
        {
            return userRepository.FindById(id);
        }

        public User? GetUserByUsername(string username)
        {
            // TODO : implement check for user role
            return userRepository.FindAll()
                .FirstOrDefault(u => (u.UserName == username));
        }
    }
}
