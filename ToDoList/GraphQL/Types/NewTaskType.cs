using GraphQL.Types;
using NewTaskDataType = ToDoListData.Models.NewTask;

namespace ToDoList.GraphQL.Types
{
    public class NewTaskType : ObjectGraphType<NewTaskDataType>
    {
        public NewTaskType()
        {

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("TaskName")
                .Resolve(ctx => ctx.Source.TaskName);

            Field<StringGraphType, string?>()
                .Name("TaskText")
                .Resolve(ctx => ctx.Source.TaskText);

            Field<DateTimeGraphType, DateTime?>()
                .Name("DeadLine")
                .Resolve(ctx => ctx.Source.DeadLine);

            Field<IntGraphType, int?>()
                .Name("Category")
                .Resolve(ctx => ctx.Source.Category);
        }
    }
}
