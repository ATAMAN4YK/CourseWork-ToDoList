namespace ToDoListData.Models
{
    public class NewTask
    {
        public string TaskName { get; set; }
        public string? TaskText { get; set; }
        public DateTime? DeadLine { get; set; }
        public int? Category { get; set; }
    }
}
