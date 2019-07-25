using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tmos.Romhacks.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            for(byte b = 0x00; b < 0xFF; b++)
            {
                // Console.WriteLine("case 0x" + b.ToString("X2") + ": return Tile.Tile0x" + b.ToString("X2") + ";");
                // Console.WriteLine("Tile0x" + b.ToString("X2") + ",");
                Console.WriteLine("{Tile.Tile0x" + b.ToString("X2") + ",\"" + b.ToString("X2") + "\"},");
            }
      



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
