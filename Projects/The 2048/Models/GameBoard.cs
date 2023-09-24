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

        public void MoveLeft()
        {
            // Go through each Row
            for (int i = 0; i < 4; i++)
            {
                // Go through each cell starting from [x,1]
                for (int j = 1; j < 4; j++)
                {
                    // Check if the cell can move left, keep moving until you either hit a cell to combine with or you hit the edge
                    if (TileMatrix[i, j] != null)
                    {
                        int currentTileX = j;
                        while (currentTileX > 0 && !IsCellOccupied(i, currentTileX - 1))
                        {
                            MoveTile(TileMatrix[i, currentTileX], i, currentTileX - 1);
                            currentTileX--;
                        }

                        if (currentTileX > 0 && IsCellOccupied(i, currentTileX - 1))
                        {
                            if (TileMatrix[i, currentTileX].TileValue == TileMatrix[i, currentTileX - 1].TileValue)
                            {
                                CombineTiles(TileMatrix[i, currentTileX], i, currentTileX - 1);
                            }
                        }
                    }
                }
            }
        }
        public void MoveRight()
        {
            // Go through each Row
            for (int i = 0; i < 4; i++)
            {
                // Go through each cell starting from [x,2]
                for (int j = 2; j > -1; j--)
                {
                    // Check if the cell can move left, keep moving until you either hit a cell to combine with or you hit the edge
                    if (TileMatrix[i, j] != null)
                    {
                        int currentTileX = j;
                        while (currentTileX < 3 && !IsCellOccupied(i, currentTileX + 1))
                        {
                            MoveTile(TileMatrix[i, currentTileX], i, currentTileX + 1);
                            currentTileX++;
                        }

                        if (currentTileX < 3 && IsCellOccupied(i, currentTileX + 1))
                        {
                            if (TileMatrix[i, currentTileX].TileValue == TileMatrix[i, currentTileX + 1].TileValue)
                            {
                                CombineTiles(TileMatrix[i, currentTileX], i, currentTileX + 1);
                            }
                        }
                    }
                }
            }
        }
        public void MoveUp()
        {
            // Go through each Column
            for (int j = 0; j < 4; j++)
            {
                // Go through each cell starting from [1,x]
                for (int i = 1; i < 4; i++)
                {
                    // Check if the cell can move up, keep moving until you either hit a cell to combine with or you hit the edge
                    if (TileMatrix[i, j] != null)
                    {
                        int currentTileY = i;
                        while (currentTileY > 0 && !IsCellOccupied(currentTileY - 1, j))
                        {
                            MoveTile(TileMatrix[currentTileY, j], currentTileY - 1, j);
                            currentTileY--;
                        }

                        if (currentTileY > 0 && IsCellOccupied(currentTileY - 1, j))
                        {
                            if (TileMatrix[currentTileY, j].TileValue == TileMatrix[currentTileY - 1, j].TileValue)
                            {
                                CombineTiles(TileMatrix[currentTileY, j], currentTileY - 1, j);
                            }
                        }
                    }
                }
            }
        }
        public void MoveDown()
        {
            // Go through each Column
            for (int j = 0; j < 4; j++)
            {
                // Go through each cell starting from [2,x]
                for (int i = 2; i > -1; i--)
                {
                    // Check if the cell can move up, keep moving until you either hit a cell to combine with or you hit the edge
                    if (TileMatrix[i, j] != null)
                    {
                        int currentTileY = i;
                        while (currentTileY < 3 && !IsCellOccupied(currentTileY + 1, j))
                        {
                            MoveTile(TileMatrix[currentTileY, j], currentTileY + 1, j);
                            currentTileY++;
                        }

                        if (currentTileY < 3 && IsCellOccupied(currentTileY + 1, j))
                        {
                            if (TileMatrix[currentTileY, j].TileValue == TileMatrix[currentTileY + 1, j].TileValue)
                            {
                                CombineTiles(TileMatrix[currentTileY, j], currentTileY + 1, j);
                            }
                        }
                    }
                }
            }
        }
        public bool IsCellOccupied(int row, int col)
        {
            if (TileMatrix[row, col] == null)
            {
                return false;
            }
            return true;
        }
        public void MoveTile(Tile currentTile, int newRow, int newCol)
        {
            //int oldRow = currentTile.Row;
            //int oldCol = currentTile.Column;

            //currentTile.Row = newRow;
            //currentTile.Column = newCol;

            //TileMatrix[newRow, newCol] = currentTile;

            TileMatrix[newRow, newCol] = Tile.CloneToLocation(currentTile, newRow, newCol);
            DestroyTile(TileMatrix[currentTile.Row, currentTile.Column]);
        }
        // Moving tile, Stationary Tile row, Stationary Tile column
        public void CombineTiles(Tile movingTile, int newRow, int newCol)
        {
            //TileMatrix[newRow, newCol].TileValue += movingTile.TileValue;
            //// Change Tile Image
            //DestroyTile(movingTile);
        }
        public void DestroyTile(Tile tileToDestroy)
        {
            GameGrid.Children.Remove(TileMatrix[tileToDestroy.Row, tileToDestroy.Column].TileImage);
            TileMatrix[tileToDestroy.Row, tileToDestroy.Column].TileImage = null;
            tileToDestroy = null;
        }
    }
}
