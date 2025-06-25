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
        private readonly ITaskRepository _taskRepository;

        public EditTaskModel(ITaskService taskService, ITaskRepository taskRepository)
        {
            _taskService = taskService;
            _taskRepository = taskRepository;
        }

        [BindProperty(Name = "id", SupportsGet = true)]
        public int TaskId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "������ ����������� ������ ����� ��������")]
        [MinLength(3, ErrorMessage = "����������� ������ �������� ������ - 3 �������")]
        [MaxLength(32, ErrorMessage = "������������ ������ �������� ������ - 32 �������")]
        public required string Name { get; set; }

        [BindProperty]
        [StringLength(256, ErrorMessage = "������������ ������ �������� ������ - 256 ��������")]
        public string? Description { get; set; }

        [BindProperty]
        public bool IsDone { get; set; }

        [BindProperty]
        public required DateTime CreatedAt { get; set; }

        public IActionResult OnGet()
        {
            UserTask? task = _taskService.GetTaskById(TaskId);

            if (task is null)
                return RedirectToPage("/Index", new { error = "������������ ������������� ������" });

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
                return RedirectToPage("/Index", new { error = "������������ ������������� ������" });

            task.Name = Name;
            task.Description = Description;
            task.IsDone = IsDone;
            task.CreatedAt = CreatedAt;

            _taskRepository.SaveChanges();

            return RedirectToPage("/Index");
        }
    }
}
