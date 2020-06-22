using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SchoolDatabase.Tests
{
    internal class InMemoryDatabaseSchoolContext : IBaseSchoolDatabase
    {
        private readonly SchoolContext schoolContext;
        private bool disposed = false;

        public IQueryable<Student> Students =>
            this.schoolContext.Students;

        public InMemoryDatabaseSchoolContext()
        {
            this.schoolContext = new SchoolContext(new DbContextOptionsBuilder<SchoolContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

            this.schoolContext.Database.EnsureCreated();
        }

        public void AddStudents(IEnumerable<Student> students)
        {
            this.schoolContext.AddRange(students);
            this.schoolContext.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.schoolContext.Dispose();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
