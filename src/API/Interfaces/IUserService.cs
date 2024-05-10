using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Interfaces
{
    interface IUserService
    {
        List<User> GetUsers();
        User GetUserById(int id);
        User GetUserByUsername(string username);
    }
}
