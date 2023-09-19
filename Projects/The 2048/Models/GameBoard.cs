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
using System.Runtime.CompilerServices;

namespace GameCenterProject.Projects.The_2048.Models
{
    class GameBoard
    {
        public Grid GameGrid;
        //public List<Tile> TileList { get; set; }
        public Tile[,] TileMatrix { get; set; }
        public Tile[,] Board { get; set; }

        public GameBoard(Grid gridFromWindow)
        {
            GameGrid = gridFromWindow;
            Board = new Tile[4, 4];
            TileMatrix = new Tile[4,4];
        }

        public void CreateTile(int tileValue)
        {
            var rand = new Random();
            int rowToSpawn = rand.Next(0, 4);
            int colToSpawn = rand.Next(0, 4);

            bool isOccupied = true;

            while(isOccupied)
            {
                rowToSpawn = rand.Next(0, 4);
                colToSpawn = rand.Next(0, 4);
                isOccupied = IsCellOccupied(rowToSpawn, colToSpawn);
            }

            Tile newTile = new Tile(tileValue, rowToSpawn, colToSpawn);

            newTile.TileImage.HorizontalAlignment = HorizontalAlignment.Stretch;
            newTile.TileImage.VerticalAlignment = VerticalAlignment.Stretch;
            newTile.TileImage.Width = 198;
            newTile.TileImage.Height = 198;

            GameGrid.Children.Add(newTile.TileImage);

            newTile.Row = rowToSpawn;
            newTile.Column = colToSpawn;

            TileMatrix[rowToSpawn, colToSpawn] = newTile;
        }
        public void MoveTiles(Key keyPressed)
        {
            int rowChange = 0;
            int colChange = 0;
            switch (keyPressed)
            {
                case Key.Left:
                    colChange = -1;

                    foreach (Tile currentTile in TileMatrix)
                    {
                        if (currentTile == null) continue;
                        if (IsMovePossible(rowChange, colChange, currentTile))
                        {
                            MoveTile(currentTile, currentTile.Row + rowChange, currentTile.Column + colChange);
                        }
                    }
                    break;

                case Key.Up:
                    rowChange = -1;

                    foreach (Tile currentTile in TileMatrix)
                    {
                        if (currentTile == null) continue;
                        if (IsMovePossible(rowChange, colChange, currentTile))
                        {
                            MoveTile(currentTile, currentTile.Row + rowChange, currentTile.Column + colChange);
                        }
                    }
                    break;

                case Key.Right:
                    colChange = 1;

                    foreach (Tile currentTile in TileMatrix)
                    {
                        if (currentTile == null) continue;
                        if (IsMovePossible(rowChange, colChange, currentTile))
                        {
                            MoveTile(currentTile, currentTile.Row + rowChange, currentTile.Column + colChange);
                        }
                    }
                    break;

                case Key.Down:
                    rowChange = 1;

                    foreach (Tile currentTile in TileMatrix)
                    {
                        if (currentTile == null) continue;
                        if (IsMovePossible(rowChange, colChange, currentTile))
                        {
                            MoveTile(currentTile, currentTile.Row + rowChange, currentTile.Column + colChange);
                        }
                    }
                    break;
            }

            int rowCount = GameGrid.RowDefinitions.Count;
            int colCount = GameGrid.ColumnDefinitions.Count;
        }
        public bool IsCellOccupied(int row, int col)
        {
            if (TileMatrix[row, col] == null)
            {
                return false;
            }
            return true;
        }
        public bool IsMovePossible(int rowChange, int colChange, Tile tile)
        {
            if ((tile.Row + rowChange) < 0 || (tile.Row + rowChange) > 3 || (tile.Column + colChange) < 0 || (tile.Column + colChange) > 3)
            {
                return false;
            }
            return true;
        }
        public void MoveTile(Tile currentTile, int newRow, int newCol)
        {
            if (!IsCellOccupied(newRow, newCol))
            {
                int oldRow = currentTile.Row;
                int oldCol = currentTile.Column;

                currentTile.Row = newRow;
                currentTile.Column = newCol;

                TileMatrix[currentTile.Row, currentTile.Column] = currentTile;

                TileMatrix[oldRow, oldCol] = null!;
            }
            else
            {
                CombineTiles(currentTile, newRow, newCol);
            }
        }
        public void CombineTiles(Tile movingTile, int newRow, int newCol)
        {
            movingTile.TileValue += TileMatrix[newRow, newCol].TileValue;
            movingTile = null!;

            // Change Tile Image
        }
    }
}
