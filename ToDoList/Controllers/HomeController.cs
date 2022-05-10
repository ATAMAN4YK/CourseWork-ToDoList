using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MSSQLDataBase;
using XMLDataBase;
using ToDoList.ViewModels;
using ToDoListData.Models;
using Models = ToDoListData.Models;
using ToDoListData.Providers;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        static int selectedDB = 0;
        static IDataProvider DataBase = new MSDataProvider();
        public IActionResult ChangeDataBase(string dataBase)
        {
            if (dataBase == "MSDataBase")
            {
                DataBase = new MSDataProvider();
                selectedDB = 0;
            }
            else
            {
                DataBase = new XMLDataProvider();
                selectedDB = 1;
            }

            return RedirectToAction("TasksList");
        }
        public IActionResult TasksList()
        {
            var taskList = DataBase.GetTasksList();
            var viewModel = new TasksListViewModel
            {
                CompletedTasks = taskList.Where(item => item.IsCompleted).ToList(),
                NotCompletedTasks = taskList.Where(item => !item.IsCompleted).ToList(),
                Categories = DataBase.GetCategoryList(),
                SelectedDB = selectedDB
            };
            return View(viewModel);
        }
        public IActionResult CreateTask()
        {
            var categoryList = DataBase.GetCategoryList();
            return View(categoryList);
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
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(NewCategory newCategory)
        {
            DataBase.AddCategory(newCategory);
            return View("SuccessfulCreate");
        }
        public IActionResult CategoriesList()
        {
            var categories = DataBase.GetCategoryList();
            return View(categories);
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