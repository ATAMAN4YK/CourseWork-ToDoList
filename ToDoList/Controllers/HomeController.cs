using Microsoft.AspNetCore.Mvc;
using ToDoList.ViewModels;
using ToDoListData.Models;
using Models = ToDoListData.Models;
using ToDoListData.Providers;
using ToDoList.DataBaseProvider;
using MSSQLDataBase;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        static IDataProvider DataBase;
        public HomeController(IDataProvider dataBase)
        {
            DataBase = dataBase;
        }
        public IActionResult ChangeDataBase(int newSelectedDB)
        {
            DataBaseStatus.ChoosedDataBase = newSelectedDB;
            return RedirectToAction("TasksList");
        }
        public IActionResult TasksList()
        {
            var taskList = DataBase.GetTasksList();
            var viewModel = new TasksListViewModel
            {
                CompletedTasks = Enumerable.Reverse(
                        taskList.Where(item => item.IsCompleted).ToList()
                        .OrderBy(task => task.FinishDate).ToList())
                    .ToList(),

                NotCompletedTasks = taskList.Where(item => !item.IsCompleted).ToList()
                    .OrderBy(task => task.DeadLine).ToList(),

                Categories = DataBase.GetCategoryList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateTask(NewTask task)
        {
            DataBase.AddTask(task);
            return View("SuccessfulCreate");
        }

        [HttpPost]
        public IActionResult DeleteTask(int TaskId)
        {
            DataBase.DeleteTask(DataBase.GetTaskById(TaskId));
            return RedirectToAction("TasksList");
        }

        [HttpPost]
        public RedirectToActionResult DoneTask(int TaskId)
        {
            var task = DataBase.GetTaskById(TaskId);
            task.IsCompleted = task.IsCompleted ? false : true;
            task.FinishDate = DateTime.Now;
            DataBase.UpdateTask(task);
            return RedirectToAction("TasksList");
        }

        [HttpPost]
        public RedirectToActionResult NotDoneTask(int TaskId)
        {
            var task = DataBase.GetTaskById(TaskId);
            task.IsCompleted = task.IsCompleted ? false : true;
            task.FinishDate = null;
            DataBase.UpdateTask(task);
            return RedirectToAction("TasksList");
        }

        public IActionResult CategoriesList()
        {
            var categories = DataBase.GetCategoryList();
            return View(categories);
        }

        [HttpPost]
        public IActionResult CreateCategory(NewCategory newCategory)
        {
            DataBase.AddCategory(newCategory);
            return View("SuccessfulCreate");
        }
        public IActionResult DeleteCategory(int id)
        {
            DataBase.DeleteCategory(DataBase.GetCategoryById(id));
            return RedirectToAction("CategoriesList");
        }
        public IActionResult EditTask(int TaskId)
        {
            var viewModel = new EditTaskViewModel
            {
                Task = DataBase.GetTaskById(TaskId),
                Categories = DataBase.GetCategoryList()
            };
            return View("EditTask", viewModel);
        }

        [HttpPost]
        public IActionResult UpdateEditedTask(Models.Task editedTask)
        {
            DataBase.UpdateTask(editedTask);
            return View("SuccessfulCreate");
        }

        public IActionResult EditCategory(int CategoryId)
        {
            return View("EditCategory", DataBase.GetCategoryById(CategoryId));
        }

        [HttpPost]
        public IActionResult UpdateEditedCategory(Category editedCategory)
        {
            DataBase.UpdateCategory(editedCategory);
            return View("SuccessfulCreate");
        }
    }
}