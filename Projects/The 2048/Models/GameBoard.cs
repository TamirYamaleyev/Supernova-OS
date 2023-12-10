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
        private static GameBoard _instance;
        private GameBoard()
        {
            TileMatrix = new int[4, 4];
        }
        public static GameBoard Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameBoard();
                }
                return _instance;
            }
        }
        private static int[,] _tileMatrix { get; set; }
        public static int[,] TileMatrix { get { return _tileMatrix; } set { _tileMatrix = value; } }

        public static void DEVCreateTile(int col, int row, int tileValue)
        {
            TileMatrix[col, row] = tileValue;
        }
        public static void CreateTile(int noOfTiles, int tileValue)
        {
            Random random = new Random();
            for (int i = 0; i < noOfTiles; i++)
            {
                int rowLocation = random.Next(0, 3);
                int colLocation = random.Next(0, 3);
                TileMatrix[colLocation, rowLocation] = tileValue;
            }
        }
        public static void ChangeTileValue(int col, int row, int newValue)
        {
            TileMatrix[col, row] = newValue;
        }
        public static void MoveTile(int oldCol, int oldRow, int newCol, int newRow)
        {
            TileMatrix[newCol, newRow] = TileMatrix[oldCol, oldRow];
            TileMatrix[oldCol, oldRow] = 0;
        }
        public static void DestroyTile(int col, int row)
        {
            TileMatrix[col, row] = 0;
        }
        public static void ClearBoard()
        {
            for (int col = 0; col < TileMatrix.GetLength(0); col++)
            {
                for (int row = 0; row < TileMatrix.GetLength(1); row++)
                {

                    DestroyTile(col, row);
                }
            }
        }
        public static void UpdateBoard(Grid boardGrid)
        {
            for (int col = 0; col < TileMatrix.GetLength(0); col++)
            {
                for (int row = 0; row < TileMatrix.GetLength(1); row++)
                {
                    if (TileMatrix[col, row] != 0)
                    {
                        Image tileImage = new Image
                        {
                            Source = new BitmapImage(new Uri("/Projects/The 2048/Images/Number2Tile.png", UriKind.RelativeOrAbsolute)),
                            Stretch = System.Windows.Media.Stretch.UniformToFill,
                            Width = 198,
                            Height = 198
                        };
                        Grid.SetRow(tileImage, row);
                        Grid.SetColumn(tileImage, col);
                        boardGrid.Children.Add(tileImage);
                    }
                    else
                    {
                        foreach (UIElement element in boardGrid.Children)
                        {
                            if (element is Image && Grid.GetRow(element) == row && Grid.GetColumn(element) == col)
                            {
                                boardGrid.Children.Remove(element);
                                break;
                            }
                        }
                    }
                }
            }
        }
        public static int GetTileValue(int col, int row)
        {
            return TileMatrix[col, row];
        }
        public static bool IsTileEmpty(int col, int row)
        {
            return TileMatrix[col, row] == 0;
        }
        public static void MoveUp(Grid grid)
        {
            for (int col = 0; col <= 3; col++)
            {
                for (int row = 1; row <= 3; row++)
                {
                    // if tile exists, being checking
                    if (!IsTileEmpty(col, row))
                    {
                        int rowToMove = row;
                        // check tiles above one by one
                        for (int checkRow = row - 1; checkRow >= 0; checkRow--)
                        {
                            //MessageBox.Show($"checkRow: {checkRow} rowToMove: {rowToMove} row: {row}");
                            if (IsTileEmpty(col, checkRow))
                            {
                                rowToMove = checkRow;
                                //MessageBox.Show($"rowToMove: {rowToMove}");
                            }
                        }
                        if (rowToMove != row)
                        {
                            MoveTile(col, row, col, rowToMove);
                            //MessageBox.Show($"Moved Up - Col: {col}, Row: {row}");
                        }
                    }
                }
            }
            UpdateBoard(grid);
        }
        public static void MoveRight(Grid grid)
        {
            for (int row = 3; row >= 0; row--)
            {
                for (int col = 2; col >= 0; col--)
                {
                    // if tile exists, being checking
                    if (!IsTileEmpty(col, row))
                    {
                        int colToMove = col;
                        // check tiles above one by one
                        for (int checkCol = col + 1; checkCol <= 3; checkCol++)
                        {
                            //MessageBox.Show($"checkRow: {checkRow} rowToMove: {rowToMove} row: {row}");
                            if (IsTileEmpty(checkCol, row))
                            {
                                colToMove = checkCol;
                                //MessageBox.Show($"rowToMove: {rowToMove}");
                            }
                        }
                        if (colToMove != col)
                        {
                            MoveTile(col, row, colToMove, row);
                            //MessageBox.Show($"Moved Up - Col: {col}, Row: {row}");
                        }
                    }
                }
            }
            UpdateBoard(grid);
        }
        
        public static void MoveDown(Grid grid)
        {
            for (int col = 3; col >= 0; col--)
            {
                for (int row = 2; row >= 0; row--)
                {
                    // if tile exists, being checking
                    if (!IsTileEmpty(col, row))
                    {
                        int rowToMove = row;
                        // check tiles above one by one
                        for (int checkRow = row + 1; checkRow <= 3; checkRow++)
                        {
                            //MessageBox.Show($"checkRow: {checkRow} rowToMove: {rowToMove} row: {row}");
                            if (IsTileEmpty(col, checkRow))
                            {
                                rowToMove = checkRow;
                                //MessageBox.Show($"rowToMove: {rowToMove}");
                            }
                        }
                        if (rowToMove != row)
                        {
                            MoveTile(col, row, col, rowToMove);
                            //MessageBox.Show($"Moved Up - Col: {col}, Row: {row}");
                        }
                    }
                }
            }
            UpdateBoard(grid);
        }
        public static void MoveLeft(Grid grid)
        {
            for (int row = 0; row <= 3; row++)
            {
                for (int col = 1; col <= 3; col++)
                {
                    // if tile exists, being checking
                    if (!IsTileEmpty(col, row))
                    {
                        int colToMove = col;
                        // check tiles above one by one
                        for (int checkCol = col - 1; checkCol >= 0; checkCol--)
                        {
                            //MessageBox.Show($"checkRow: {checkRow} rowToMove: {rowToMove} row: {row}");
                            if (IsTileEmpty(checkCol, row))
                            {
                                colToMove = checkCol;
                                //MessageBox.Show($"rowToMove: {rowToMove}");
                            }
                        }
                        if (colToMove != col)
                        {
                            MoveTile(col, row, colToMove, row);
                            //MessageBox.Show($"Moved Up - Col: {col}, Row: {row}");
                        }
                    }
                }
            }
            UpdateBoard(grid);
        }

        //    public Tile[,] Board { get; set; }

        //    public void CreateTile(int tileValue)
        //    {
        //        var rand = new Random();
        //        int rowToSpawn = rand.Next(0, 4);
        //        int colToSpawn = rand.Next(0, 4);

        //        bool isOccupied = true;

        //        while(isOccupied)
        //        {
        //            rowToSpawn = rand.Next(0, 4);
        //            colToSpawn = rand.Next(0, 4);
        //            isOccupied = IsCellOccupied(rowToSpawn, colToSpawn);
        //        }

        //        Tile newTile = new Tile(tileValue, rowToSpawn, colToSpawn);

        //        newTile.TileImage.HorizontalAlignment = HorizontalAlignment.Stretch;
        //        newTile.TileImage.VerticalAlignment = VerticalAlignment.Stretch;
        //        newTile.TileImage.Width = 198;
        //        newTile.TileImage.Height = 198;

        //        GameGrid.Children.Add(newTile.TileImage);

        //        newTile.Row = rowToSpawn;
        //        newTile.Column = colToSpawn;

        //        TileMatrix[rowToSpawn, colToSpawn] = newTile;
        //    }

        //    public void MoveLeft()
        //    {
        //        // Go through each Row
        //        for (int i = 0; i < 4; i++)
        //        {
        //            // Go through each cell starting from [x,1]
        //            for (int j = 1; j < 4; j++)
        //            {
        //                // Check if the cell can move left, keep moving until you either hit a cell to combine with or you hit the edge
        //                if (TileMatrix[i, j] != null)
        //                {
        //                    int currentTileX = j;
        //                    while (currentTileX > 0 && !IsCellOccupied(i, currentTileX - 1))
        //                    {
        //                        MoveTile(TileMatrix[i, currentTileX], i, currentTileX - 1);
        //                        currentTileX--;
        //                    }

        //                    if (currentTileX > 0 && IsCellOccupied(i, currentTileX - 1))
        //                    {
        //                        if (TileMatrix[i, currentTileX].TileValue == TileMatrix[i, currentTileX - 1].TileValue)
        //                        {
        //                            CombineTiles(TileMatrix[i, currentTileX], i, currentTileX - 1);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    public void MoveRight()
        //    {
        //        // Go through each Row
        //        for (int i = 0; i < 4; i++)
        //        {
        //            // Go through each cell starting from [x,2]
        //            for (int j = 2; j > -1; j--)
        //            {
        //                // Check if the cell can move left, keep moving until you either hit a cell to combine with or you hit the edge
        //                if (TileMatrix[i, j] != null)
        //                {
        //                    int currentTileX = j;
        //                    while (currentTileX < 3 && !IsCellOccupied(i, currentTileX + 1))
        //                    {
        //                        MoveTile(TileMatrix[i, currentTileX], i, currentTileX + 1);
        //                        currentTileX++;
        //                    }

        //                    if (currentTileX < 3 && IsCellOccupied(i, currentTileX + 1))
        //                    {
        //                        if (TileMatrix[i, currentTileX].TileValue == TileMatrix[i, currentTileX + 1].TileValue)
        //                        {
        //                            CombineTiles(TileMatrix[i, currentTileX], i, currentTileX + 1);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    public void MoveUp()
        //    {
        //        // Go through each Column
        //        for (int j = 0; j < 4; j++)
        //        {
        //            // Go through each cell starting from [1,x]
        //            for (int i = 1; i < 4; i++)
        //            {
        //                // Check if the cell can move up, keep moving until you either hit a cell to combine with or you hit the edge
        //                if (TileMatrix[i, j] != null)
        //                {
        //                    int currentTileY = i;
        //                    while (currentTileY > 0 && !IsCellOccupied(currentTileY - 1, j))
        //                    {
        //                        MoveTile(TileMatrix[currentTileY, j], currentTileY - 1, j);
        //                        currentTileY--;
        //                    }

        //                    if (currentTileY > 0 && IsCellOccupied(currentTileY - 1, j))
        //                    {
        //                        if (TileMatrix[currentTileY, j].TileValue == TileMatrix[currentTileY - 1, j].TileValue)
        //                        {
        //                            CombineTiles(TileMatrix[currentTileY, j], currentTileY - 1, j);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    public void MoveDown()
        //    {
        //        // Go through each Column
        //        for (int j = 0; j < 4; j++)
        //        {
        //            // Go through each cell starting from [2,x]
        //            for (int i = 2; i > -1; i--)
        //            {
        //                // Check if the cell can move up, keep moving until you either hit a cell to combine with or you hit the edge
        //                if (TileMatrix[i, j] != null)
        //                {
        //                    int currentTileY = i;
        //                    while (currentTileY < 3 && !IsCellOccupied(currentTileY + 1, j))
        //                    {
        //                        MoveTile(TileMatrix[currentTileY, j], currentTileY + 1, j);
        //                        currentTileY++;
        //                    }

        //                    if (currentTileY < 3 && IsCellOccupied(currentTileY + 1, j))
        //                    {
        //                        if (TileMatrix[currentTileY, j].TileValue == TileMatrix[currentTileY + 1, j].TileValue)
        //                        {
        //                            CombineTiles(TileMatrix[currentTileY, j], currentTileY + 1, j);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    public bool IsCellOccupied(int row, int col)
        //    {
        //        if (TileMatrix[row, col] == null)
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //    public void MoveTile(Tile currentTile, int newRow, int newCol)
        //    {
        //        //int oldRow = currentTile.Row;
        //        //int oldCol = currentTile.Column;

        //        //currentTile.Row = newRow;
        //        //currentTile.Column = newCol;

        //        //TileMatrix[newRow, newCol] = currentTile;

        //        TileMatrix[newRow, newCol] = Tile.CloneToLocation(currentTile, newRow, newCol);
        //        DestroyTile(TileMatrix[currentTile.Row, currentTile.Column]);
        //    }
        //    // Moving tile, Stationary Tile row, Stationary Tile column
        //    public void CombineTiles(Tile movingTile, int newRow, int newCol)
        //    {
        //        //TileMatrix[newRow, newCol].TileValue += movingTile.TileValue;
        //        //// Change Tile Image
        //        //DestroyTile(movingTile);
        //    }
        //    public void DestroyTile(Tile tileToDestroy)
        //    {
        //        GameGrid.Children.Remove(TileMatrix[tileToDestroy.Row, tileToDestroy.Column].TileImage);
        //        TileMatrix[tileToDestroy.Row, tileToDestroy.Column].TileImage = null;
        //        tileToDestroy = null;
        //    }
        //}
    }
}
