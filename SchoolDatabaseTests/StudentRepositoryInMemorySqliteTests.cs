using System.Data.Common;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolDatabase.Tests
{
    [TestClass]
#pragma warning disable CA1001 // Types that own disposable fields should be disposable
    public class StudentRepositoryInMemorySqliteTests
#pragma warning restore CA1001 // Types that own disposable fields should be disposable
    {
        private DbConnection connection;
        private SchoolContext schoolContext;

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.connection = CreateInMemoryDatabase();
            this.schoolContext = new SchoolContext(new DbContextOptionsBuilder<SchoolContext>()
                .UseSqlite(this.connection)
                .Options);

            this.schoolContext.Database.EnsureCreated();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (this.schoolContext != null)
            {
                this.schoolContext.Dispose();
            }

            if (this.connection != null)
            {
                this.connection.Dispose();
            }
        }

        [TestMethod]
        public void StudentRepository_Empty_Test()
        {
            var studentRepository = new StudentRepository(this.schoolContext);

            Assert.IsFalse(studentRepository.AllStudents.Any());
        }
    }
}
