using System;
using System.Collections.Generic;


namespace Expense.Data
{
    public interface IExpenseDataGateway
    {
        ExpenseRecord Create(long userId, long projectId, string name, string expenseType, decimal amount, DateTime dates);

        List<ExpenseRecord> FindBy(long projectId);
    }
}

