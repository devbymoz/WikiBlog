using Microsoft.VisualStudio.TestTools.UnitTesting;
using WikiBlog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiBlog.Controllers;

namespace WikiBlog.Services.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        [TestMethod()]
        public void IsMajor_returnsTrue_WhenDateIsOver18Years()
        {
            // Arrange
            DateTime dateTime = DateTime.Now.AddYears(-19);

            //act
            bool result = UserService.IsMajor(dateTime);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsMajor_returnsFalse_WhenDateIsUnder18Years()
        {
            // Arrange
            DateTime dateTime = DateTime.Now.AddYears(-1);

            //act
            bool result = UserService.IsMajor(dateTime);

            // Assert
            Assert.IsFalse(result);
        }
    }
}