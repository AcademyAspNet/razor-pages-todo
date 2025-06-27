using RazorPagesTodoList.Models;

namespace RazorPagesTodoList.Repositories.Implementations
{
    public class TaskRepository : Repository<UserTask>
    {
        protected override string GetFilePath()
        {
            return "tasks.json";
        }
    }
}
