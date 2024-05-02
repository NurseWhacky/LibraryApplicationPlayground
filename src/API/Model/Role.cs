using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace API.Model
{
    public enum UserRole
    {
        [XmlEnum("Admin")]
        Admin,
        [XmlEnum("User")]
        User
    }
}
