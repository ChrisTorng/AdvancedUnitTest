using System;
using System.Linq;

namespace SchoolDatabase
{
    public class StudentRepository
    {
        private readonly ISchoolDatabase schoolDatabase;
        private readonly IDateTime dateTime;

        public StudentRepository(ISchoolDatabase schoolDatabase, IDateTime dateTime = null)
        {
            this.schoolDatabase = schoolDatabase;
            this.dateTime = dateTime ?? new DefaultDateTime();
        }

        public IQueryable<Student> AllStudents =>
            this.schoolDatabase.Students;

        public DateTime CurrentStudentsStartDate
        {
            get
            {
                var isNextYear = this.dateTime.Now.Month < 8;
                var thisYearStartDate = isNextYear
                    ? new DateTime(this.dateTime.Now.Year - 1, 8, 1)
                    : new DateTime(this.dateTime.Now.Year, 8, 1);
                return thisYearStartDate.AddYears(-2);
            }
        }

        public IQueryable<Student> CurrentStudents =>
            this.schoolDatabase.Students.Where(s =>
                s.EnrollmentDate >= this.CurrentStudentsStartDate);
    }
}
