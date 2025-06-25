using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesTodoList.Exceptions;
using RazorPagesTodoList.Services;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesTodoList.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ITaskService _taskService;

        public IndexModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public bool ShouldShowNewTaskForm { get; set; } = false;

        [BindProperty(Name = "error", SupportsGet = true)]
        public string? ErrorMessage { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "������ ����������� ������ ����� ��������")]
        [MinLength(3, ErrorMessage = "����������� ������ �������� ������ - 3 �������")]
        [MaxLength(32, ErrorMessage = "������������ ������ �������� ������ - 32 �������")]
        public required string Name { get; set; }

        [BindProperty]
        [StringLength(256, ErrorMessage = "������������ ������ �������� ������ - 256 ��������")]
        public string? Description { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ShouldShowNewTaskForm = true;
                return Page();
            }

            _taskService.CreateTask(Name, Description);

            return RedirectToPage("/Index");
        }

        public IActionResult OnGetChangeTaskStatus(int taskId)
        {
            try
            {
                _taskService.ChangeTaskStatus(taskId);
            }
            catch (TaskNotFoundException)
            {
                return RedirectToPage("/Index", new { error = "������������ ������������� ������" });
            }

            return RedirectToPage("/Index");
        }

        public IActionResult OnGetDeleteTask(int taskId)
        {
            _taskService.DeleteTask(taskId);
            return RedirectToPage("/Index");
        }
    }
}
