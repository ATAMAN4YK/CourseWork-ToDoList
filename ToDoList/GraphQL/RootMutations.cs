using ToDoList.GraphQL.Task;
using ToDoList.GraphQL.Category;
using GraphQL.Types;

namespace ToDoList.GraphQL
{
    public class RootMutations : ObjectGraphType
    {
        public RootMutations()
        {
            Field<TasksMutations>()
                .Name("Task")
                .Resolve(_ => new { });

            Field<CategoriesMutations>()
                .Name("Categories")
                .Resolve(_ => new { });
        }
    }
}