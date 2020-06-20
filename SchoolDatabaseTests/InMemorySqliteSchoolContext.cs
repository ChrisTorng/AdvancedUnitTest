using System;
using System.Data.Common;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace SchoolDatabase.Tests
{
    internal class InMemorySqliteSchoolContext : ISchoolDatabase
    {
        private readonly DbConnection connection;
        private readonly SchoolContext schoolContext;
        private bool disposed = false;

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        public InMemorySqliteSchoolContext()
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

        public IQueryable<Student> Students =>
            this.schoolContext.Students;
    }
}
