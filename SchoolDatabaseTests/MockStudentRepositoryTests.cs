﻿using System;
using System.Collections.Generic;
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
        [DataRow(2020, 7, 31, 2017, 8, 1)]
        [DataRow(2020, 8, 1, 2018, 8, 1)]
        [DataRow(2021, 8, 1, 2019, 8, 1)]
        public void StudentRepository_CurrentStudentsStartDate_Test(
            int inputYear, int inputMonth, int inputDay,
            int expectedYear, int expectedMonth, int expectedDay)
        {
            using var schoolContext = new MockSchoolDatabase();
            var dateTime = new MockDateTime(new DateTime(inputYear, inputMonth, inputDay));

            var studentRepository = new StudentRepository(schoolContext, dateTime);
            Assert.AreEqual(new DateTime(expectedYear, expectedMonth, expectedDay),
                studentRepository.CurrentStudentsStartDate);
        }

        [TestMethod]
        [DynamicData(nameof(CurrentStudentsTestData), DynamicDataSourceType.Method)]
        public void StudentRepository_CurrentStudents_Test(DateTime now, int expectedLength)
        {
            var allStudents = new Student[]
            {
                new Student
                {
                    FirstMidName = "a",
                    LastName = "b",
                    EnrollmentDate = new DateTime(2018, 8, 1),
                },
                new Student
                {
                    FirstMidName = "c",
                    LastName = "d",
                    EnrollmentDate = new DateTime(2018, 7, 31),
                },
            };

            using var schoolContext = new MockSchoolDatabase(allStudents);
            var dateTime = new MockDateTime(now);
            var studentRepository = new StudentRepository(schoolContext, dateTime);
            CollectionAssert.AreEqual(allStudents.Take(expectedLength).ToArray(),
                studentRepository.CurrentStudents.ToArray());
        }

        private static IEnumerable<object[]> CurrentStudentsTestData()
        {
            yield return new object[] { new DateTime(2020, 7, 31), 2 };
            yield return new object[] { new DateTime(2020, 8, 1), 1 };
            yield return new object[] { new DateTime(2021, 8, 1), 0 };
        }
    }
}
