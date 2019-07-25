using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmos.Romhacks.Mods
{
    public class WSTile
    {
        public byte Id;
        public string Name { get; set; }

        //Image file or path?
        public bool IsWalkable { get; set; }

    }
}
