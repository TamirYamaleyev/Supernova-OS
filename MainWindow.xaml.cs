using gameCenter.Projects.Project1;
using GameCenterProject.Projects;
using GameCenterProject.Projects.Brick_Breaker;
using GameCenterProject.Projects.Calculator.Models;
using GameCenterProject.Projects.CurrencyConverter;
using GameCenterProject.Projects.RubiksTimer;
using GameCenterProject.Projects.SpaceShooter;
using GameCenterProject.Projects.The_2048;
using GameCenterProject.Projects.TodoList.Models;
using System;
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
            DateLabel.Content = DateTime.UtcNow.ToString("dddd, dd, MMMM, yyyy, HH:mm:ss");
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
                _ => "please pick a game"
            };
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Image)!.Opacity = 1;
            GameText.Content = "please pick a game";
        }

        private void Image1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Project1 project1 = new();
            projectPresentationPage presentation = new();
            presentation.OnStart("To-Do List", "" + "LOREM IMPSUM", Image1.Source, project1);
            Hide();
            presentation.ShowDialog();
            Show();
        }

        private void Image2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TodoList todoListProject = new();
            Hide();
            todoListProject.ShowDialog();
            Show();
        }

        private void Image3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CurrencyConverterView currencyConverterProject = new();
            projectPresentationPage presentation = new();
            presentation.OnStart("Currency Converter", "" + "LOREM IPSUM", Image1.Source, currencyConverterProject);
            Hide();
            presentation.ShowDialog();
            Show();
        }
        private void Image4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RubiksTimerWindow cubeTimerProject = new();
            projectPresentationPage presentation = new();
            presentation.OnStart("Rubik's Cube Timer", "" + "LOREM IPSUM", Image1.Source, cubeTimerProject);
            Hide();
            presentation.ShowDialog();
            Show();
        }

        private void Image5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SpaceShooterWindow SpaceShooterProject = new();
            projectPresentationPage presentation = new();
            presentation.OnStart("Space Shooter", "" + "LOREM IPSUM", Image1.Source, SpaceShooterProject);
            Hide();
            presentation.ShowDialog();
            Show();
        }

        private void Image6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CalculatorWindow calculatorWindow = new();
            projectPresentationPage presentation = new();
            presentation.OnStart("Calculator", "" + "Calculator", Image1.Source, calculatorWindow);
            Hide();
            presentation.ShowDialog();
            Show();
        }
    }
}
