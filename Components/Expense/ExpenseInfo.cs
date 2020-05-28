using System;

namespace Expense
{
    public struct ExpenseInfo
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ProjectId { get; set; }
        public string Name { get; set; }
        public string ExpenseType { get; set; }
        public decimal Amount { get;  set; }
        public DateTime Dates { get;  set; }
        public string Info { get; set; }


        public ExpenseInfo(long id, long userId, long projectId, string name, string expenseType, decimal amount, DateTime dates, string info)
        {
            Id = id;
            UserId = userId;
            ProjectId = projectId;
            Name = name;
            ExpenseType = expenseType;
            Amount = amount;
            Dates = dates;
            Info = info;
        }
    }
}