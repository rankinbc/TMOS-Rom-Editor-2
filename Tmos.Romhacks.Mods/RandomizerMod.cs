using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmos.Romhacks.Core;

namespace Tmos.Romhacks.Mods
{
    public class RandomizerMod
    {
          TmosRom _romData;
          public RandomizerModWorldScreen[] WorldScreens { get; set; }

           public TmosTileSection[] TileSections { get; set; }

        public byte[] TileData { get; set; } //Entire tile data section from rom

        public RandomizerMod()
        {

        }

        public void LoadDataFromRom(TmosRom rom)
        {
            _romData = rom;
            
            LoadWorldScreensFromRom(_romData);
            LoadTileDataFromRom(_romData);
            LoadTileSectionsFromRom(_romData);
        }

        private int GetBottomTileDataOffset(byte dataPointer)
        {
            int bottomTileDataOffset = 0;
            int topTileDataOffset = 0;
            if (dataPointer >= 0x40 && dataPointer < 0x8f)
            {
                bottomTileDataOffset = 0x2000;
                topTileDataOffset = 0x0000;
            }

            else if (dataPointer >= 0x8f && dataPointer < 0xA0)
            {
                bottomTileDataOffset = 0x0000;
                topTileDataOffset = 0x2000;
            }
            else if (dataPointer >= 0xC0)
            {
                topTileDataOffset = 0x2000;
                bottomTileDataOffset = 0x2000;
            }
            return bottomTileDataOffset;
        }
        private int GetTopTileDataOffset(byte dataPointer)
        {
            int bottomTileDataOffset = 0;
            int topTileDataOffset = 0;
            if (dataPointer >= 0x40 && dataPointer < 0x8f)
            {
                bottomTileDataOffset = 0x2000;
                topTileDataOffset = 0x0000;
            }

            else if (dataPointer >= 0x8f && dataPointer < 0xA0)
            {
                bottomTileDataOffset = 0x0000;
                topTileDataOffset = 0x2000;
            }
            else if (dataPointer >= 0xC0)
            {
                topTileDataOffset = 0x2000;
                bottomTileDataOffset = 0x2000;
            }
            return topTileDataOffset;
        }

        public void UpdateWorldScreenTopTileSection(int worldScreenIndex, int tileSectionIndex)
        {
            //
            RandomizerModWorldScreen ws = WorldScreens[worldScreenIndex];

            //Get Tile Data Offset

   

            ws.TopTiles = (byte)tileSectionIndex;
            ws.TileSectionTop = _romData.LoadTileSection(ws.TopTiles, GetTopTileDataOffset(ws.DataPointer));
        }
        public void UpdateWorldScreenBottomTileSection(int worldScreenIndex, int tileSectionIndex)
        {
            RandomizerModWorldScreen ws = WorldScreens[worldScreenIndex];
            ws.BottomTiles = (byte)tileSectionIndex;
            ws.TileSectionBottom = _romData.LoadTileSection(ws.BottomTiles, GetBottomTileDataOffset(ws.DataPointer));
        }

      

        public void LoadWorldScreensFromRom(TmosRom tmosRom)
        {
            WorldScreens = new RandomizerModWorldScreen[TmosData.DataStructureCounts.WorldScreen_Count];

    

            //Now we have data from the rom
            
            //Convert the TmosWorldScreens into RandomizerModWorldScreens (which will hopefully be less tied to the lower level details)
            for (int i = 0; i < WorldScreens.Length; i++)
            {
                TmosWorldScreen tmosWorldScreen = tmosRom.LoadWorldScreen(i);

                RandomizerModWorldScreen ws = new RandomizerModWorldScreen()
                {
                    WSContent = RandomizerModWorldScreen.GetContentFromValue(tmosWorldScreen.Content),
                

                    //Loading the original byte values into RandomizeModWorldScreen just for now, even though eventually it should have interactive types like the above values rather than byte values.
                    ParentWorld = tmosWorldScreen.ParentWorld, //music and some other things
                    AmbientSound = tmosWorldScreen.AmbientSound,
                  //  Content = tmosWorldScreen.Content, //Just used this value to create the WSContent object, so kind of reduntant putting it in the RandomizerModWorldScreenObject
                    ObjectSet = tmosWorldScreen.ObjectSet,
                    ScreenIndexRight = tmosWorldScreen.ScreenIndexRight,
                    ScreenIndexLeft = tmosWorldScreen.ScreenIndexLeft,
                    ScreenIndexDown = tmosWorldScreen.ScreenIndexDown,
                    ScreenIndexUp = tmosWorldScreen.ScreenIndexUp,
                    DataPointer = tmosWorldScreen.DataPointer,
                    ExitPosition = tmosWorldScreen.ExitPosition,
                    TopTiles = tmosWorldScreen.TopTiles,//Just used this value to create the TileSectionTop object, so kind of reduntant putting it in
                    BottomTiles = tmosWorldScreen.BottomTiles,//Just used this value to create the TileSectionBottom object, so kind of reduntant putting it in 
                    WorldScreenColor = tmosWorldScreen.WorldScreenColor,
                    SpritesColor = tmosWorldScreen.SpritesColor,
                    Unknown = tmosWorldScreen.Unknown,
                    Event = tmosWorldScreen.Event,

                };

                int bottomTileDataOffset = 0;
                int topTileDataOffset = 0;
                if (ws.DataPointer >= 0x40 && ws.DataPointer < 0x8f)
                {
                    bottomTileDataOffset = 0x2000;
                    topTileDataOffset = 0x0000;
                }

                else if (ws.DataPointer >= 0x8f && ws.DataPointer < 0xA0)
                {
                    bottomTileDataOffset = 0x0000;
                    topTileDataOffset = 0x2000;
                }
                else if (ws.DataPointer >= 0xC0)
                {
                    topTileDataOffset = 0x2000;
                    bottomTileDataOffset = 0x2000;
                }


                ws.TileSectionTop = _romData.LoadTileSection(ws.TopTiles , topTileDataOffset);
                ws.TileSectionBottom = _romData.LoadTileSection(ws.BottomTiles, bottomTileDataOffset );

                WorldScreens[i] = ws;
            }
        }

        public void LoadTileDataFromRom(TmosRom tmosRom)
        {
            TileData = tmosRom.LoadTileData();
        }

            public static RandomizerModWorldScreen TmosWorldScreenToRandomizerWorldScreen(TmosWorldScreen tmosWorldScreen)
        {
            RandomizerModWorldScreen ws = new RandomizerModWorldScreen()
            {
                WSContent = RandomizerModWorldScreen.GetContentFromValue(tmosWorldScreen.Content),

                //Loading the original byte values into RandomizeModWorldScreen just for now, even though eventually it should have interactive types like the above values rather than byte values.
                ParentWorld = tmosWorldScreen.ParentWorld, //music and some other things
                AmbientSound = tmosWorldScreen.AmbientSound,
                //  Content = tmosWorldScreen.Content, //Just used this value to create the WSContent object, so kind of reduntant putting it in the RandomizerModWorldScreenObject
                ObjectSet = tmosWorldScreen.ObjectSet,
                ScreenIndexRight = tmosWorldScreen.ScreenIndexRight,
                ScreenIndexLeft = tmosWorldScreen.ScreenIndexLeft,
                ScreenIndexDown = tmosWorldScreen.ScreenIndexDown,
                ScreenIndexUp = tmosWorldScreen.ScreenIndexUp,
                DataPointer = tmosWorldScreen.DataPointer,
                ExitPosition = tmosWorldScreen.ExitPosition,
                TopTiles = tmosWorldScreen.TopTiles,//Just used this value to create the TileSectionTop object, so kind of reduntant putting it in
                BottomTiles = tmosWorldScreen.BottomTiles,//Just used this value to create the TileSectionBottom object, so kind of reduntant putting it in 
                WorldScreenColor = tmosWorldScreen.WorldScreenColor,
                SpritesColor = tmosWorldScreen.SpritesColor,
                Unknown = tmosWorldScreen.Unknown,
                Event = tmosWorldScreen.Event,

            };
            return ws;
        }

        public static TmosWorldScreen RandomizerWorldScreenToTmosWorldScreen(RandomizerModWorldScreen randomizerWorldScreen)
        {
            byte[] wsData = new byte[]
            {
                randomizerWorldScreen.ParentWorld,
            randomizerWorldScreen.AmbientSound,
            RandomizerModWorldScreen.GetContentValue(randomizerWorldScreen.WSContent),
            randomizerWorldScreen.ObjectSet,
            randomizerWorldScreen.ScreenIndexRight,
            randomizerWorldScreen.ScreenIndexLeft,
            randomizerWorldScreen.ScreenIndexDown,
            randomizerWorldScreen.ScreenIndexUp,
            randomizerWorldScreen.DataPointer,
            randomizerWorldScreen.ExitPosition,
            randomizerWorldScreen.TopTiles,
            randomizerWorldScreen.BottomTiles,
            randomizerWorldScreen.WorldScreenColor,
            randomizerWorldScreen.SpritesColor,
            randomizerWorldScreen.Unknown,
           randomizerWorldScreen.Event
            };
            TmosWorldScreen ws = new TmosWorldScreen(wsData);
            return ws;
        }

        public void LoadTileSectionsFromRom(TmosRom tmosRom)
        {
          //  TileSections = new TmosTileSection[TmosData.DataStructureCounts.TileSection_Count];
            TileSections = new TmosTileSection[255];

            for (int i = 0; i < TileSections.Length; i++)
            {
                TmosTileSection tmosTileSection = tmosRom.LoadTileSection(i,0);
                TileSections[i] = tmosTileSection;
            }
        }

        public void Randomize()
        {
            //Randomize the data of WorldScreens

        }



    }
}
