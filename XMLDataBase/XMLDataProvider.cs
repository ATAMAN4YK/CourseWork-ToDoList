using System.Xml.Linq;
using ToDoListData.Models;
using ToDoListData.Providers;
using Models = ToDoListData.Models;

namespace XMLDataBase
{
    public class XMLDataProvider : IDataProvider
    {
        private static int maxTaskId;
        private static int maxCategoryId;
        public XMLDataProvider()
        {
            XDocument xTasks;
            XDocument xCategories;
            try
            {
                xTasks = XDocument.Load(@"../XMLDataBase/DataBase/Tasks.xml");
            }
            catch (Exception)
            {
                xTasks = new XDocument(new XElement("tasks"));
                xTasks.Save(@"../XMLDataBase/DataBase/Tasks.xml");
                maxTaskId = 0;
            }

            try
            {
                xCategories = XDocument.Load(@"../XMLDataBase/DataBase/Categories.xml");
            }
            catch (Exception)
            {
                xCategories = new XDocument(new XElement("categories"));
                xCategories.Save(@"../XMLDataBase/DataBase/Categories.xml");
                maxCategoryId = 0;
            }
        }
        private void DefineMaxTaskId()
        {
            IDataProvider db = new XMLDataProvider();
            List<Models.Task> tasksList = db.GetTasksList();
            if (tasksList.Count != 0)
            {
                maxTaskId = tasksList.Max(task => task.Id);
            }
            else
            {
                maxTaskId = 0;
            }
        }
        private void DefineMaxCategoryId()
        {
            IDataProvider db = new XMLDataProvider();
            List<Category> categoriesList = db.GetCategoryList();
            if (categoriesList.Count != 0)
            {
                maxCategoryId = categoriesList.Max(category => category.Id);

            }
            else
            {
                maxCategoryId = 0;
            }
        }
        void IDataProvider.AddCategory(NewCategory category)
        {
            XDocument xCategoriesDocument = XDocument.Load(@"../XMLDataBase/DataBase/Categories.xml");
            XElement? categoriesNode = xCategoriesDocument.Element("categories");
            DefineMaxCategoryId();
            if (categoriesNode != null)
            {
                categoriesNode.Add(
                    new XElement("category", new XAttribute("id", ++maxCategoryId),
                        new XElement("Name", category.Name),
                        new XElement("Description", category.Description == null ? "Null" : category.Description)
                    ));
            }
            xCategoriesDocument.Save(@"../XMLDataBase/DataBase/Categories.xml");
        }

        void IDataProvider.AddTask(NewTask task)
        {
            XDocument xTasksDocument = XDocument.Load(@"../XMLDataBase/DataBase/Tasks.xml");
            XElement? tasksNode = xTasksDocument.Element("tasks");
            DefineMaxTaskId();
            if (tasksNode != null)
            {
                
                tasksNode.Add(
                    new XElement("task", new XAttribute("id", ++maxTaskId),
                        new XElement("TaskName", task.TaskName),
                        new XElement("TaskText", task.TaskText),
                        new XElement("DeadLine", task.DeadLine.ToString() == "" ? "Null" : task.DeadLine.ToString()),
                        new XElement("Category", new XAttribute("id", task.Category == null ? "Null" : task.Category)),
                        new XElement("isCompleted", "False"),
                        new XElement("FinishDate", "Null")
                    ));
            }
            xTasksDocument.Save(@"../XMLDataBase/DataBase/Tasks.xml");
        }

        void IDataProvider.DeleteCategory(Category category)
        {
            XDocument xCategoriesDocument = XDocument.Load(@"../XMLDataBase/DataBase/Categories.xml");
            XElement? categoriesNode = xCategoriesDocument.Element("categories");

            if (categoriesNode != null)
            {
                var removeCategory = categoriesNode.Elements("category")
                    .FirstOrDefault(c => c.Attribute("id")?.Value == $"{category.Id}");

                if (removeCategory != null)
                {
                    removeCategory.Remove();
                    xCategoriesDocument.Save(@"../XMLDataBase/DataBase/Categories.xml");

                    // Set null categoryId in tasks where been deleted category
                    IDataProvider db = new XMLDataProvider();
                    List<Models.Task> nullCategoryTasksList = db.GetTasksList()
                        .Where(task => task.Category == category.Id).ToList(); 
                    foreach (Models.Task task in nullCategoryTasksList)
                    {
                        task.Category = null;
                        db.UpdateTask(task);
                    }
                }
            }
        }

        void IDataProvider.DeleteTask(Models.Task task)
        {
            XDocument xTasksDocument = XDocument.Load(@"../XMLDataBase/DataBase/Tasks.xml");
            XElement? tasksNode = xTasksDocument.Element("tasks");

            if (tasksNode != null)
            {
                var removeTask = tasksNode.Elements("task")
                    .FirstOrDefault(t => t.Attribute("id")?.Value == $"{task.Id}");

                if (removeTask != null)
                {
                    removeTask.Remove();
                    xTasksDocument.Save(@"../XMLDataBase/DataBase/Tasks.xml");
                }
            }
        }

        Category IDataProvider.GetCategoryById(int id)
        {
            XDocument xCategoriesDocument = XDocument.Load(@"../XMLDataBase/DataBase/Categories.xml");
            XElement? categoriesNode = xCategoriesDocument.Element("categories");

            var category = categoriesNode.Elements("category")
                .FirstOrDefault(c => c.Attribute("id")?.Value == $"{id}");

            return new Category
            {
                Id = Int32.Parse(category.Attribute("id")?.Value),
                Name = category.Element("Name")?.Value,
                Description = category.Element("Description")?.Value == "Null" ? null : category.Element("Description")?.Value
            };
        }

        List<Category> IDataProvider.GetCategoryList()
        {
            XDocument xCategoriesDocument = XDocument.Load(@"../XMLDataBase/DataBase/Categories.xml");
            XElement? categoriesNode = xCategoriesDocument.Element("categories");
            List<Category> categoriesList = new List<Category>();
            if (categoriesNode != null)
            {
                foreach (var category in categoriesNode.Elements("category"))
                {
                    int id = Int32.Parse(category.Attribute("id").Value);
                    string categoryName = category.Element("Name").Value;
                    string? categoryDescription = category.Element("Description").Value == "Null" ? null : category.Element("Description").Value;

                    categoriesList.Add(new Category
                    {
                        Id = id,
                        Name = categoryName,
                        Description = categoryDescription
                    });
                }
            }
            return categoriesList;
        }

        Models.Task IDataProvider.GetTaskById(int id)
        {
            XDocument xTasksDocument = XDocument.Load(@"../XMLDataBase/DataBase/Tasks.xml");
            XElement? tasksNode = xTasksDocument.Element("tasks");

            var task = tasksNode.Elements("task")
                .FirstOrDefault(t => t.Attribute("id")?.Value == $"{id}");

            return new Models.Task
            {
                Id = Int32.Parse(task.Attribute("id").Value),
                TaskName = task.Element("TaskName").Value,
                TaskText = task.Element("TaskText").Value == "Null" ? null : task.Element("TaskText").Value,
                DeadLine = task.Element("DeadLine").Value == "Null" ? null : DateTime.Parse(task.Element("DeadLine").Value),
                Category = task.Element("Category").Attribute("id").Value == "Null" ? null : Int32.Parse(task.Element("Category").Attribute("id").Value),
                IsCompleted = bool.Parse(task.Element("isCompleted").Value),
                FinishDate = task.Element("FinishDate").Value == "Null" ? null : DateTime.Parse(task.Element("FinishDate").Value)
            };
        }

        List<Models.Task> IDataProvider.GetTasksList()
        {
            XDocument xTasksDocument = XDocument.Load(@"../XMLDataBase/DataBase/Tasks.xml");
            XElement? tasksNode = xTasksDocument.Element("tasks");
            List<Models.Task> tasksList = new List<Models.Task>();
            if (tasksNode != null)
            {
                foreach (var task in tasksNode.Elements("task"))
                {
                    int id = Int32.Parse(task.Attribute("id").Value);
                    string taskName = task.Element("TaskName").Value;
                    string? taskText = task.Element("TaskText").Value == "Null" ? null : task.Element("TaskText").Value;
                    DateTime? deadLine = task.Element("DeadLine").Value == "Null" ? null : DateTime.Parse(task.Element("DeadLine").Value);
                    int? categoryId = task.Element("Category").Attribute("id").Value == "Null" ? null : Int32.Parse(task.Element("Category").Attribute("id").Value);
                    bool isCompleted = bool.Parse(task.Element("isCompleted").Value);
                    DateTime? finishDate = task.Element("FinishDate").Value == "Null" ? null : DateTime.Parse(task.Element("FinishDate").Value);

                    tasksList.Add(new Models.Task
                    {
                        Id = id,
                        TaskName = taskName,
                        TaskText = taskText,
                        DeadLine = deadLine,
                        FinishDate = finishDate,
                        Category = categoryId,
                        IsCompleted = isCompleted
                    });
                }
            }
            return tasksList;
        }

        void IDataProvider.UpdateCategory(Category category)
        {
            XDocument xCategoriesDocument = XDocument.Load(@"../XMLDataBase/DataBase/Categories.xml");
            XElement? categoriesNode = xCategoriesDocument.Element("categories");
            if (categoriesNode != null)
            {
                var xmlCategory = categoriesNode.Elements("category")
                    .FirstOrDefault(c => c.Attribute("id")?.Value == $"{category.Id}");

                if (xmlCategory != null)
                {
                    xmlCategory.Element("Name")?.SetValue(category.Name);
                    xmlCategory.Element("Description")?.SetValue(category.Description == null ? "Null" : category.Description);

                    xCategoriesDocument.Save(@"../XMLDataBase/DataBase/Categories.xml");
                }
            }
        }

        void IDataProvider.UpdateTask(Models.Task task)
        {
            XDocument xTasksDocument = XDocument.Load(@"../XMLDataBase/DataBase/Tasks.xml");
            XElement? tasksNode = xTasksDocument.Element("tasks");
            if (tasksNode != null)
            {
                var xmlTask = tasksNode.Elements("task")
                    .FirstOrDefault(t => t.Attribute("id")?.Value == $"{task.Id}");

                if (xmlTask != null)
                {
                    xmlTask.Element("TaskName")?.SetValue(task.TaskName);
                    xmlTask.Element("TaskText")?.SetValue(task.TaskText == null ? "Null" : task.TaskText);
                    xmlTask.Element("DeadLine")?.SetValue(task.DeadLine == null ? "Null" : task.DeadLine.Value);
                    xmlTask.Element("Category")?.Attribute("id")?.SetValue(task.Category == null ? "Null" : task.Category);
                    xmlTask.Element("isCompleted")?.SetValue(task.IsCompleted.ToString());
                    xmlTask.Element("FinishDate")?.SetValue(task.FinishDate == null ? "Null" : task.FinishDate.Value);

                    xTasksDocument.Save(@"../XMLDataBase/DataBase/Tasks.xml");
                }
            }
        }
    }
}
