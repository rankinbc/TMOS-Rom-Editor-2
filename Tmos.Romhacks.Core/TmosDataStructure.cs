using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmos.Romhacks.Core
{
   public class TmosDataStructure
    {
        public TmosDataStructure(int address, int length)
        {
            Address = address;
            Length = length;
        }
        public int Address;
        public int Length;
    }
}
