using GraphQL.Types;
using NewCategoryDataModel = ToDoListData.Models.NewCategory;

namespace ToDoList.GraphQL.Category.Inputs
{
    public class NewCategoryInputType : InputObjectGraphType<NewCategoryDataModel>
    {
        public NewCategoryInputType()
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