using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SchoolDatabase;

namespace AdvancedUnitTest.Controllers.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_EmptyStudent_Test()
        {
            var logger = Mock.Of<ILogger<HomeController>>();
            var schoolDatabase = Mock.Of<ISchoolDatabase>();
            var studentReposity = new StudentRepository(schoolDatabase);

            using var controller = new HomeController(logger, studentReposity);

            var actionResult = controller.Index(null, null);
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));

            var viewResult = actionResult as ViewResult;
            Assert.IsInstanceOfType(viewResult.Model, typeof(Student[]));

            var students = viewResult.Model as Student[];
            Assert.AreEqual(0, students.Length);
        }
    }
}
