using ToDoListData.Models;
using Models = ToDoListData.Models;


namespace ToDoList.ViewModels {

    public class TasksListViewModel
    {
        public List<Models.Task> CompletedTasks;
        public List<Models.Task> NotCompletedTasks;
        public List<Category> Categories;
        public int SelectedDB = 0;
    }
}