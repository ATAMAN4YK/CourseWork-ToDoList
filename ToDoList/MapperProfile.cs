using ToDoList.GraphQL.Category;
using ToDoList.GraphQL.Task;
using ToDoList.GraphQL.Task.Inputs;
using AutoMapper;
using NewTaskDataType = ToDoListData.Models.NewTask;
using DataTaskType = ToDoListData.Models.Task;

namespace ToDoList
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<NewTaskDataType, NewTaskInput>().ReverseMap();
            CreateMap<NewTaskDataType, UpdateTaskInput>().ReverseMap();
            CreateMap<UpdateTaskInput, DataTaskType>().ReverseMap();
            CreateMap<UpdateTaskInputType, UpdateTaskInput>().ReverseMap();

/*            CreateMap<CategoryEntity, NewCategoryInput>().ReverseMap();
            CreateMap<CategoryEntity, UpdateCategoryInput>().ReverseMap();*/
        }
    }
}