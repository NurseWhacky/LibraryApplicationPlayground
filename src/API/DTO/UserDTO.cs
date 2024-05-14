using API.Model;
using API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace API.DTO
{
    public class UserDTO
    {
        public User User { get; set; }
        public XElement XmlUser { get; set; }

        public UserDTO(User user) 
        { 
            User = user; 
            XmlUser = user.FromEntity();
        }

        public UserDTO(XElement xmlUser)
        {
            XmlUser = xmlUser;
            User = xmlUser.ToEntity<User>() ?? new User();
        }
    }
}
