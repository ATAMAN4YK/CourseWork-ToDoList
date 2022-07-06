using GraphQL.Types;
using NewCategoryDataModel = ToDoListData.Models.NewCategory;

namespace ToDoList.GraphQL.Types
{
    public class NewCategoryType : ObjectGraphType<NewCategoryDataModel>
    {
        public NewCategoryType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Name")
                .Resolve(ctx => ctx.Source.Name);

            Field<StringGraphType, string?>()
                .Name("Description")
                .Resolve(ctx => ctx.Source.Description);
        }
    }
}
