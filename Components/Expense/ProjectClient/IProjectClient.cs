using System.Threading.Tasks;

namespace Expense.ProjectClient
{
    public interface IProjectClient
    {
        Task<ProjectInfo> Get(long projectId);
    }
}