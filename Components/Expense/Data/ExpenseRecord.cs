using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace Expense.Data
{
    [Table("expense")]
   public class ExpenseRecord
    {
        [Column("id")] public long Id { get; private set; }
        [Column("user_id")] public long UserId { get; private set; }
        [Column("project_id")] public long ProjectId { get; private set; }
        [Column("name")] public string Name { get; private set; }
        [Column("exp_typ")] public string ExpenseType { get; private set; }
        [Column("amount")] public decimal Amount { get; private set; }
        [Column("date")] public DateTime Dates { get; private set; }
        private ExpenseRecord()
        {
        }

        public ExpenseRecord(long userId, long projectId, string name, string expenseType, decimal amount, DateTime dates) : this(default(long), userId, projectId, name, expenseType, amount, dates)
        {
        }

        public ExpenseRecord(long id,long userId ,long projectId, string name,string expenseType, decimal amount, DateTime dates)
        {
            Id = id;
            UserId = userId;
            ProjectId = projectId;
            Name = name;
            ExpenseType = expenseType;
            Amount = amount;
            Dates = dates;
          

        }
    }
}
