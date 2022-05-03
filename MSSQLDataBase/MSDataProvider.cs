using System.Data.SqlClient;
using ToDoListData.Models;
using ToDoListData.Providers;
using Models = ToDoListData.Models;
using Dapper;

namespace MSSQLDataBase
{
    public class MSDataProvider : IDataProvider
    {
        private readonly string SqlConnectionString = "Data Source=DESKTOP-SCDGN8K;Initial Catalog=todolistdb;Integrated Security=True";
        void IDataProvider.AddCategory(NewCategory category)
        {
            using (SqlConnection conn = new SqlConnection(SqlConnectionString))
            {
                conn.Open();

                conn.Execute(
                    "INSERT INTO Categories (Name, Description) " +
                    "VALUES (@Name, @Description)",
                    new NewCategory
                    {
                        Name = category.Name,
                        Description = category.Description,
                    });
            }
        }

        void IDataProvider.AddTask(NewTask task)
        {
            using (SqlConnection conn = new SqlConnection(SqlConnectionString))
            {
                conn.Open();

                conn.Execute(
                    "INSERT INTO TASKS (TaskName, TaskText, Deadline, Category) " +
                    "VALUES (@TaskName, @TaskText, @DeadLine, @Category)",
                    new NewTask
                    { 
                        TaskName = task.TaskName, 
                        TaskText = task.TaskText, 
                        DeadLine = task.DeadLine, 
                        Category = task.Category 
                    });
            }
        }

        void IDataProvider.DeleteCategory(Category category)
        {
            using (SqlConnection conn = new SqlConnection(SqlConnectionString))
            {
                conn.Open();

                conn.Execute(
                    "DELETE Categories WHERE Id = @Id",
                    new Category { Id = category.Id });
            }
        }

        void IDataProvider.DeleteTask(Models.Task task)
        {
            using (SqlConnection conn = new SqlConnection(SqlConnectionString))
            {
                conn.Open();

                conn.Execute(
                    "DELETE Tasks WHERE Id = @Id",
                    new Models.Task { Id = task.Id });
            }
        }

        List<Category> IDataProvider.GetCategoryList()
        {
            using (SqlConnection conn = new SqlConnection(SqlConnectionString))
            {
                conn.Open();

                string query = "SELECT Id, Name, Description FROM Categories";
                var categoryList = conn.Query<Category>(query).ToList();

                return categoryList;
            }
        }

        Models.Task IDataProvider.GetTaskById(int id)
        {
            using (SqlConnection conn = new SqlConnection(SqlConnectionString))
            {
                conn.Open();

                var task = conn.Query<Models.Task>(
                    "SELECT Id, TaskName, TaskText, Deadline, FinishDate, Category, isCompleted " +
                    "FROM Tasks WHERE id = @Id",
                    new Models.Task { Id = id }).First();

                return task;
            }
        }
        Category IDataProvider.GetCategoryById(int id)
        {
            using (SqlConnection conn = new SqlConnection(SqlConnectionString))
            {
                conn.Open();

                var category = conn.Query<Category>(
                    "SELECT Id, Name, Description " +
                    "FROM Categories WHERE Id = @Id",
                    new Category { Id = id }).First();

                return category;
            }
        }

        List<Models.Task> IDataProvider.GetTasksList()
        {
            using(SqlConnection conn = new SqlConnection(SqlConnectionString))
            {
                conn.Open();

                var taskList = conn.Query<Models.Task>(
                    "SELECT Id, TaskName, TaskText, Deadline, FinishDate, Category, isCompleted " +
                    "FROM Tasks " +
                    "ORDER BY isCompleted, Deadline").ToList();

                return taskList;
            }
        }

        void IDataProvider.UpdateTask(Models.Task task)
        {
            using (SqlConnection conn = new SqlConnection(SqlConnectionString))
            {
                conn.Open();

                conn.Execute(
                    "UPDATE Tasks " +
                    "SET " +
                        "TaskName = @TaskName, " +
                        "TaskText = @TaskText, " +
                        "Deadline = @DeadLine, " +
                        "FinishDate = @FinishDate, " +
                        "Category = @Category, " +
                        "isCompleted = @isCompleted " +
                        "WHERE Id = @Id",
                    new Models.Task
                    {
                        Id = task.Id,
                        TaskName = task.TaskName,
                        TaskText = task.TaskText,
                        DeadLine = task.DeadLine,
                        FinishDate = task.FinishDate,
                        Category = task.Category,
                        IsCompleted = task.IsCompleted
                    });
            }
        }
        
        void IDataProvider.UpdateCategory(Category category)
        {
            using (SqlConnection conn = new SqlConnection(SqlConnectionString))
            {
                conn.Open();

                conn.Execute(
                    "UPDATE Categories " +
                    "SET " +
                        "Name = @Name, " +
                        "Description = @Description " +
                        "WHERE Id = @Id",
                    new Category
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Description = category.Description
                    });
            }
        }
    }
}
