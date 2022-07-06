using GraphQL.Types;
using ToDoListData.Providers;
using DataTaskType = ToDoListData.Models.Task;

namespace ToDoList.GraphQL.Task.Inputs
{
    public class UpdateTaskInputType : InputObjectGraphType<UpdateTaskInput>
    {
        private IDataProvider repository;
        public UpdateTaskInputType(IDataProvider repository)
        {
            this.repository = repository;

            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("Id")
                .Resolve(ctx => ctx.Source.Id);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("TaskName")
                .Resolve(ctx => ctx.Source.TaskName);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("TaskText")
                .Resolve(ctx => ctx.Source.TaskText);

            Field<DateTimeGraphType, DateTime?>()
                .Name("DeadLine")
                .Resolve(ctx => ctx.Source.DeadLine);

            Field<BooleanGraphType, bool>()
                .Name("IsCompleted")
                .Resolve(ctx => ctx.Source.IsCompleted);

            Field<DateTimeGraphType, DateTime?>()
                .Name("FinishDate")
                .Resolve(ctx => ctx.Source.FinishDate);

            Field<NonNullGraphType<IntGraphType>, int?>()
                .Name("Category")
                .Resolve(ctx => ctx.Source.Category);

        }
    }
}