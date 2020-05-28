using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestSupport.Data;
using Expense.Data;
using Xunit;

namespace ExpenseTests.Data
{
    [Collection("Timesheets")]
    public class ExpenseDataGatewayTest
    {
        private static readonly TestDatabaseSupport Support =
            new TestDatabaseSupport(TestDatabaseSupport.ExpenseManagementConnectionString);

        private static readonly DbContextOptions<ExpenseContext> DbContextOptions =
            new DbContextOptionsBuilder<ExpenseContext>().UseMySql(TestDatabaseSupport.ExpenseManagementConnectionString)
                .Options;

        public ExpenseDataGatewayTest()
        {
            Support.TruncateAllTables();
        }

        [Fact]
        public void TestCreate()
        {
            var gateway = new ExpenseDataGateway(new ExpenseContext(DbContextOptions));
            gateway.Create(12, 22, "Raj", "tour", 20, DateTime.Now);

            // todo...
            var projectIds = Support.QuerySql("select project_id from expense");

            Assert.Equal(22L, projectIds[0]["project_id"]);
        }

        [Fact]
        public void TestFind()
        {
            Support.ExecSql(@"insert into expense (id, project_id, user_id, date, name, exp_type, amount) 
            values (2346, 22, 12, now(), 'Raj','tour', 20);");

            var gateway = new ExpenseDataGateway(new ExpenseContext(DbContextOptions));
            var list = gateway.FindBy(12);

            // todo...
            var actual = list.First();
            Assert.Equal(2346, actual.Id);
            Assert.Equal(22, actual.ProjectId);
            Assert.Equal(12, actual.UserId);
        }
    }
}