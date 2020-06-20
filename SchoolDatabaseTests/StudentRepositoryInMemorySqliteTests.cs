using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolDatabase.Tests
{
    [TestClass]
    public class StudentRepositoryInMemorySqliteTests
    {
        [TestMethod]
        public void StudentRepository_EmptyAllStudents_Test()
        {
            using var schoolContext = new InMemorySqliteSchoolContext();
            var studentRepository = new StudentRepository(schoolContext);

            Assert.IsFalse(studentRepository.AllStudents.Any());
        }
    }
}
