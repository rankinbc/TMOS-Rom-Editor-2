using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmos.Romhacks.Core
{
    public class TmosWorldScreenDataOffset
    {
        byte[] _data;

        public TmosWorldScreenDataOffset(byte[] data)
        {
            _data = data;
        }

        //Get as int (convert byte[2] to int to get true value)
        //public int GetAsInt
    }
}
