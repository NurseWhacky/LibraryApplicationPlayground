using Microsoft.VisualStudio.TestTools.UnitTesting;
using API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Model;
using API.DTOs;

namespace API.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        // TODO : problem with user "serialization" (Role), findbyid returns null
        UserService service = new UserService();
        [TestMethod()]
        public void GetUserByIdTest()
        {
            int id = 2;
            User? user = service.GetUserById(id);
            Assert.IsNotNull(user);
        }

        [TestMethod()]
        public void DoLoginTest()
        {
            UserLoginDTO userLoginDTO = new UserLoginDTO("user2", "hashed_password2");
            User? admin = service.DoLogin(userLoginDTO);
            Assert.IsNotNull(admin);
        }
    }
}