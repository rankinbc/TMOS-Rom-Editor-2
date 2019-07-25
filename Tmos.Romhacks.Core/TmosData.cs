using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmos.Romhacks.Core
{
    public static class TmosData
    {


        public static class DataStructureCounts
        {
            public static int WorldScreenDataOffsets_Count = 5;
            public static int WorldScreen_Count = 739;
            public static int TileSection_Count = 940;
         
        }

        public static class DataStructures
        {
            public static TmosDataStructure WorldScreenDataOffsets = new TmosDataStructure(0x03968b, 2);
            public static TmosDataStructure WorldScreens = new TmosDataStructure(0x039695, 16);
            public static TmosDataStructure TileSection = new TmosDataStructure(0x03c4c7, 32);
            public static TmosDataStructure Tile = new TmosDataStructure(0x011b0b, 4);
            public static TmosDataStructure MiniTile = new TmosDataStructure(0x01160b, 4); //4 make up a tile (collision data based on this)
            public static TmosDataStructure AllTileData = new TmosDataStructure(0x03C4C7, 0x3AC1); //The entire section of tile data for the rom
        }

        public static TmosWorldScreenDataOffset GetWorldScreenDataOffset(byte[] rom, int index)
        {
            byte[] selectedData = GetDataStructure(rom, DataStructures.WorldScreenDataOffsets, index,0);
            TmosWorldScreenDataOffset worldScreenDataOffset = new TmosWorldScreenDataOffset(selectedData);
            return worldScreenDataOffset;
        }

        public static TmosWorldScreen GetWorldScreen(byte[] rom, int index)
        {
            byte[] selectedData = GetDataStructure(rom, DataStructures.WorldScreens, index,0);
            TmosWorldScreen worldScreen = new TmosWorldScreen(selectedData);
            return worldScreen;
        }

        public static void SaveWorldScreen(byte[] rom, int index, TmosWorldScreen worldScreen)
        {
            SaveDataStructure(rom, DataStructures.WorldScreens, index, worldScreen._data);
        }

        public static TmosTileSection GetTileSection(byte[] rom, int index, int offset)
        {
            byte[] selectedData = GetDataStructure(rom, DataStructures.TileSection, index ,offset);
            TmosTileSection tileSection = new TmosTileSection(selectedData);
    
            return tileSection;
        }

        public static TmosTile GetTile(byte[] rom, int index)
        {
            byte[] selectedData = GetDataStructure(rom, DataStructures.Tile, index,0);
            TmosTile tile = new TmosTile(selectedData);
            return tile;
        }

        public static TmosMiniTile GetMiniTile(byte[] rom, int index)
        {
            byte[] selectedData = GetDataStructure(rom, DataStructures.MiniTile, index,0);
            TmosMiniTile tile = new TmosMiniTile(selectedData);
            return tile;
        }

        public static byte[] GetTileData(byte[] rom)
        {
            byte[] selectedData = GetDataStructure(rom, DataStructures.AllTileData,0);
            return selectedData;
        }


        public static TmosWorldScreenTiles GetWorldScreenTiles(byte[] rom, TmosWorldScreen ws)
        {
            int bottomTileDataStartIndex = 0;
            int topTileDataStartIndex = 0;

            if (ws.DataPointer >= 0x40 && ws.DataPointer < 0x8f)
            {
                bottomTileDataStartIndex = 0x2000 / TmosData.DataStructures.TileSection.Length;
                topTileDataStartIndex = 0;
            }

            else if (ws.DataPointer >= 0x8f && ws.DataPointer < 0xA0)
            {
                bottomTileDataStartIndex = 0;
                topTileDataStartIndex = 0x2000 / TmosData.DataStructures.TileSection.Length;
            }
            else if (ws.DataPointer >= 0xC0)
            {
                topTileDataStartIndex = 0x2000 / TmosData.DataStructures.TileSection.Length;
                bottomTileDataStartIndex = 0x2000 / TmosData.DataStructures.TileSection.Length;
            }

            TmosWorldScreenTiles wsTiles = new TmosWorldScreenTiles();
            wsTiles.TopTiles = GetTileSection(rom, ws.TopTiles, topTileDataStartIndex);
            wsTiles.BottomTiles = GetTileSection(rom, ws.BottomTiles, bottomTileDataStartIndex);

           


            return wsTiles;

        }




        private static byte[] GetDataStructure(byte[] bytes, TmosDataStructure dataStructure, int index)
        {
            return GetDataStructure(bytes, dataStructure, index, 0);
        }
        private static byte[] GetDataStructure(byte[] bytes, TmosDataStructure dataStructure, int index, int byteOffset)
        {
            byte[] structure = new byte[dataStructure.Length];
            int sourceOffset = dataStructure.Address + (dataStructure.Length * index) + byteOffset;
            Array.Copy(bytes, sourceOffset, structure, 0, dataStructure.Length);
            return structure;
        }

        private static void SaveDataStructure(byte[] bytes, TmosDataStructure dataStructure, int index, byte[] structureByteContent)
        {
            
            byte[] structure = new byte[dataStructure.Length];
            int sourceOffset = dataStructure.Address + (dataStructure.Length * index);

            Array.Copy(structureByteContent, 0, bytes, sourceOffset, dataStructure.Length);
        }
    }


}

