using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tmos.Romhacks.Core.TmosData;

namespace Tmos.Romhacks.Core
{
     public class TmosRom
    {
        public static byte[] Bytes;
        bool HasUnsavedChanges;

        public TmosRom() { }

        public void LoadRom(string filePath)
        {
            Bytes = File.ReadAllBytes(filePath);
            HasUnsavedChanges = false;
        }

        public void WriteRom(string filePath)
        {
            File.WriteAllBytes(filePath, Bytes);
        }

        public byte[] LoadTileData()
        {
            return TmosData.GetTileData(Bytes);
        }

        public TmosWorldScreen LoadWorldScreen(int index)
        {
            return TmosData.GetWorldScreen(Bytes, index);
        }

        public void SaveWorldScreen(int index, TmosWorldScreen worldScreen)
        {
            TmosData.SaveWorldScreen(Bytes,index, worldScreen);
        }

        public TmosWorldScreenDataOffset LoadWorldScreenDataOffset(int index)
        {
            return TmosData.GetWorldScreenDataOffset(Bytes, index);
        }

        public TmosTileSection LoadTileSection(int index, int tileDataOffset)
        {
            return TmosData.GetTileSection(Bytes, index, tileDataOffset);
        }

        public TmosTile LoadTile(int index)
        {
            return TmosData.GetTile(Bytes, index);
        }

        public TmosMiniTile LoadMiniTile(int index)
        {
            return TmosData.GetMiniTile(Bytes, index);
        }

        //public TmosWorldScreenTiles LoadWorldScreenTiles(TmosWorldScreen ws)
        //{
        //    return TmosData.GetWorldScreenTiles(Bytes, ws);
        //}


    


    }
}
