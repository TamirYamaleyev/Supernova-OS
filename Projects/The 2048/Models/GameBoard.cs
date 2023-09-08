using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Security.Cryptography.Xml;

namespace GameCenterProject.Projects.The_2048.Models
{
    class GameBoard
    {
        public Grid gameGrid;
        public Tile[,] Board { get; set; }

        public GameBoard(Grid gridFromWindow)
        {
            gameGrid = gridFromWindow;
            Board = new Tile[4, 4];
        }

        public void CreateTile(int tileValue)
        {
            var rand = new Random();
            int rowToSpawn = rand.Next(0, 4);
            int colToSpawn = rand.Next(0, 4);

            while (CheckIfCellIsOpen(rowToSpawn, colToSpawn))
            {
                rowToSpawn = rand.Next(0, 4);
                colToSpawn = rand.Next(0, 4);
            }

            Tile newTile = new Tile(tileValue, rowToSpawn, colToSpawn);

            newTile.TileImage.HorizontalAlignment = HorizontalAlignment.Stretch;
            newTile.TileImage.VerticalAlignment = VerticalAlignment.Stretch;
            newTile.TileImage.Width = 198;
            newTile.TileImage.Height = 198;

            gameGrid.Children.Add(newTile.TileImage);

            newTile.Row = rowToSpawn;
            newTile.Column = colToSpawn;
            //Grid.SetRow(newTile.TileImage, rowToSpawn);
            //Grid.SetColumn(newTile.TileImage, colToSpawn);
        }
        public static void MoveTiles(Key keyPressed)
        {
            int rowChange;
            int colChange;
            switch (keyPressed)
            {
                case Key.Left:
                    rowChange = -1;
                    break;
                case Key.Up:
                    colChange = -1;
                    break;
                case Key.Right:
                    rowChange = 1;
                    break;
                case Key.Down:
                    colChange = 1;
                    break;
            }

            
        }
        public bool CheckIfCellIsOpen(int row, int col)
        {
            UIElement cellContent = gameGrid.Children.Cast<UIElement>().FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col)!;
            return cellContent is Image;
        }
    }
}
