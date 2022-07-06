using GraphQL.Types;
using DataTaskType = ToDoListData.Models.Task;

namespace ToDoList.GraphQL.Types
{
    public class TaskType : ObjectGraphType<DataTaskType>
    {
        public TaskType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("Id")
                .Resolve(ctx => ctx.Source.Id);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("TaskName")
                .Resolve(ctx => ctx.Source.TaskName);

            Field<StringGraphType, string?>()
                .Name("TaskText")
                .Resolve(ctx => ctx.Source.TaskText);

            Field<DateTimeGraphType, DateTime?>()
                .Name("DeadLine")
                .Resolve(ctx => ctx.Source.DeadLine);

            Field<DateTimeGraphType, DateTime?>()
                .Name("FinishDate")
                .Resolve(ctx => ctx.Source.FinishDate);

            Field<IntGraphType, int?>()
                .Name("Category")
                .Resolve(ctx => ctx.Source.Category);

            Field<NonNullGraphType<BooleanGraphType>, bool>()
                .Name("IsCompleted")
                .Resolve(ctx => ctx.Source.IsCompleted);
        }
    }
}
