using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SchoolDatabase.Tests
{
    internal abstract class BaseSchoolDatabase : ISchoolDatabase
    {
        protected DbContextOptions<SchoolContext> ContextOptions { get; }

        private readonly SchoolContext schoolContext;
        private bool disposed = false;

        public BaseSchoolDatabase(DbContextOptions<SchoolContext> options)
        {
            this.ContextOptions = options;
            SchoolContext schoolContext = new SchoolContext(options);
            this.schoolContext = schoolContext;

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

        public IQueryable<Student> Students =>
            this.schoolContext.Students;
    }
}
