using ToDoList.GraphQL;
using GraphQL;
using NewTaskDataType = ToDoListData.Models.NewTask;
using DataTaskType = ToDoListData.Models.Task;
using CategoryDataModel = ToDoListData.Models.Category;
using NewCategoryDataModel = ToDoListData.Models.NewCategory;
using GraphQL.Server;
using ToDoList.GraphQL.Task;
using ToDoList.GraphQL.Types;
using ToDoList.GraphQL.Category;
using GraphQL.SystemTextJson;
using GraphQL.MicrosoftDI;
using GraphQL.NewtonsoftJson;
using ToDoList.GraphQL.Task.Inputs;
using ToDoList.GraphQL.Category.Inputs;

namespace ToDoList.Extensions
{
    public static class GraphQLServices
    {
        public static IServiceCollection AddGraphQLApi(this IServiceCollection services)
        {
            services.AddTransient<RootQuery>();
            services.AddTransient<RootMutations>();

            services.AddTransient<ToDoListSchema>();

            services.AddTransient<TaskQueries>();
            services.AddSingleton<TasksMutations>();

            services.AddTransient<TaskType>();
            services.AddTransient<CategoryType>();

            services.AddSingleton<NewTaskInputType>();
            services.AddSingleton<NewCategoryInputType>();
            services.AddSingleton<NewCategoryType>();

            services.AddSingleton<NewTaskDataType>();
            services.AddSingleton<NewTaskType>();
            services.AddSingleton<DataTaskType>();

            services.AddSingleton<CategoryDataModel>();
            services.AddSingleton<NewCategoryDataModel>();

            services.AddSingleton<UpdateTaskInputType>();
            services.AddSingleton<UpdateCategoryInputType>();

            services.AddTransient<CategoryQueries>();
            services.AddSingleton<CategoriesMutations>();
            
            services.AddCors();

            services.AddGraphQL(options => options
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
                .AddHttpMiddleware<ToDoListSchema>()
                .AddSystemTextJson()
                .AddNewtonsoftJson()
                .AddSchema<ToDoListSchema>()
                );

            return services;
        }
    }
}
