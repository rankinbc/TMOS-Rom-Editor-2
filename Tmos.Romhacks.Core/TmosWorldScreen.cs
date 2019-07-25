using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmos.Romhacks.Core
{
    public class TmosWorldScreen
    {
        public byte[] _data = null;

        public enum DataContent
        {
            ParentWorld, //music and some other things
            AmbientSound,
            Content,
            ObjectSet, //includes doors
            ScreenIndexRight,
            ScreenIndexLeft,
            ScreenIndexDown,
            ScreenIndexUp,
            DataPointer,
            ExitPosition,
            TopTiles,
            BottomTiles,
            WorldScreenColor,
            SpritesColor,
            Unknown,
            Event //dialog
        }

        public byte ParentWorld { get { return _data[(int)DataContent.ParentWorld]; } set { _data[(int)DataContent.ParentWorld] = value; } }
        public byte AmbientSound { get { return _data[(int)DataContent.AmbientSound]; } set { _data[(int)DataContent.AmbientSound] = value; } }
        public byte Content { get { return _data[(int)DataContent.Content]; } set { _data[(int)DataContent.Content] = value; } }
        public byte ObjectSet { get { return _data[(int)DataContent.ObjectSet]; } set { _data[(int)DataContent.ObjectSet] = value; } }
        public byte ScreenIndexRight { get { return _data[(int)DataContent.ScreenIndexRight]; } set { _data[(int)DataContent.ScreenIndexRight] = value; } }
        public byte ScreenIndexLeft { get { return _data[(int)DataContent.ScreenIndexLeft]; } set { _data[(int)DataContent.ScreenIndexLeft] = value; } }
        public byte ScreenIndexDown { get { return _data[(int)DataContent.ScreenIndexDown]; } set { _data[(int)DataContent.ScreenIndexDown] = value; } }
        public byte ScreenIndexUp { get { return _data[(int)DataContent.ScreenIndexUp]; } set { _data[(int)DataContent.ScreenIndexUp] = value; } }
        public byte DataPointer { get { return _data[(int)DataContent.DataPointer]; } set { _data[(int)DataContent.DataPointer] = value; } }
        public byte ExitPosition { get { return _data[(int)DataContent.ExitPosition]; } set { _data[(int)DataContent.ExitPosition] = value; } }
        public byte TopTiles { get { return _data[(int)DataContent.TopTiles]; } set { _data[(int)DataContent.TopTiles] = value; } }
        public byte BottomTiles { get { return _data[(int)DataContent.BottomTiles]; } set { _data[(int)DataContent.BottomTiles] = value; } }
        public byte WorldScreenColor { get { return _data[(int)DataContent.WorldScreenColor]; } set { _data[(int)DataContent.WorldScreenColor] = value; } }
        public byte SpritesColor { get { return _data[(int)DataContent.SpritesColor]; } set { _data[(int)DataContent.SpritesColor] = value; } }
        public byte Unknown { get { return _data[(int)DataContent.Unknown]; } set { _data[(int)DataContent.Unknown] = value; } }
        public byte Event { get { return _data[(int)DataContent.Event]; } set { _data[(int)DataContent.Event] = value; } }

       

        public TmosWorldScreen(byte[] data)
        {
            _data = data;
        }

       
    }
}
