using System.Collections.Generic;

namespace Accounting
{
    class FakeRepo : IBudgetRepo
    {
        public List<Budget> Budgets { get; set; }

        public List<Budget> GetAll()
        {
            return Budgets ?? new List<Budget>()
            {
                new Budget {Amount = 30, YearMonth = "201909"},
                new Budget {Amount = 300, YearMonth = "201911"},
                new Budget {Amount = 3100, YearMonth = "201912"},
                new Budget {Amount = 31000, YearMonth = "202001"},
                new Budget {Amount = 310000, YearMonth = "202012"}
            };
        }
    }
}
