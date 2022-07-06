using GraphQL.Types;

namespace ToDoList.GraphQL.Task.Inputs
{
    public class NewTaskInputType : InputObjectGraphType<NewTaskInput>
    {
        public NewTaskInputType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("TaskName")
                .Resolve(ctx => ctx.Source.TaskName);

            Field<NonNullGraphType<StringGraphType>, string?>()
                .Name("TaskText")
                .Resolve(ctx => ctx.Source.TaskText);

            Field<DateTimeGraphType, DateTime?>()
                .Name("DeadLine")
                .Resolve(ctx => ctx.Source.DeadLine);

            Field<NonNullGraphType<IntGraphType>, int?>()
                .Name("Category")
                .Resolve(ctx => ctx.Source.Category);
        }
    }
}