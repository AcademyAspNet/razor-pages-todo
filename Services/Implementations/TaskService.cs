using RazorPagesTodoList.Exceptions;
using RazorPagesTodoList.Models;
using RazorPagesTodoList.Repositories;
using System.Security.Cryptography;

namespace RazorPagesTodoList.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<UserTask> _taskRepository;

        public TaskService(IRepository<UserTask> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public List<UserTask> GetTasks()
        {
            return _taskRepository.GetAll();
        }

        public void CreateTask(string name, string? description)
        {
            UserTask task = new UserTask()
            {
                Name = name,
                Description = description
            };

            _taskRepository.Create(task);
        }

        public UserTask? GetTaskById(int id)
        {
            return _taskRepository.GetById(id);
        }

        public void ChangeTaskStatus(int id)
        {
            UserTask? task = _taskRepository.GetById(id);

            if (task == null)
                throw new TaskNotFoundException(id);

            task.IsDone = !task.IsDone;
            _taskRepository.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            _taskRepository.Delete(id);
        }
    }
}
