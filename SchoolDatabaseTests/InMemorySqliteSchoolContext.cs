using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace SchoolDatabase.Tests
{
    internal class InMemorySqliteSchoolContext : IBaseSchoolDatabase
    {
        private readonly DbConnection connection;
        private readonly SchoolContext schoolContext;
        private bool disposed = false;

        public IQueryable<Student> Students =>
            this.schoolContext.Students;

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
#pragma warning disable CA2000 // Dispose objects before losing scope
                .UseSqlite(this.connection)
#pragma warning restore CA2000 // Dispose objects before losing scope
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

        public void AddStudents(IEnumerable<Student> students)
        {
            this.schoolContext.AddRange(students);
            this.schoolContext.SaveChanges();
        }
    }
}
