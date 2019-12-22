using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Accounting
{
    public class Tests
    {
        private Accounting _accounting;

        [SetUp]
        public void Setup()
        {
            _accounting = new Accounting();
            var fakeRepo = new FakeRepo();
            _accounting.Repo = fakeRepo;
        }

        [Test]
        public void test_Full_Month()
        {
            var startDate = DateTime.Parse("2019/12/1");
            var endDate = DateTime.Parse("2019/12/31");

            var fakeRepo = new FakeRepo {Budgets = new List<Budget>
            {
                new Budget {YearMonth = "201909", Amount = 30 },
                new Budget {YearMonth = "201911", Amount = 300 },
                new Budget {YearMonth = "201912", Amount = 3100 },
                new Budget {YearMonth = "202001", Amount = 31000 },
                new Budget {YearMonth = "202012", Amount = 310000 },
            }};

            _accounting.Repo = fakeRepo;

            var actual = _accounting.QueryBudget(startDate, endDate);

            Assert.AreEqual(3100, actual);
        }

        [Test]
        public void test_PartialMonth()
        {
            var startDate = DateTime.Parse("2019/12/01");
            var endDate = DateTime.Parse("2019/12/10");
            var actual = _accounting.QueryBudget(startDate, endDate);
            Assert.AreEqual(1000,actual);
        }
        [Test]
        public void test_Cross_Months()
        {
            var startDate = DateTime.Parse("2019/11/30");
            var endDate = DateTime.Parse("2019/12/1");
            var actual = _accounting.QueryBudget(startDate, endDate);
            Assert.AreEqual(110,actual);
        }
        [Test]
        public void test_Cross_Years()
        {
            var startDate = DateTime.Parse("2019/12/01");
            var endDate = DateTime.Parse("2020/01/31");
            var actual = _accounting.QueryBudget(startDate, endDate);
            Assert.AreEqual(34100, actual);
        }

        [Test]
        public void test_Invalid_Input()
        {
            var startDate = DateTime.Parse("2019/12/31");
            var endDate = DateTime.Parse("2019/12/01");
            var actual = _accounting.QueryBudget(startDate, endDate);
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void test_Same_day()
        {
            var startDate = DateTime.Parse("2019/12/01");
            var endDate = DateTime.Parse("2019/12/01");
            var actual = _accounting.QueryBudget(startDate, endDate);
            Assert.AreEqual(100, actual);
        }
        [Test]
        public void test_No_data()
        {
            var startDate = DateTime.Parse("2020/08/01");
            var endDate = DateTime.Parse("2020/08/31");
            var actual = _accounting.QueryBudget(startDate, endDate);
            Assert.AreEqual(0, actual);
        }[Test]
        public void test_Contain_No_Data_Multi_Months()
        {
            var startDate = DateTime.Parse("2019/09/01");
            var endDate = DateTime.Parse("2019/11/30");
            var actual = _accounting.QueryBudget(startDate, endDate);

            Assert.AreEqual(330, actual);
        }
    }
}