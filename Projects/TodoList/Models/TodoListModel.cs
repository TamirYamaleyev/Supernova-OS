using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCenterProject.Projects.TodoList.Models
{
    internal class TodoListModel
    {
        public ObservableCollection<TodoTask> Tasks { get; set; }
        public TodoListModel()
        {
            Tasks = new ObservableCollection<TodoTask>();
        }
        public void UpdateTask(int taskId, string newDescription)
        {
            TodoTask task = Tasks.FirstOrDefault(task => task.Id == taskId)!;
            if (task != null)
            {
                task.Description = newDescription;
            }
            else
            {
                throw new Exception("The task with this ID wasn't found");
            }
        }
        public void ToggleTaskIsComplete(int taskId)
        {
            TodoTask task = Tasks.FirstOrDefault(task => task.Id == taskId)!;
            if (task != null)
            {
                task.IsComplete = !task.IsComplete;
            }
            else
            {
                throw new Exception("The task with this Id wasn't found");
            }
        }
        public void AddNewTask(TodoTask task)
        {
            Tasks.Add(task);
        }
        public void RemoveTask(TodoTask task)
        {
            Tasks.Remove(task);
        }
    }
}
