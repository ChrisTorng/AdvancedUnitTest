using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolDatabase.Tests
{
    [TestClass]
    public class StudentRepositoryTests
    {
        private static void StudentRepository_EmptyAllStudents_Test(IBaseSchoolDatabase schoolDatabase)
        {
            var studentRepository = new StudentRepository(schoolDatabase);
            Assert.IsFalse(studentRepository.AllStudents.Any());
        }

        [TestMethod]
        public void InMemorySqlite_StudentRepository_EmptyAllStudents_Test()
        {
            using var schoolContext = new InMemorySqliteSchoolContext();

            StudentRepository_EmptyAllStudents_Test(schoolContext);
        }

        [TestMethod]
        public void InMemoryDatabase_StudentRepository_EmptyAllStudents_Test()
        {
            using var schoolContext = new InMemoryDatabaseSchoolContext();

            StudentRepository_EmptyAllStudents_Test(schoolContext);
        }

        [TestMethod]
        public void MockSchoolDatabase_StudentRepository_EmptyAllStudents_Test()
        {
            using var schoolContext = new MockSchoolDatabase();

            StudentRepository_EmptyAllStudents_Test(schoolContext);
        }

        private static void StudentRepository_AllStudentsQuery_Test(IBaseSchoolDatabase schoolDatabase)
        {
            schoolDatabase.AddStudents(new Student[]
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

            var studentRepository = new StudentRepository(schoolDatabase);
            Assert.IsTrue(studentRepository.AllStudents.Any());
            Assert.AreEqual(2, studentRepository.AllStudents.Count());

            var student = studentRepository.AllStudents
                .Where(s => s.FirstMidName == "c").Single();
            Assert.AreEqual("d", student.LastName);
            Assert.AreEqual(new DateTime(3, 3, 3), student.EnrollmentDate);
        }

        [TestMethod]
        public void InMemorySqlite_StudentRepository_AllStudentsQuery_Test()
        {
            using var schoolContext = new InMemorySqliteSchoolContext();

            StudentRepository_AllStudentsQuery_Test(schoolContext);
        }

        [TestMethod]
        public void InMemoryDatabase_StudentRepository_AllStudentsQuery_Test()
        {
            using var schoolContext = new InMemoryDatabaseSchoolContext();

            StudentRepository_AllStudentsQuery_Test(schoolContext);
        }

        [TestMethod]
        public void MockSchoolDatabase_StudentRepository_AllStudentsQuery_Test()
        {
            using var schoolContext = new MockSchoolDatabase();

            StudentRepository_AllStudentsQuery_Test(schoolContext);
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
