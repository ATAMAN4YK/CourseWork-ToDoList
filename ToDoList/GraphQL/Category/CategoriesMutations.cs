using AutoMapper;
using ToDoListData.Providers;
using GraphQL;
using GraphQL.Types;
using ToDoList.GraphQL.Types;
using ToDoList.GraphQL.Category.Inputs;
using CategoryDataModel = ToDoListData.Models.Category;
using NewCategoryDataModel = ToDoListData.Models.NewCategory;

namespace ToDoList.GraphQL.Category
{
    public class CategoriesMutations : ObjectGraphType
    {
        private IDataProvider repository;
        public CategoriesMutations(IDataProvider repository, IMapper mapper)
        {
            this.repository = repository;
            Field<NewCategoryType, NewCategoryDataModel>()
                .Name("Create")
                .Argument<NonNullGraphType<NewCategoryInputType>, NewCategoryDataModel>("NewCategory", "New category arguments")
                .Resolve(ctx =>
                {
                    var input = ctx.GetArgument<NewCategoryDataModel>("NewCategory");
                    var category = mapper.Map<NewCategoryDataModel>(input);
                    repository.AddCategory(category);
                    return category;
                });

            Field<CategoryType, CategoryDataModel>()
                 .Name("Update")
                 .Argument<NonNullGraphType<UpdateCategoryInputType>, CategoryDataModel>("UpdateCategory", "Category to update")
                 .Resolve(ctx =>
                 {
                     var input = ctx.GetArgument<CategoryDataModel>("UpdateCategory");
                     var category = mapper.Map<CategoryDataModel>(input);
                     repository.UpdateCategory(category);
                     return category;
                 });

            Field<CategoryType, CategoryDataModel>()
                .Name("Remove")
                .Argument<NonNullGraphType<IntGraphType>, int>("Id", "Category id to remove")
                .Resolve(context =>
                {
                    int id = context.GetArgument<int>("Id");
                    var category = repository.GetCategoryById(id);
                    repository.DeleteCategory(category);
                    return category;
                });
        }
    }
}