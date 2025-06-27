namespace RazorPagesTodoList.Models
{
    public class UserTask : Entity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsDone { get; set; } = false;
    }
}
