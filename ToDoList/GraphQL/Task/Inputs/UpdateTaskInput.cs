namespace ToDoList.GraphQL.Task.Inputs
{
    public class UpdateTaskInput
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string? TaskText { get; set; }
        public DateTime? DeadLine { get; set; }
        public DateTime? FinishDate { get; set; }
        public int? Category { get; set; }
        public bool IsCompleted { get; set; }
    }
}
