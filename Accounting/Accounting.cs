using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Accounting
{
    class Accounting
    {
        public IBudgetRepo Repo { get; set; }

        public decimal QueryBudget(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate) return 0m;

            var budget = 0m;

            var currentDate = new DateTime(startDate.Year, startDate.Month, 1);
            
            while (currentDate <= endDate)
            {
                if (IsSameYearMonth(startDate, currentDate))
                {
                    if (IsSameYearMonth(endDate, currentDate))
                    {
                        budget += BudgetOfMonth(startDate, endDate.Subtract(startDate).Days + 1);
                    }
                    else
                    {
                        budget += BudgetOfMonth(startDate,
                            DateTime.DaysInMonth(startDate.Year, startDate.Month) - startDate.Day + 1);

                    }
                }
                else if (IsSameYearMonth(currentDate, endDate))
                {
                    budget += BudgetOfMonth(endDate, endDate.Day);
                }
                else
                {
                    budget += BudgetOfMonth(currentDate, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
                }

                currentDate = currentDate.AddMonths(1);
            }

            return budget;
        }

        private static bool IsSameYearMonth(DateTime startDate, DateTime currentDate)
        {
            return currentDate.Year == startDate.Year &&
                   currentDate.Month == startDate.Month;
        }

        private decimal BudgetOfMonth(DateTime startDate, int days)
        {


            var daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);

            var budget = Repo.GetAll().FirstOrDefault(model => model.YearMonth == startDate.ToString("yyyyMM"));
            if (budget != null) return (decimal)budget.Amount / daysInMonth * days;
            return 0;
        }

        
    }
}
