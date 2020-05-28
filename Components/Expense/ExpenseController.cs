using System.Linq;
using Expense.Data;
using Expense.ProjectClient;
using Microsoft.AspNetCore.Mvc;

namespace Expense
{
    [Route("expense"), Produces("application/json")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseDataGateway _gateway;
        private readonly IProjectClient _client;

        public ExpenseController(IExpenseDataGateway gateway, IProjectClient client)
        {
            _gateway = gateway;
            _client = client;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int projectId)
        {
            var records = _gateway.FindBy(projectId);
            var list = records.Select(record => new ExpenseInfo(record.Id, record.UserId, record.ProjectId, record.Name, record.ExpenseType, record.Amount, record.Dates, "Expense info"))
                .ToList();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ExpenseInfo info)
        {
            if (!ProjectIsActive(info.ProjectId)) return new StatusCodeResult(304);

            var record = _gateway.Create(info.UserId,info.ProjectId, info.Name,info.ExpenseType,info.Amount,info.Dates);
            var value = new ExpenseInfo(record.Id,record.UserId, record.ProjectId, record.Name, record.ExpenseType, record.Amount, record.Dates, "Expense info");
            return Created($"expense/{value.Id}", value);
        }

        private bool ProjectIsActive(long projectId)
        {
            var info = _client.Get(projectId);
            return info.Result?.Active ?? false;
        }
    }
}
