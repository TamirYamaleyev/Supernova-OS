﻿using System;
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
        public List<Tile> TileList { get; set; }
        public Tile[,] Board { get; set; }

        public GameBoard(Grid gridFromWindow)
        {
            GameGrid = gridFromWindow;
            Board = new Tile[4, 4];
            TileList = new List<Tile>();
        }

        public void CreateTile(int tileValue)
        {
            var rand = new Random();
            int rowToSpawn = rand.Next(0, 4);
            int colToSpawn = rand.Next(0, 4);

            bool isntOpen = true;

            while(isntOpen)
            {
                rowToSpawn = rand.Next(0, 4);
                colToSpawn = rand.Next(0, 4);
                isntOpen = CheckIfCellIsOccupied(rowToSpawn, colToSpawn);
            }

            Tile newTile = new Tile(tileValue, rowToSpawn, colToSpawn);

            newTile.TileImage.HorizontalAlignment = HorizontalAlignment.Stretch;
            newTile.TileImage.VerticalAlignment = VerticalAlignment.Stretch;
            newTile.TileImage.Width = 198;
            newTile.TileImage.Height = 198;

            GameGrid.Children.Add(newTile.TileImage);

            newTile.Row = rowToSpawn;
            newTile.Column = colToSpawn;

            TileList.Add(newTile);
        }
        public void MoveTiles(Key keyPressed, Grid gridFromWindow)
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

            int rowCount = gridFromWindow.RowDefinitions.Count;
            int colCount = gridFromWindow.ColumnDefinitions.Count;
        }
        public bool CheckIfCellIsOccupied(int row, int col)
        {
            var children = GameGrid.Children.Cast<UIElement>().All(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col);
            //foreach (Image child in children)
            //{
            //    return child is Image;
            //}
            return false;
            //UIElement cellContent = GameGrid.Children.Cast<UIElement>().FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == col)!;
        }
    }
}
