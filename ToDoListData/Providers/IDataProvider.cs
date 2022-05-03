using ToDoListData.Models;

namespace ToDoListData.Providers
{
    public interface IDataProvider
    {
        List<Models.Task> GetTasksList();
        void AddTask(NewTask task);
        void DeleteTask(Models.Task task);
        void UpdateTask(Models.Task task);
        Models.Task GetTaskById(int id);

        List<Category> GetCategoryList();
        void AddCategory(NewCategory category);
        void DeleteCategory(Category category);
        Category GetCategoryById(int id);

    }
}
