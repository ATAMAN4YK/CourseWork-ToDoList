using GraphQL.Types;
    
namespace ToDoList.GraphQL
{
    public class ToDoListSchema : Schema
    {
        public ToDoListSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<RootQuery>();
            Mutation = provider.GetRequiredService<RootMutations>();
        }
    }
}
