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

        public static DateTime CurrentStudentsStartDate
        {
            get
            {
                var isNextYear = DateTime.Now.Month < 8;
                var thisYearStartDate = isNextYear
                    ? new DateTime(DateTime.Now.Year - 1, 8, 1)
                    : new DateTime(DateTime.Now.Year, 8, 1);
                return thisYearStartDate.AddYears(-2);
            }
        }

        public IQueryable<Student> CurrentStudents =>
            this.schoolDatabase.Students.Where(s =>
                s.EnrollmentDate >= CurrentStudentsStartDate);
    }
}
