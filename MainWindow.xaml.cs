using gameCenter.Projects.Project1;
using GameCenterProject.Projects.Brick_Breaker;
using GameCenterProject.Projects.CurrencyConverter;
using GameCenterProject.Projects.TodoList.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Image image = (sender as Image)!;
            image.Opacity = 0.7;
            GameText.Content = (image.Name) switch
            {
                "Image1" => "A User Management System",
                "Image2" => "A To Do List",
                "Image3" => "A Currency Conversion System",
                "Image4" => "Game No. 4 is a game about lorm ipsum & happy birthday",
                "Image5" => "Game No. 5 is a game about lorm ipsum & happy birthday",
                "Image6" => "Brick Breaker Clone",
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
            Hide();
            project1.ShowDialog();
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
            Hide();
            currencyConverterProject.ShowDialog();
            Show();
        }

        private void Image4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrickBreakerWindow brickBreakerGame = new();
            Hide();
            brickBreakerGame.ShowDialog();
            Show();
        }
    }
}
