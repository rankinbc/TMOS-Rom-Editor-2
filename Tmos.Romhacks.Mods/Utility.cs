using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmos.Romhacks.Mods
{
    public class Utility
    {
          public static int HexStringToInt(string hex)
        {
            byte[] bytes = Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();

            return Convert.ToInt32(bytes[0]);
        }
    }
}
