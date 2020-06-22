using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolDatabase.Tests
{
    internal class MockSchoolDatabase : IBaseSchoolDatabase
    {
        public MockSchoolDatabase()
        {
            this.Students = Array.Empty<Student>().AsQueryable();
        }

        public IQueryable<Student> Students { get; private set; }

        public void AddStudents(IEnumerable<Student> students)
        {
            this.Students = students.AsQueryable();
        }

        public void Dispose()
        {
        }
    }
}
