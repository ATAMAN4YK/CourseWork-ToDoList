namespace ToDoList.GraphQL.Task.Inputs
{
    public class NewTaskInput
    {
        public string TaskName { get; set; }
        public string? TaskText { get; set; }
        public DateTime? DeadLine { get; set; }
        public int? Category { get; set; }
    }
}