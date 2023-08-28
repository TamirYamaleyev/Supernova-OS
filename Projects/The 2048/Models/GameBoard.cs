using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace GameCenterProject.Projects.The_2048.Models
{
    class GameBoard
    {
        public Grid gameGrid;
        public Tile[,] Board { get; set; }

        public GameBoard(Grid grid)
        {
            gameGrid = grid;
            Board = new Tile[4, 4];
        }

        public void CreateTile()
        {
            var rand = new Random();
            int rowToSpawn = rand.Next(0, 4);
            int colToSpawn = rand.Next(0, 4);

            Tile newTile = new Tile(2, rowToSpawn, colToSpawn);

            for (int row = 0; row < gameGrid.RowDefinitions.Count; row++)
            {
                for (int col = 0; col < gameGrid.ColumnDefinitions.Count; col++)
                {
                    // Access the cell content at the specified row and column
                    UIElement cellContent = gameGrid.Children
                        .Cast<UIElement>()
                        .FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col)!;
                }
            }

        }
    }
}
