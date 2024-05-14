using API.Interfaces;
using API.Model;
using DataAccess;

namespace API.Services
{
    public class UserService : IUserService
    {
        //private readonly LoggedUser? currentUser;
        private readonly IRepository<User> userRepository;


        //public UserService(IRepository<User> userRepository, LoggedUser currentUser)
        public UserService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
            //this.currentUser = currentUser;
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
