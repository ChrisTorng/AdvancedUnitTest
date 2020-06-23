﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolDatabase.Tests
{
    [TestClass]
    public class MockStudentRepositoryTests
    {
        [TestMethod]
        public void StudentRepository_EmptyAllStudents_Test()
        {
            using var schoolContext = new MockSchoolDatabase();

            var studentRepository = new StudentRepository(schoolContext);

            Assert.IsFalse(studentRepository.AllStudents.Any());
        }

        [TestMethod]
        public void StudentRepository_AllStudentsQuery_Test()
        {
            using var schoolContext = new MockSchoolDatabase(new Student[]
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
        public void StudentRepository_CurrentStudentsStartDate_Test()
        {
            Assert.AreEqual(new DateTime(2017, 8, 1), StudentRepository.CurrentStudentsStartDate);
        }
    }
}
