using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameCenterProject.Projects.TodoList.Models
{
    /// <summary>
    /// Interaction logic for TodoList.xaml
    /// </summary>
    public partial class TodoList : Window
    {
        private TodoListModel _todoList;
        public TodoList()
        {
            InitializeComponent();
            InitializeTasks();
        }

        private void InitializeTasks()
        {
            _todoList = new TodoListModel();
            _todoList.AddNewTask(new TodoTask(1, "To do homework"));
            _todoList.AddNewTask(new TodoTask(2, "Complete the project"));
            listTasks.ItemsSource = _todoList.Tasks;
        }

        private void OnTaskToggled(object sender, RoutedEventArgs e)
        {
            // Check the type of a variable and create a name for it
            if (sender is CheckBox checkBox && checkBox.DataContext is TodoTask task)
            {
                _todoList.ToggleTaskIsComplete(task.Id);
            }
        }
        private void OnTextBlockMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                TextBlock textBlock = sender as TextBlock;
                StackPanel parent = textBlock.Parent as StackPanel;
                TextBox editTextBox = parent.FindName("editTaskDescription") as TextBox;
                Button btnSave = parent.FindName("btnSave") as Button;

                textBlock.Visibility = Visibility.Collapsed;
                editTextBox.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Visible;
                editTextBox.Text = textBlock.Text;
            }
        }
        private void OnSaveEdit(object sender, RoutedEventArgs e)
        {
            Button btnSave = sender as Button;
            StackPanel parent = btnSave.Parent as StackPanel;
            TextBox editTextBox = parent.FindName("editTaskDescription") as TextBox;
            TextBlock textBlock = parent.FindName("txtTaskDescription") as TextBlock;
            TodoTask task = btnSave.DataContext as TodoTask;

            _todoList.UpdateTask(task.Id, editTextBox.Text);
            textBlock.Text = task.Description;

            textBlock.Visibility = Visibility.Visible;
            editTextBox.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Collapsed;
        }
        private void OnAddTask(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewTask.Text))
            {
                TodoTask newTask = new TodoTask(_todoList.Tasks.Count + 1, txtNewTask.Text);
                _todoList.AddNewTask(newTask);
                txtNewTask.Clear();
            }
        }
    }
}
