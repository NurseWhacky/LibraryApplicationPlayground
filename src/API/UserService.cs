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
        private readonly LoggedUser? currentUser;
        private readonly IRepository<User> userRepository;

        
        public UserService(IRepository<User> userRepository, LoggedUser currentUser)
        {
            this.userRepository = userRepository;
            this.currentUser = currentUser;
        }


        public User? GetUserById(int id)
        {
            return userRepository.FindById(id);
        }

        public User? GetUserByUsername(string username)
        {
            // TODO : implement check for user role
            return userRepository.FindAll()
                .FirstOrDefault(u => (u.Username == username));
        }
    }
}
