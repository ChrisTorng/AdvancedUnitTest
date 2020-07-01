﻿////using System;
////using System.Collections.Generic;
////using System.Linq;
////using Microsoft.QualityTools.Testing.Fakes;
////using Microsoft.VisualStudio.TestTools.UnitTesting;
////using SchoolDatabase.Fakes;

////namespace SchoolDatabase.Tests
////{
////    [TestClass]
////    [TestCategory("SkipWhenLiveUnitTesting")]
////    public class FakesShimDefaultDateTimeStudentRepositoryTests
////    {
////        [TestMethod]
////        [DataRow(2020, 7, 31, 2017, 8, 1)]
////        [DataRow(2020, 8, 1, 2018, 8, 1)]
////        [DataRow(2021, 8, 1, 2019, 8, 1)]
////        public void StudentRepository_CurrentStudentsStartDate_Test(
////            int inputYear, int inputMonth, int inputDay,
////            int expectedYear, int expectedMonth, int expectedDay)
////        {
////            using var schoolContext = new MockSchoolDatabase();

////            using var shim = ShimsContext.Create();
////            ShimDefaultDateTime.AllInstances.NowGet =
////                d => new DateTime(inputYear, inputMonth, inputDay);

////            var dateTime = new DefaultDateTime();

////            var studentRepository = new StudentRepository(schoolContext, dateTime);
////            Assert.AreEqual(new DateTime(expectedYear, expectedMonth, expectedDay),
////                studentRepository.CurrentStudentsStartDate);
////        }

////        [TestMethod]
////        [DynamicData(nameof(CurrentStudentsTestData), DynamicDataSourceType.Method)]
////        public void StudentRepository_CurrentStudents_Test(DateTime now, int expectedLength)
////        {
////            var allStudents = new Student[]
////            {
////                new Student
////                {
////                    FirstMidName = "a",
////                    LastName = "b",
////                    EnrollmentDate = new DateTime(2018, 8, 1),
////                },
////                new Student
////                {
////                    FirstMidName = "c",
////                    LastName = "d",
////                    EnrollmentDate = new DateTime(2018, 7, 31),
////                },
////            };

////            using var schoolContext = new MockSchoolDatabase(allStudents);

////            using var shim = ShimsContext.Create();
////            ShimDefaultDateTime.AllInstances.NowGet = d => now;

////            var dateTime = new DefaultDateTime();

////            var studentRepository = new StudentRepository(schoolContext, dateTime);
////            CollectionAssert.AreEqual(allStudents.Take(expectedLength).ToArray(),
////                studentRepository.CurrentStudents.ToArray());
////        }

////        private static IEnumerable<object[]> CurrentStudentsTestData()
////        {
////            yield return new object[] { new DateTime(2020, 7, 31), 2 };
////            yield return new object[] { new DateTime(2020, 8, 1), 1 };
////            yield return new object[] { new DateTime(2021, 8, 1), 0 };
////        }
////    }
////}
