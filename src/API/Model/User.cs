using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model
{
    public class User : Entity
    {
        //public int UserId { get; init; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public enum UserRole  { Admin, User}
        public UserRole Role { get; set; }

        public User() { }
    }
}
