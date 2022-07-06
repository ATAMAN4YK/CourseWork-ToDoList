using GraphQL.Types;
using CategoryDataModel = ToDoListData.Models.Category;

namespace ToDoList.GraphQL.Category.Inputs
{
    public class UpdateCategoryInputType : InputObjectGraphType<CategoryDataModel>
    {
        public UpdateCategoryInputType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("Id")
                .Resolve(ctx => ctx.Source.Id);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Name")
                .Resolve(ctx => ctx.Source.Name);

            Field<StringGraphType, string?>()
                .Name("Description")
                .Resolve(ctx => ctx.Source.Description);
        }
    }
}