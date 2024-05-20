using API.Interfaces;
using API.Model;
using DataAccess;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;


        public UserService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }


        public User? GetUserById(int id)
        {
            return userRepository.FindById(id);
        }

        public User? GetUserByUsername(string username)
        {
            return userRepository.FindAll()
                .FirstOrDefault(u => u.Username == username);
        }

        public List<User> GetUsers()
        {
            return userRepository.FindAll().ToList();
        }
    }
}
