using System.Collections.Generic;

namespace SchoolDatabase.Tests
{
    internal interface IBaseSchoolDatabase : ISchoolDatabase
    {
        void AddStudents(IEnumerable<Student> students);
    }
}
