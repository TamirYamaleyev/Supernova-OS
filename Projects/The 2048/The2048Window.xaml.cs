using GameCenterProject.Projects.The_2048.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.RightsManagement;
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

namespace GameCenterProject.Projects.The_2048
{
    /// <summary>
    /// Interaction logic for The2048Window.xaml
    /// </summary>
    public partial class The2048Window : Window
    {
        private GameBoard TheGameBoard;
        public The2048Window()
        {
            InitializeComponent();

            // Error Grabber
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            StartGame();
            this.KeyDown += MainWindow_KeyDown;
        }
        public void StartGame()
        {
            GameBoard singleGameBoard = GameBoard.Instance;
            GameBoard.ClearBoard();

            //for (int col = 0; col <= 3; col++)
            //{
            //    for (int row = 1; row <= 3; row++)
            //    {
            //        GameBoard.DEVCreateTile(col, row, 2);
            //    }
            //}
            GameBoard.DEVCreateTile(0, 0, 2);
            GameBoard.DEVCreateTile(0, 1, 2);

            GameBoard.DEVCreateTile(1, 1, 2);
            GameBoard.DEVCreateTile(1, 2, 2);

            //GameBoard.DEVCreateTile(2, 2, 2);
            //GameBoard.DEVCreateTile(2, 3, 2);

            //GameBoard.DEVCreateTile(3, 3, 2);
            //GameBoard.DEVCreateTile(3, 1, 2);

            //GameBoard.CreateTile(2, 2);
            GameBoard.UpdateBoard(GameGrid);
        }
        public void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Left: GameBoard.MoveLeft(GameGrid); break;
                case Key.Right: GameBoard.MoveRight(GameGrid); break;
                case Key.Up: GameBoard.MoveUp(GameGrid); break;
                case Key.Down: GameBoard.MoveDown(GameGrid); break;
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = e.ExceptionObject as Exception;
            LogException(e.ExceptionObject as Exception);
            ShowExceptionMessageBox(exception);
        }
        private void LogException(Exception ex)
        {

        }
        private void ShowExceptionMessageBox(Exception ex)
        {
            MessageBox.Show($"An unexpected error occurred:\n\n{ex?.Message}\n\n{ex?.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
