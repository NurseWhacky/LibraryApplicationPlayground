using API.DTOs;
using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface ILogger
    {
        LoggedUser Login(UserLoginDTO credentials);
    }
}
