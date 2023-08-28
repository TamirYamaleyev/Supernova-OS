using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCenterProject.Projects.The_2048.Models
{
    class Tile
    {
        public int TileValue { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public Tile(int tileValue, int row, int col)
        {
            TileValue = tileValue;
            Row = row;
            Column = col;
        }
    }
}
