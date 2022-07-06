using ToDoListData.Providers;
using TaskModel = ToDoListData.Models.Task;
using GraphQL.Types;
using ToDoList.GraphQL.Types;
using GraphQL;

namespace ToDoList.GraphQL.Task
{
    public class TaskQueries : ObjectGraphType
    {
        private readonly IDataProvider repository;
        public TaskQueries(IDataProvider repository)
        {
            this.repository = repository;

            Field<NonNullGraphType<ListGraphType<TaskType>>, List<TaskModel>>()
                .Name("GetAllTasks")
                .Resolve(ctx => // about ctx i don't know
                {
                    return repository.GetTasksList();
                });

            Field<NonNullGraphType<TaskType>, TaskModel>()
                .Name("GetTaskById")
                .Argument<NonNullGraphType<IntGraphType>, int>("id", "Task id") // why to id s?
                .Resolve(ctx =>
                {
                    return repository.GetTaskById(ctx.GetArgument<int>("id"));
                });
        }
    }
}
