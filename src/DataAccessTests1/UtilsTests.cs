using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAccess.Tests
{
    [TestClass()]
    public class UtilsTests
    {
        [TestMethod()]
        public void ReadDataSetFromFileTest()
        {
            DataSet dataset = Utils.ReadDataSetFromFile();

            foreach (DataTable dt in dataset.Tables)
            {
                Utils.PrintColumns(dt);

            }

            Assert.IsTrue(dataset is not null);
        }
    }
}