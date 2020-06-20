using System;
using System.Linq;

namespace SchoolDatabase
{
    public class StudentRepository
    {
        private readonly ISchoolDatabase schoolDatabase;

        public StudentRepository(ISchoolDatabase schoolDatabase)
        {
            this.schoolDatabase = schoolDatabase;
        }

        public IQueryable<Student> AllStudents =>
            this.schoolDatabase.Students;

        public IQueryable<Student> CurrentStudents
        {
            get
            {
                var isNextYear = DateTime.Now.Month < 8;
                var thisYearStartDate = isNextYear
                    ? new DateTime(DateTime.Now.Year - 1, 8, 1)
                    : new DateTime(DateTime.Now.Year, 8, 1);
                var currentStudentsYearStartDate = thisYearStartDate.AddYears(-2);

                return this.schoolDatabase.Students.Where(s =>
                    s.EnrollmentDate >= currentStudentsYearStartDate);
            }
        }
    }
}
