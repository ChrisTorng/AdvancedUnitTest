using System;
using System.Data.Common;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolDatabase.Tests
{
    [TestClass]
    public class StudentRepositoryInMemorySqliteTests : IDisposable
    {
        private DbConnection connection;
        private SchoolContext schoolContext;
        private bool disposed = false;

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

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.schoolContext.Dispose();
                this.connection.Dispose();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        [TestMethod]
        public void StudentRepository_Empty_Test()
        {
            var studentRepository = new StudentRepository(this.schoolContext);

            Assert.IsFalse(studentRepository.AllStudents.Any());
        }
    }
}
