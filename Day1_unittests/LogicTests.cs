using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day1_unittests
{
    [TestClass]
    public class Day1UnitTest
    {
        [TestMethod]
        public void TestMethodFindConsecutiveIncrementsAll()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5};
            var count = Day1.Logic.GetConsecutiveIncrementsCount(list);
            Assert.IsTrue(count == 4);
        }

        [TestMethod]
        public void TestMethodFindConsecutiveIncrementsNone()
        {
            List<int> list = new List<int>() { 5, 4, 3, 2, 1};
            var count = Day1.Logic.GetConsecutiveIncrementsCount(list);
            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public void TestMethodFindConsecutiveIncrementsEmptyList()
        {
            List<int> list = new List<int>();
            var count = Day1.Logic.GetConsecutiveIncrementsCount(list);
            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public void TestMethodFindConsecutiveIncrementsListTooSmall()
        {
            List<int> list = new List<int>() { 1 };
            var count = Day1.Logic.GetConsecutiveIncrementsCount(list);
            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public void TestMethodFindIncrementingSectorsAll()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
            var count = Day1.Logic.GetIncrementingSectorsCount(list, 3);
            Assert.IsTrue(count == 2);
        }

        [TestMethod]
        public void TestMethodFindIncrementingSectorsNone()
        {
            List<int> list = new List<int>() { 5, 4, 3, 2, 1 };
            var count = Day1.Logic.GetIncrementingSectorsCount(list, 3);
            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public void TestMethodFindIncrementingSectorsEmptyList()
        {
            List<int> list = new List<int>();
            var count = Day1.Logic.GetIncrementingSectorsCount(list, 3);
            Assert.IsTrue(count == 0);
        }

        [TestMethod]
        public void TestMethodFindIncrementingSectorsListTooSmall()
        {
            List<int> list = new List<int>() { 1, 2 };
            var count = Day1.Logic.GetIncrementingSectorsCount(list, 3);
            Assert.IsTrue(count == 0);
        }

    }
}
