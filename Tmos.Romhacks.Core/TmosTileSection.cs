using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmos.Romhacks.Core
{
    public  class TmosTileSection
    {
         byte[] _data = null;

   
   

        public TmosTileSection(byte[] data)
        {
            _data = data;
        }

       

        public byte[,] GetTileSectionGrid()
        {
            byte[,] tileGrid = new byte[8, 4]; //(0,0) top left
            int byteIndex = 0;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    tileGrid[x, y] = _data[byteIndex ];
                    byteIndex++;
                }
            }
            return tileGrid;
        }


        





    }
}
