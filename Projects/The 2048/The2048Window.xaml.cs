using GameCenterProject.Projects.The_2048.Models;
using System;
using System.Collections.Generic;
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
            StartGame();
            this.KeyDown += MainWindow_KeyDown;
        }
        public void StartGame()
        {
            TheGameBoard = new GameBoard(GameGrid);
            TheGameBoard.CreateTile(2);
            TheGameBoard.CreateTile(2);
            TheGameBoard.CreateTile(2);
            TheGameBoard.CreateTile(2);
            TheGameBoard.CreateTile(2);
            TheGameBoard.CreateTile(2);
            TheGameBoard.CreateTile(2);
            TheGameBoard.CreateTile(2);
            TheGameBoard.CreateTile(2);
        }
        public void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Up || e.Key == Key.Right || e.Key == Key.Down)
            {
                TheGameBoard.MoveTiles(e.Key);
            }
        }
    }
}
