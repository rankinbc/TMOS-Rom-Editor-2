using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmos.Romhacks.Core
{
    public class TmosWorldScreenTiles
    {
        public TmosTileSection TopTiles { get; set; }
        public TmosTileSection BottomTiles { get; set; }

        //get grid
        public byte[,] GetTileGrid(byte[] romTileData, int tileByteOffset)
        {
            byte[,] topTileGrid = TopTiles.GetTileSectionGrid();
            byte[,] bottomTileGrid = BottomTiles.GetTileSectionGrid();

            byte[,] fullGrid = new byte[8,6];   //only 2 rows used from bottom grid

            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (y < 4)
                    {
                        fullGrid[x, y] = topTileGrid[x, y];
                    }
                    else
                    {
                        fullGrid[x, y] = bottomTileGrid[x, y - 4];
                    }
                   
                }
            }
            return fullGrid;

        }


    }
}
