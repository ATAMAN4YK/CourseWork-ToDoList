using ToDoList.GraphQL.Task.Inputs;
using ToDoList.GraphQL.Types;
using AutoMapper;
using NewTaskDataType = ToDoListData.Models.NewTask;
using DataTaskType = ToDoListData.Models.Task;
using GraphQL;
using GraphQL.Types;
using ToDoListData.Providers;

namespace ToDoList.GraphQL.Task
{
    public class TasksMutations : ObjectGraphType
    {
        private IDataProvider repository;
        public TasksMutations(IDataProvider repository, IMapper mapper)
        {
            this.repository = repository;

            Field<NewTaskType, NewTaskDataType>()
                .Name("Create")
                .Argument<NonNullGraphType<NewTaskInputType>, NewTaskInput>("NewTask", "New task arguments")
                .Resolve(ctx =>
                {
                    var input = ctx.GetArgument<NewTaskInput>("NewTask");
                    var task = mapper.Map<NewTaskDataType>(input);
                    repository.AddTask(task);
                    return task;
                });

            Field<TaskType, DataTaskType>()
                 .Name("Update")
                 .Argument<NonNullGraphType<UpdateTaskInputType>, UpdateTaskInput>("UpdateTask", "Task to update")
                 .Resolve(ctx =>
                 {
                     var input = ctx.GetArgument<UpdateTaskInput>("UpdateTask");
                     var task = mapper.Map<DataTaskType>(input);
                     repository.UpdateTask(task);
                     return task;
                 });

            Field<TaskType, DataTaskType>()
                .Name("Remove")
                .Argument<NonNullGraphType<IntGraphType>, int>("Id", "Task id to remove")
                .Resolve(context =>
                {
                    int id = context.GetArgument<int>("Id");
                    var task = repository.GetTaskById(id);
                    repository.DeleteTask(task);
                    return task;
                });
        }
    }
}