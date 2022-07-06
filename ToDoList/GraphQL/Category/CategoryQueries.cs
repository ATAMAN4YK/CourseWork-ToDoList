using ToDoListData.Providers;
using CategoryModel = ToDoListData.Models.Category;
using GraphQL.Types;
using ToDoList.GraphQL.Types;
using GraphQL;

namespace ToDoList.GraphQL.Category
{
    public class CategoryQueries : ObjectGraphType
    {
        private readonly IDataProvider repository;
        public CategoryQueries(IDataProvider repository)
        {
            this.repository = repository;

            Field<NonNullGraphType<ListGraphType<CategoryType>>, List<CategoryModel>>()
                .Name("GetAllCategories")
                .Resolve(ctx => // about ctx i don't know
                {
                    return this.repository.GetCategoryList();
                });

            Field<NonNullGraphType<CategoryType>, CategoryModel>()
                .Name("GetCategoryById")
                .Argument<NonNullGraphType<IntGraphType>, int>("id", "Category id") // why two id s?
                .Resolve(ctx =>
                {
                    return this.repository.GetCategoryById(ctx.GetArgument<int>("id"));
                });
        }
    }
}
