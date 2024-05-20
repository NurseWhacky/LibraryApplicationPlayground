using API.Model;
using DataAccess;
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
            XmlUser = Utilities.FromEntity(user);
        }

        public UserDTO(XElement xmlUser)
        {
            XmlUser = xmlUser;
            User = xmlUser.ToEntity<User>() ?? new User();
        }
    }
}
