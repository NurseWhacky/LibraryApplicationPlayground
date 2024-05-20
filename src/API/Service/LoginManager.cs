using API.API_DTOs;
using API.DTOs;
using API.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Service
{
    public class LoginManager 
    {
        private IRepository repository;

        public LoginManager(IRepository repository)
        {
            this.repository = repository;
        }

        public LoggedUser? Login(UserLoginObject credentials)
        {
            return new LoggedUser(new Model.User());
        }
    }
}
