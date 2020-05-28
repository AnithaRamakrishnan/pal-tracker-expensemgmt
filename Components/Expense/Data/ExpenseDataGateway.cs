using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Expense.Data
{
   public class ExpenseDataGateway : IExpenseDataGateway
    {
        private readonly ExpenseContext _context;

        public ExpenseDataGateway(ExpenseContext context)
        {
            _context = context;
        }

        public ExpenseRecord Create(long userId, long projectId, string name, string expenseType, decimal amount, DateTime dates)
        {
            var recordToCreate = new ExpenseRecord(userId, projectId, name, expenseType, amount, dates);

            _context.ExpenseRecords.Add(recordToCreate);
            _context.SaveChanges();

            return recordToCreate;
        }

        public List<ExpenseRecord> FindBy(long projectId) => _context.ExpenseRecords
            .AsNoTracking()
            .Where(s => s.ProjectId == projectId)
            .ToList();
    }
}
