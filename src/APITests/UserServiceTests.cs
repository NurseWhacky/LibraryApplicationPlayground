using Microsoft.VisualStudio.TestTools.UnitTesting;
using API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Model;

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
    }
}