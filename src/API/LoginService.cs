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
    public class LoginService : ILogger
    {
        private readonly IRepository<User> userRepository;

        public LoginService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public LoggedUser? Login(UserLoginDTO credentials)
        {
            if(credentials.Username == null || credentials.Password == null)
            { return null; }

            User? authenticatedUser = userRepository.FindAll()
                .Where(u => (u.Username == credentials.Username && u.Password == credentials.Password))
                .FirstOrDefault();
            if (authenticatedUser == null)
            { return null; }
            return new LoggedUser(authenticatedUser);
        }
    }
}
