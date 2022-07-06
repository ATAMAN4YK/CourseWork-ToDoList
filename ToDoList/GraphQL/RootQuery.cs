using ToDoList.GraphQL.Task;
using ToDoList.GraphQL.Category;
using GraphQL.Types;

namespace ToDoList.GraphQL
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            Field<TaskQueries>()
                .Name("Tasks")
                .Resolve(_ => new { }); // why _ and why => new {}

            Field<CategoryQueries>()
                .Name("Categories")
                .Resolve(_ => new { }); // why _ and why => new {} 
        }
    }
}