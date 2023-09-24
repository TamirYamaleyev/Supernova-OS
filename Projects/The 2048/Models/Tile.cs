using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GameCenterProject.Projects.The_2048.Models
{
    class Tile
    {
        public int TileValue { get; set; }
        public int Row
        {
            get { return Grid.GetRow(TileImage); }
            set { Grid.SetRow(TileImage, value); }
        }
        public int Column
        {
            get { return Grid.GetColumn(TileImage); }
            set { Grid.SetColumn(TileImage, value); }
        }
        public Image TileImage { get; set; }

        public Tile(int tileValue, int row, int col)
        {
            TileImage = new Image();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("/Projects/The 2048/Images/Number2Tile.png", UriKind.RelativeOrAbsolute);
            bitmapImage.EndInit();
            TileImage.Source = bitmapImage;

            TileValue = tileValue;
            Row = row;
            Column = col;
        }
        public static Tile CloneToLocation(Tile originalTile, int newRow, int newCol)
        {
            Tile newTile = new Tile(originalTile.TileValue, newRow, newCol);
            return newTile;
        }
    }
}
