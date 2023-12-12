using gameCenter.Projects.Project1;
using GameCenterProject.Projects;
using GameCenterProject.Projects.Calculator.Models;
using GameCenterProject.Projects.CurrencyConverter;
using GameCenterProject.Projects.RubiksTimer;
using GameCenterProject.Projects.SpaceShooter;
using GameCenterProject.Projects.TodoList.Models;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace GameCenterProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer clock = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            clock.Tick += dateTimerTick!;
            clock.Start();
        }
        private void dateTimerTick (object sender, EventArgs e)
        {
            DateLabel.Content = DateTime.Now.ToString("dddd, dd, MMMM, yyyy, HH:mm:ss");
            CommandManager.InvalidateRequerySuggested();
        }
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Image image = (sender as Image)!;
            image.Opacity = 0.7;
            GameText.Content = (image.Name) switch
            {
                "Image1" => "User Management System",
                "Image2" => "To Do List",
                "Image3" => "Currency Conversion System",
                "Image4" => "Rubik's Cube Timer",
                "Image5" => "Space Shooter Game",
                "Image6" => "Calculator",
                _ => "Choose Program:"
            };
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Image)!.Opacity = 1;
            GameText.Content = "Choose Program:";
        }

        private void Image1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Project1 project1 = new();
            projectPresentationPage presentation = new();
            string projectDescription = 
                "A program that lets you add and remove users, update their details and freeze/unfreeze their accounts.";
            presentation.OnStart("User Management Program", projectDescription, Image1.Source, project1);
            Hide();
            presentation.ShowDialog();
            Show();
        }

        private void Image2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TodoList todoListProject = new();
            projectPresentationPage presentation = new();
            string projectDescription = 
                "A simple To-Do List that allows adding and removing tasks, as well as checking/unchecking their completion and " +
                "editing by double-clicking the text.";
            presentation.OnStart("To Do List", projectDescription, Image2.Source, todoListProject);
            Hide();
            presentation.ShowDialog();
            Show();
        }

        private void Image3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CurrencyConverterView currencyConverterProject = new();
            projectPresentationPage presentation = new();
            string projectDescription = 
                "A Currency Conversion system that lets the user choose the currency they're converting from and to, " +
                "and then uses the appropriate exchange rate multiplied by the amount entered.";
            presentation.OnStart("Currency Converter", projectDescription, Image3.Source, currencyConverterProject);
            Hide();
            presentation.ShowDialog();
            Show();
        }
        private void Image4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RubiksTimerWindow cubeTimerProject = new();
            projectPresentationPage presentation = new();
            string projectDescription =
                "A Rubik's Cube timer that allows the user to scramble their cube based on a random combination presented before " +
                "each solve, as well as check their solve time and even record the solve in a times history.";
            presentation.OnStart("Rubik's Cube Timer", projectDescription, Image4.Source, cubeTimerProject);
            Hide();
            presentation.ShowDialog();
            Show();
        }

        private void Image5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SpaceShooterWindow SpaceShooterProject = new();
            projectPresentationPage presentation = new();
            string projectDescription =
                "A Space Shooter game where the player controls a ship that has to dodge enemies flying towards it whilst shooting them in order to " +
                "score points.";
            presentation.OnStart("Space Shooter", projectDescription, Image5.Source, SpaceShooterProject);
            Hide();
            presentation.ShowDialog();
            Show();
        }

        private void Image6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CalculatorWindow calculatorWindow = new();
            projectPresentationPage presentation = new();
            string projectDescription =
                "A simple standard calculator with basic arithmetic functions based on the built in Microsoft Windows Calculator.";
            presentation.OnStart("Calculator", projectDescription, Image6.Source, calculatorWindow);
            Hide();
            presentation.ShowDialog();
            Show();
        }
    }
}
