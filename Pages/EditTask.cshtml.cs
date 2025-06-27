using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesTodoList.Models;
using RazorPagesTodoList.Repositories;
using RazorPagesTodoList.Services;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesTodoList.Pages
{
    public class EditTaskModel : PageModel
    {
        private readonly ITaskService _taskService;
        private readonly IRepository<UserTask> _taskRepository;

        public EditTaskModel(ITaskService taskService, IRepository<UserTask> taskRepository)
        {
            _taskService = taskService;
            _taskRepository = taskRepository;
        }

        [BindProperty(Name = "id", SupportsGet = true)]
        public int TaskId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Задача обязательно должна иметь название")]
        [MinLength(3, ErrorMessage = "Минимальная длинна названия задачи - 3 символа")]
        [MaxLength(32, ErrorMessage = "Максимальная длинна названия задачи - 32 символа")]
        public required string Name { get; set; }

        [BindProperty]
        [StringLength(256, ErrorMessage = "Максимальная длинна описания задачи - 256 символов")]
        public string? Description { get; set; }

        [BindProperty]
        public bool IsDone { get; set; }

        [BindProperty]
        public required DateTime CreatedAt { get; set; }

        public IActionResult OnGet()
        {
            UserTask? task = _taskService.GetTaskById(TaskId);

            if (task is null)
                return RedirectToPage("/Index", new { error = "Некорректный идентификатор задачи" });

            Name = task.Name;
            Description = task.Description;
            IsDone = task.IsDone;
            CreatedAt = task.CreatedAt;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            UserTask? task = _taskService.GetTaskById(TaskId);

            if (task is null)
                return RedirectToPage("/Index", new { error = "Некорректный идентификатор задачи" });

            task.Name = Name;
            task.Description = Description;
            task.IsDone = IsDone;
            task.CreatedAt = CreatedAt;

            _taskRepository.SaveChanges();

            return RedirectToPage("/Index");
        }
    }
}
