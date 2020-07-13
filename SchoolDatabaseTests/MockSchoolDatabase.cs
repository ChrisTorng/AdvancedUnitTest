using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolDatabase.Tests
{
    internal class MockSchoolDatabase : ISchoolDatabase
    {
        public MockSchoolDatabase(IEnumerable<Student>? students = null)
        {
            this.Students = students?.AsQueryable() ?? Array.Empty<Student>().AsQueryable();
        }

        public IQueryable<Student> Students { get; }

        public void Dispose()
        {
        }
    }
}
