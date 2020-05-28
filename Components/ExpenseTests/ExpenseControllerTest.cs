using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Expense;
using Expense.Data;
using Expense.ProjectClient;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace ExpenseTests
{
    public class ExpenseControllerTest
    {
        private readonly Mock<IExpenseDataGateway> _gateway;
        private readonly Mock<IProjectClient> _client;
        private readonly ExpenseController _controller;

        public ExpenseControllerTest()
        {
            _gateway = new Mock<IExpenseDataGateway>();
            _client = new Mock<IProjectClient>();
            _controller = new ExpenseController(_gateway.Object, _client.Object);
        }

        [Fact]
        public void TestPost()
        {
            _gateway.Setup(g => g.Create(4765, 55432, "An epic Expense", "travel", 20, DateTime.Parse("2015-05-17")))
                .Returns(new ExpenseRecord(1234, 4765, 55432, "An epic Expense", "travel", 20, DateTime.Parse("2015-05-17")));
            _client.Setup(c => c.Get(55432)).Returns(Task.FromResult(new ProjectInfo(true)));

            var response = _controller.Post(new ExpenseInfo(-1, 4765, 55432,"An epic Expense", "travel", 20, DateTime.Parse("2015-05-17"), ""));
            var body = (ExpenseInfo)((ObjectResult)response).Value;

            Assert.IsType<CreatedResult>(response);

            Assert.Equal(1234, body.Id);
            Assert.Equal(4765, body.UserId);
            Assert.Equal(55432, body.ProjectId);
            Assert.Equal("An epic Expense", body.Name);
            Assert.Equal("travel", body.ExpenseType);
            Assert.Equal(20, body.Amount);
            Assert.Equal(DateTime.Parse("2015-05-17"), body.Dates);
        }

        [Fact]
        public void TestPost_InactiveProject()
        {
            _gateway.Setup(g => g.Create(55432, 4765, "An epic Expense", "travel", 20, DateTime.Parse("2015-05-17"))).Returns(new ExpenseRecord(55432, 4765, "An epic Expense","travel",20, DateTime.Parse("2015-05-17")));
            _client.Setup(c => c.Get(55432)).Returns(Task.FromResult(new ProjectInfo(false)));

            var response = _controller.Post(new ExpenseInfo(-1, 55432, 4765, "An epic Expense", "travel", 20, DateTime.Parse("2015-05-17"), ""));

            Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(304, ((StatusCodeResult)response).StatusCode);
        }

        [Fact]
        public void TestGet()
        {
            _gateway.Setup(g => g.FindBy(4765)).Returns(new List<ExpenseRecord>
            {
                new ExpenseRecord(1234, 4765, 55432, "An epic Expense","travel",20,DateTime.Parse("2015-05-17")),
                new ExpenseRecord(5678, 4765, 55433,"An even more epic Expense","travel",20,DateTime.Parse("2015-05-17"))
            });

            var response = _controller.Get(4765);
            var body = (List<ExpenseInfo>)((ObjectResult)response).Value;

            Assert.IsType<OkObjectResult>(response);

            Assert.Equal(2, ((List<ExpenseInfo>)((ObjectResult)response).Value).Count);

            Assert.Equal(1234, body[0].Id);
            Assert.Equal(55432, body[0].ProjectId);
            Assert.Equal(4765, body[0].UserId);
            Assert.Equal("An epic Expense", body[0].Name);
            Assert.Equal("travel", body[0].ExpenseType);
            Assert.Equal(20, body[0].Amount);
            Assert.Equal(DateTime.Parse("2015-05-17"), body[0].Dates);
            Assert.Equal("Expense info", body[0].Info);

            Assert.Equal(5678, body[1].Id);
            Assert.Equal(55433, body[1].ProjectId);
            Assert.Equal(4765, body[1].UserId);
            Assert.Equal("An even more epic Expense", body[1].Name);
            Assert.Equal("travel", body[1].ExpenseType);
            Assert.Equal(20, body[1].Amount);
            Assert.Equal(DateTime.Parse("2015-05-17"), body[1].Dates);
            Assert.Equal("Expense info", body[1].Info);
        }
    }
}
