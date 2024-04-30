using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess.Tests
{
    [TestClass()]
    public class XmlExtensionsTests
    {
            public class Persona { public string Name { get; set; } public string Surname { get; set; } public int Age { get; set; } }
        //[TestMethod()]
        //public void ToEntityTest()
        //{
        //    XElement personElement = new XElement("Persona", new XElement("Name", "pippo"), new XElement("Surname", "topolinis"), new XElement("Age", "15"));
        //    Persona personaFromXEl = personElement.ToEntity<Persona>();
        //    Assert.AreEqual(int.Parse("15"), personaFromXEl.Age);
        //    //Assert.Fail();
        //}
    }
}