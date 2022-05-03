using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MSSQLDataBase;
using ToDoList.ViewModels;
using ToDoListData.Models;
using ToDoListData.Providers;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        IDataProvider DataBase = new MSDataProvider();
        public IActionResult TasksList()
        {
            var taskList = DataBase.GetTasksList();
            var viewModel = new TasksListViewModel
            {
                CompletedTasks = taskList.Where(item => item.IsCompleted).ToList(),
                NotCompletedTasks = taskList.Where(item => !item.IsCompleted).ToList(),
                Categories = DataBase.GetCategoryList()
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
    }
}