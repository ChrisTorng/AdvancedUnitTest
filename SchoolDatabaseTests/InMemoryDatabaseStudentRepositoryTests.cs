using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolDatabase.Tests
{
    [TestClass]
    public class InMemoryDatabaseStudentRepositoryTests
    {
        [TestMethod]
        public void InMemoryDatabase_StudentRepository_EmptyAllStudents_Test()
        {
            using var schoolContext = new InMemoryDatabaseSchoolContext();

            var studentRepository = new StudentRepository(schoolContext);
            Assert.IsFalse(studentRepository.AllStudents.Any());
        }

        [TestMethod]
        public void InMemoryDatabase_StudentRepository_AllStudentsQuery_Test()
        {
            using var schoolContext = new InMemoryDatabaseSchoolContext();

            schoolContext.AddStudents(new Student[]
                {
                    new Student
                    {
                        FirstMidName = "a",
                        LastName = "b",
                        EnrollmentDate = new DateTime(2, 2, 2),
                    },
                    new Student
                    {
                        FirstMidName = "c",
                        LastName = "d",
                        EnrollmentDate = new DateTime(3, 3, 3),
                    },
                });

            var studentRepository = new StudentRepository(schoolContext);
            Assert.IsTrue(studentRepository.AllStudents.Any());
            Assert.AreEqual(2, studentRepository.AllStudents.Count());

            var student = studentRepository.AllStudents
                .Where(s => s.FirstMidName == "c").Single();
            Assert.AreEqual("d", student.LastName);
            Assert.AreEqual(new DateTime(3, 3, 3), student.EnrollmentDate);
        }

        [TestMethod]
        public void InMemoryDatabase_Test()
        {
            using var schoolContext1 = new InMemoryDatabaseSchoolContext();
            using var schoolContext2 = new InMemoryDatabaseSchoolContext();
            schoolContext2.AddStudents(new Student[]
                {
                    new Student
                    {
                        FirstMidName = "a",
                        LastName = "b",
                        EnrollmentDate = new DateTime(2, 2, 2),
                    },
                });

            Assert.AreEqual(0, schoolContext1.Students.Count());
            Assert.AreEqual(1, schoolContext2.Students.Count());
        }
    }
}
