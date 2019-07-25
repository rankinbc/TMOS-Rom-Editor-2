using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmos.Romhacks.Core;

namespace Tmos.Romhacks.Mods
{
    public class RandomizerModWorldScreen
    {
        //    private TmosWorldScreen _tmosWorldScreen;

        public Content WSContent { get; set; }
        public TmosTileSection TileSectionTop { get; set; }
        public TmosTileSection TileSectionBottom { get; set; }


        //These will hopefully be made into usable objects like the ones above, instead of a byte value, but for now just loading up the byte value straight from the ROM data
        public byte ParentWorld { get; set; }
        public byte AmbientSound { get; set; }
      //  public byte Content { get; set; } 
        public byte ObjectSet { get; set; } 
        public byte ScreenIndexRight { get; set; }
        public byte ScreenIndexLeft { get; set; }
        public byte ScreenIndexDown { get; set; }
        public byte ScreenIndexUp { get; set; }
        public byte DataPointer { get; set; }
        public byte ExitPosition { get; set; }
        public byte TopTiles { get; set; }
        public byte BottomTiles { get; set; } 
        public byte WorldScreenColor { get; set; }
        public byte SpritesColor { get; set; }
        public byte Unknown { get; set; }
        public byte Event { get; set; } 



        public RandomizerModWorldScreen()
        {

        }


        #region Directional Tests

        public bool CollisionTest_Right_IsCompatable(RandomizerModWorldScreen destinationWS)
        {
            if (ScreenIndexRight == 0xFF) return true; 


            //take the edge of this screen's grid, and compare it to the opposite edge of the destinationWS 

            Tile[,] tileGrid = GetTileGrid();
            Tile[,] destinationTileGrid = destinationWS.GetTileGrid();

            Tile[] tileGrid_RightEdge = new Tile[6];
            for (int y = 0; y < tileGrid.GetLength(1); y++)
            {
                tileGrid_RightEdge[y] = tileGrid[7, y];
            }

            Tile[] destinationTileGrid_LeftEdge = new Tile[6];
            for (int y = 0; y < tileGrid.GetLength(1); y++)
            {
                destinationTileGrid_LeftEdge[y] = destinationTileGrid[0, y];
            }

            bool[] compatableCollisionTiles = new bool[6];
            for (int y = 0; y < tileGrid.GetLength(1); y++)
            {
                if (TileIsWalkable(tileGrid_RightEdge[y]) == TileIsWalkable(destinationTileGrid_LeftEdge[y]))
                {
                    compatableCollisionTiles[y] = true;
                }
            }

            //Now we have a bool array indicating which squares are compatable with the other screen. Use this to decide if the screens are compatable

                return AllBoolsAreTrue(compatableCollisionTiles);

        }

        public bool CollisionTest_Left_IsCompatable(RandomizerModWorldScreen destinationWS)
        {
            if (ScreenIndexLeft == 0xFF) return true;
            //take the edge of this screen's grid, and compare it to the opposite edge of the destinationWS 

            Tile[,] tileGrid = GetTileGrid();
            Tile[,] destinationTileGrid = destinationWS.GetTileGrid();

            Tile[] tileGrid_LeftEdge = new Tile[6];
            for (int y = 0; y < tileGrid.GetLength(1); y++)
            {
                tileGrid_LeftEdge[y] = tileGrid[0, y];
            }

            Tile[] destinationTileGrid_RightEdge = new Tile[6];
            for (int y = 0; y < tileGrid.GetLength(1); y++)
            {
                destinationTileGrid_RightEdge[y] = destinationTileGrid[7, y];
            }

            bool[] compatableCollisionTiles = new bool[6];
            for (int y = 0; y < tileGrid.GetLength(1); y++)
            {
                if (TileIsWalkable(tileGrid_LeftEdge[y]) == TileIsWalkable(destinationTileGrid_RightEdge[y]))
                {
                    compatableCollisionTiles[y] = true;
                }
            }

            //Now we have a bool array indicating which squares are compatable with the other screen. Use this to decide if the screens are compatable

            return AllBoolsAreTrue(compatableCollisionTiles);

        }

        public bool CollisionTest_Up_IsCompatable(RandomizerModWorldScreen destinationWS)
        {
            if (ScreenIndexUp == 0xFF) return true;
            //take the edge of this screen's grid, and compare it to the opposite edge of the destinationWS 

            Tile[,] tileGrid = GetTileGrid();
            Tile[,] destinationTileGrid = destinationWS.GetTileGrid();

            Tile[] tileGrid_TopEdge = new Tile[8];
            for (int x = 0; x < tileGrid.GetLength(0); x++)
            {
                tileGrid_TopEdge[x] = tileGrid[x, 0];
            }

            Tile[] destinationTileGrid_BottomEdge = new Tile[8];
            for (int x = 0; x < tileGrid.GetLength(0); x++)
            {
                destinationTileGrid_BottomEdge[x] = destinationTileGrid[x, 5];
            }

            bool[] compatableCollisionTiles = new bool[8];
            for (int x = 0; x < tileGrid.GetLength(0); x++)
            {
                if (TileIsWalkable(tileGrid_TopEdge[x]) == TileIsWalkable(destinationTileGrid_BottomEdge[x]))
                {
                    compatableCollisionTiles[x] = true;
                }
            }

            //Now we have a bool array indicating which squares are compatable with the other screen. Use this to decide if the screens are compatable

            return AllBoolsAreTrue(compatableCollisionTiles);

        }

        public bool CollisionTest_Down_IsCompatable(RandomizerModWorldScreen destinationWS)
        {
            if (ScreenIndexDown == 0xFF) return true;
            //take the edge of this screen's grid, and compare it to the opposite edge of the destinationWS 

            Tile[,] tileGrid = GetTileGrid();
            Tile[,] destinationTileGrid = destinationWS.GetTileGrid();

            Tile[] tileGrid_BottomEdge = new Tile[8];
            for (int x = 0; x < tileGrid.GetLength(0); x++)
            {
                tileGrid_BottomEdge[x] = tileGrid[x, 5];
            }

            Tile[] destinationTileGrid_TopEdge = new Tile[8];
            for (int x = 0; x < tileGrid.GetLength(0); x++)
            {
                destinationTileGrid_TopEdge[x] = destinationTileGrid[x, 0];
            }

            bool[] compatableCollisionTiles = new bool[8];
            for (int x = 0; x < tileGrid.GetLength(0); x++)
            {
                if (TileIsWalkable(tileGrid_BottomEdge[x]) == TileIsWalkable(destinationTileGrid_TopEdge[x]))
                {
                    compatableCollisionTiles[x] = true;
                }
            }

            //Now we have a bool array indicating which squares are compatable with the other screen. Use this to decide if the screens are compatable

            return AllBoolsAreTrue(compatableCollisionTiles);

        }

        private static bool AllBoolsAreTrue(bool[] boolArray)
        {
            foreach (bool b in boolArray)
            {
                if (!b)
                {
                    return false;
                }
            }
            return true;
        }


        #endregion Directional Tests
     



        #region Content

        public string GetContentName()
        {
            switch (WSContent)
            {
                case Content.Mosque: return "Mosque";
                case Content.FirstPriest: return "First Priest";
                case Content.Troopers: return "Troopers";
                case Content.Casino: return "Casino";
                case Content.TimeDoor: return "TimeDoor";
                case Content.University: return "University";
                case Content.RSeedPlant: return "RSeedPlant";
                case Content.RSeedInfo: return "RSeedInfo";

                case Content.WizardBattleOnEnter: return "WizardBattleOnEnter";
                case Content.DialogScreenEntranceOnScreen: return "DialogScreenEntranceOnScreen";
                case Content.EncounterPossibleOnExitFlag: return "EncounterPossibleOnExitFlag";
                case Content.Shop1: return "Shop1";
                case Content.Shop2: return "Shop2";
                case Content.Shop3: return "Shop3";


                default: return "UNKNOWN";
            }
        }

        public byte GetContentValue()
        {
            return GetContentValue(WSContent);
        }

        public static byte GetContentValue(Content content)
        {
            switch (content)
            {
                case Content.Mosque: return 0x7E;
                case Content.FirstPriest: return 0x20;
                case Content.Troopers: return 0x7F;
                case Content.Casino: return 0xBE;
                case Content.TimeDoor: return 0xC0;
                case Content.University: return 0x40;
                case Content.RSeedPlant: return 0xBC;
                case Content.RSeedInfo: return 0xBD;



                case Content.WizardBattleOnEnter: return 0x01;
                case Content.DialogScreenEntranceOnScreen: return 0xFE;
                case Content.EncounterPossibleOnExitFlag: return 0xFF;
                case Content.Shop1: return 0x60;
                case Content.Shop2: return 0x61;
                case Content.Shop3: return 0x62;



                case Content.Gilga: return 0x21;
                case Content.Gilga2: return 0x22;
                case Content.Curly: return 0x23;
                case Content.Curly2: return 0x24;
                case Content.Troll: return 0x25;
                case Content.Troll2: return 0x26;
                case Content.Salamander: return 0x27;
                case Content.Salamander2: return 0x28;
                case Content.GoraGora: return 0x29;
                case Content.GoraGora2: return 0x2A;
                default: return 0x00;
            }
        }

        public static Content GetContentFromValue(byte value)
        {
            switch (value)
            {
                case 0x7E: return Content.Mosque;
                case 0x20: return Content.FirstPriest;
                case 0x7F: return Content.Troopers;
                case 0xBE: return Content.Casino;
                case 0xC0: return Content.TimeDoor;
                case 0x40: return Content.University;
                case 0xBC: return Content.RSeedPlant;
                case 0xBD: return Content.RSeedInfo;
                case 0x01: return Content.WizardBattleOnEnter;
                case 0xFE: return Content.DialogScreenEntranceOnScreen;
                case 0xFF: return Content.EncounterPossibleOnExitFlag;

                case 0x60: return Content.Shop1;
                case 0x61: return Content.Shop2;
                case 0x62: return Content.Shop3;

                case 0x21: return Content.Gilga;
                case 0x22: return Content.Gilga2;
                case 0x23: return Content.Curly;
                case 0x24: return Content.Curly2;
                case 0x25: return Content.Troll;
                case 0x26: return Content.Troll2;
                case 0x27: return Content.Salamander;
                case 0x28: return Content.Salamander2;
                case 0x29: return Content.GoraGora;
                case 0x2A: return Content.GoraGora2;

               


              
                default: return Content.UNKNOWN;
            }
        }

        public enum Content
        {
            Mosque,
            FirstPriest,
            Troopers,
            Casino,
            TimeDoor,
            University,
            RSeedPlant,
            RSeedInfo,
            WizardBattleOnEnter,
            DialogScreenEntranceOnScreen,
            EncounterPossibleOnExitFlag,
            Shop1,
            Shop2,
            Shop3,

            Gilga,
            Gilga2,
            Curly,
            Curly2,
            Troll,
            Troll2,
            Salamander,
            Salamander2,
            GoraGora,
            GoraGora2,

            //Ch1
            Faruk,
            Kebabu,
            AquaPalace,
            WiseManMonecom,
            AchelatoPrincess,
            Sabaron,

            //Ch2
            GunMeca,
            Lah,
            Supica,
            Epin,
            WisemanRaincome,
            Princess,

            //Ch3
            NewBornCimaronTree,
            CimaronTree,
            Supapa,
            Mustafa,
            FrozenPalace,

            //Ch4
            Gubibi,
            Rainy,

            YuflaPalace,
            Rostam,
            RostamInfo,
            KingFiesal,
            WisemanMoscom,

            //Ch5
            Hasan,

            Kaji,
            LegendSword,
            ArmorofLight,
            PalaceEntrance,
            SabaronFinal,
            JarHint,
            Libcom,
            Rupias,


            UNKNOWN
        }

        #endregion Content


        #region Tiles

        public Tile[,] GetTileGrid()
        {
            byte[,] topTileGrid = TileSectionTop.GetTileSectionGrid();
            byte[,] bottomTileGrid = TileSectionBottom.GetTileSectionGrid();

            Tile[,] fullGrid = new Tile[8, 6];   //only 2 rows used from bottom grid

            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (y < 4)
                    {
                        byte tileValue = topTileGrid[x, y];
                        Tile tile = GetTileFromValue(tileValue);
                        fullGrid[x, y] = tile;
                    }
                    else
                    {
                        byte tileValue = bottomTileGrid[x, y - 4];
                        Tile tile = GetTileFromValue(tileValue);
                        fullGrid[x, y] = tile;
                    }

                }
            }
            return fullGrid;

        }

        public static bool TileIsWalkable(Tile tile)
        {
            switch (tile)
            {
                case Tile.Grass: return true;
                case Tile.Desert : return true;
                case Tile.Water: return true;
                case Tile.WaterTopEdge: return true;
                case Tile.Lava: return true;

                case Tile.Tree: return false;
                case Tile.DesertTrees: return false;


                default: return true;
            }

        }

        public static string GetTileName(Tile tile)
        {
            switch (tile)
            {
                case Tile.Grass: return "Grass";

                case Tile.Tree: return "Tree";
                case Tile.Water: return "Water";
                case Tile.WaterTopEdge: return "WaterTopEdge";
                case Tile.Desert: return "Desert";
                case Tile.DesertTrees: return "Desert Trees";
                case Tile.Lava: return "Lava";
                default: return "UNKNOWN";
            }
        }

        public static byte GetTileValue(Tile tile)
        {
            switch (tile)
            {
                case Tile.Grass: return 0x46;
                case Tile.Tree: return 0x47;
                case Tile.Water: return 0x3F;
                case Tile.WaterTopEdge: return 0x6F;
                case Tile.Desert: return 0x43;
                case Tile.DesertTrees: return 0x23;
                case Tile.Lava: return 0x42;

                case Tile.Tile0x00: return 0x00;
                case Tile.Tile0x01: return 0x01;
                case Tile.Tile0x02: return 0x02;
                case Tile.Tile0x03: return 0x03;
                case Tile.Tile0x04: return 0x04;
                case Tile.Tile0x05: return 0x05;
                case Tile.Tile0x06: return 0x06;
                case Tile.Tile0x07: return 0x07;
                case Tile.Tile0x08: return 0x08;
                case Tile.Tile0x09: return 0x09;
                case Tile.Tile0x0A: return 0x0A;
                case Tile.Tile0x0B: return 0x0B;
                case Tile.Tile0x0C: return 0x0C;
                case Tile.Tile0x0D: return 0x0D;
                case Tile.Tile0x0E: return 0x0E;
                case Tile.Tile0x0F: return 0x0F;
                case Tile.Tile0x10: return 0x10;
                case Tile.Tile0x11: return 0x11;
                case Tile.Tile0x12: return 0x12;
                case Tile.Tile0x13: return 0x13;
                case Tile.Tile0x14: return 0x14;
                case Tile.Tile0x15: return 0x15;
                case Tile.Tile0x16: return 0x16;
                case Tile.Tile0x17: return 0x17;
                case Tile.Tile0x18: return 0x18;
                case Tile.Tile0x19: return 0x19;
                case Tile.Tile0x1A: return 0x1A;
                case Tile.Tile0x1B: return 0x1B;
                case Tile.Tile0x1C: return 0x1C;
                case Tile.Tile0x1D: return 0x1D;
                case Tile.Tile0x1E: return 0x1E;
                case Tile.Tile0x1F: return 0x1F;
                case Tile.Tile0x20: return 0x20;
                case Tile.Tile0x21: return 0x21;
                case Tile.Tile0x22: return 0x22;
                case Tile.Tile0x23: return 0x23;
                case Tile.Tile0x24: return 0x24;
                case Tile.Tile0x25: return 0x25;
                case Tile.Tile0x26: return 0x26;
                case Tile.Tile0x27: return 0x27;
                case Tile.Tile0x28: return 0x28;
                case Tile.Tile0x29: return 0x29;
                case Tile.Tile0x2A: return 0x2A;
                case Tile.Tile0x2B: return 0x2B;
                case Tile.Tile0x2C: return 0x2C;
                case Tile.Tile0x2D: return 0x2D;
                case Tile.Tile0x2E: return 0x2E;
                case Tile.Tile0x2F: return 0x2F;
                case Tile.Tile0x30: return 0x30;
                case Tile.Tile0x31: return 0x31;
                case Tile.Tile0x32: return 0x32;
                case Tile.Tile0x33: return 0x33;
                case Tile.Tile0x34: return 0x34;
                case Tile.Tile0x35: return 0x35;
                case Tile.Tile0x36: return 0x36;
                case Tile.Tile0x37: return 0x37;
                case Tile.Tile0x38: return 0x38;
                case Tile.Tile0x39: return 0x39;
                case Tile.Tile0x3A: return 0x3A;
                case Tile.Tile0x3B: return 0x3B;
                case Tile.Tile0x3C: return 0x3C;
                case Tile.Tile0x3D: return 0x3D;
                case Tile.Tile0x3E: return 0x3E;
                case Tile.Tile0x3F: return 0x3F;
                case Tile.Tile0x40: return 0x40;
                case Tile.Tile0x41: return 0x41;
                case Tile.Tile0x42: return 0x42;
                case Tile.Tile0x43: return 0x43;
                case Tile.Tile0x44: return 0x44;
                case Tile.Tile0x45: return 0x45;
                case Tile.Tile0x46: return 0x46;
                case Tile.Tile0x47: return 0x47;
                case Tile.Tile0x48: return 0x48;
                case Tile.Tile0x49: return 0x49;
                case Tile.Tile0x4A: return 0x4A;
                case Tile.Tile0x4B: return 0x4B;
                case Tile.Tile0x4C: return 0x4C;
                case Tile.Tile0x4D: return 0x4D;
                case Tile.Tile0x4E: return 0x4E;
                case Tile.Tile0x4F: return 0x4F;
                case Tile.Tile0x50: return 0x50;
                case Tile.Tile0x51: return 0x51;
                case Tile.Tile0x52: return 0x52;
                case Tile.Tile0x53: return 0x53;
                case Tile.Tile0x54: return 0x54;
                case Tile.Tile0x55: return 0x55;
                case Tile.Tile0x56: return 0x56;
                case Tile.Tile0x57: return 0x57;
                case Tile.Tile0x58: return 0x58;
                case Tile.Tile0x59: return 0x59;
                case Tile.Tile0x5A: return 0x5A;
                case Tile.Tile0x5B: return 0x5B;
                case Tile.Tile0x5C: return 0x5C;
                case Tile.Tile0x5D: return 0x5D;
                case Tile.Tile0x5E: return 0x5E;
                case Tile.Tile0x5F: return 0x5F;
                case Tile.Tile0x60: return 0x60;
                case Tile.Tile0x61: return 0x61;
                case Tile.Tile0x62: return 0x62;
                case Tile.Tile0x63: return 0x63;
                case Tile.Tile0x64: return 0x64;
                case Tile.Tile0x65: return 0x65;
                case Tile.Tile0x66: return 0x66;
                case Tile.Tile0x67: return 0x67;
                case Tile.Tile0x68: return 0x68;
                case Tile.Tile0x69: return 0x69;
                case Tile.Tile0x6A: return 0x6A;
                case Tile.Tile0x6B: return 0x6B;
                case Tile.Tile0x6C: return 0x6C;
                case Tile.Tile0x6D: return 0x6D;
                case Tile.Tile0x6E: return 0x6E;
                case Tile.Tile0x6F: return 0x6F;
                case Tile.Tile0x70: return 0x70;
                case Tile.Tile0x71: return 0x71;
                case Tile.Tile0x72: return 0x72;
                case Tile.Tile0x73: return 0x73;
                case Tile.Tile0x74: return 0x74;
                case Tile.Tile0x75: return 0x75;
                case Tile.Tile0x76: return 0x76;
                case Tile.Tile0x77: return 0x77;
                case Tile.Tile0x78: return 0x78;
                case Tile.Tile0x79: return 0x79;
                case Tile.Tile0x7A: return 0x7A;
                case Tile.Tile0x7B: return 0x7B;
                case Tile.Tile0x7C: return 0x7C;
                case Tile.Tile0x7D: return 0x7D;
                case Tile.Tile0x7E: return 0x7E;
                case Tile.Tile0x7F: return 0x7F;
                case Tile.Tile0x80: return 0x80;
                case Tile.Tile0x81: return 0x81;
                case Tile.Tile0x82: return 0x82;
                case Tile.Tile0x83: return 0x83;
                case Tile.Tile0x84: return 0x84;
                case Tile.Tile0x85: return 0x85;
                case Tile.Tile0x86: return 0x86;
                case Tile.Tile0x87: return 0x87;
                case Tile.Tile0x88: return 0x88;
                case Tile.Tile0x89: return 0x89;
                case Tile.Tile0x8A: return 0x8A;
                case Tile.Tile0x8B: return 0x8B;
                case Tile.Tile0x8C: return 0x8C;
                case Tile.Tile0x8D: return 0x8D;
                case Tile.Tile0x8E: return 0x8E;
                case Tile.Tile0x8F: return 0x8F;
                case Tile.Tile0x90: return 0x90;
                case Tile.Tile0x91: return 0x91;
                case Tile.Tile0x92: return 0x92;
                case Tile.Tile0x93: return 0x93;
                case Tile.Tile0x94: return 0x94;
                case Tile.Tile0x95: return 0x95;
                case Tile.Tile0x96: return 0x96;
                case Tile.Tile0x97: return 0x97;
                case Tile.Tile0x98: return 0x98;
                case Tile.Tile0x99: return 0x99;
                case Tile.Tile0x9A: return 0x9A;
                case Tile.Tile0x9B: return 0x9B;
                case Tile.Tile0x9C: return 0x9C;
                case Tile.Tile0x9D: return 0x9D;
                case Tile.Tile0x9E: return 0x9E;
                case Tile.Tile0x9F: return 0x9F;
                case Tile.Tile0xA0: return 0xA0;
                case Tile.Tile0xA1: return 0xA1;
                case Tile.Tile0xA2: return 0xA2;
                case Tile.Tile0xA3: return 0xA3;
                case Tile.Tile0xA4: return 0xA4;
                case Tile.Tile0xA5: return 0xA5;
                case Tile.Tile0xA6: return 0xA6;
                case Tile.Tile0xA7: return 0xA7;
                case Tile.Tile0xA8: return 0xA8;
                case Tile.Tile0xA9: return 0xA9;
                case Tile.Tile0xAA: return 0xAA;
                case Tile.Tile0xAB: return 0xAB;
                case Tile.Tile0xAC: return 0xAC;
                case Tile.Tile0xAD: return 0xAD;
                case Tile.Tile0xAE: return 0xAE;
                case Tile.Tile0xAF: return 0xAF;
                case Tile.Tile0xB0: return 0xB0;
                case Tile.Tile0xB1: return 0xB1;
                case Tile.Tile0xB2: return 0xB2;
                case Tile.Tile0xB3: return 0xB3;
                case Tile.Tile0xB4: return 0xB4;
                case Tile.Tile0xB5: return 0xB5;
                case Tile.Tile0xB6: return 0xB6;
                case Tile.Tile0xB7: return 0xB7;
                case Tile.Tile0xB8: return 0xB8;
                case Tile.Tile0xB9: return 0xB9;
                case Tile.Tile0xBA: return 0xBA;
                case Tile.Tile0xBB: return 0xBB;
                case Tile.Tile0xBC: return 0xBC;
                case Tile.Tile0xBD: return 0xBD;
                case Tile.Tile0xBE: return 0xBE;
                case Tile.Tile0xBF: return 0xBF;
                case Tile.Tile0xC0: return 0xC0;
                case Tile.Tile0xC1: return 0xC1;
                case Tile.Tile0xC2: return 0xC2;
                case Tile.Tile0xC3: return 0xC3;
                case Tile.Tile0xC4: return 0xC4;
                case Tile.Tile0xC5: return 0xC5;
                case Tile.Tile0xC6: return 0xC6;
                case Tile.Tile0xC7: return 0xC7;
                case Tile.Tile0xC8: return 0xC8;
                case Tile.Tile0xC9: return 0xC9;
                case Tile.Tile0xCA: return 0xCA;
                case Tile.Tile0xCB: return 0xCB;
                case Tile.Tile0xCC: return 0xCC;
                case Tile.Tile0xCD: return 0xCD;
                case Tile.Tile0xCE: return 0xCE;
                case Tile.Tile0xCF: return 0xCF;
                case Tile.Tile0xD0: return 0xD0;
                case Tile.Tile0xD1: return 0xD1;
                case Tile.Tile0xD2: return 0xD2;
                case Tile.Tile0xD3: return 0xD3;
                case Tile.Tile0xD4: return 0xD4;
                case Tile.Tile0xD5: return 0xD5;
                case Tile.Tile0xD6: return 0xD6;
                case Tile.Tile0xD7: return 0xD7;
                case Tile.Tile0xD8: return 0xD8;
                case Tile.Tile0xD9: return 0xD9;
                case Tile.Tile0xDA: return 0xDA;
                case Tile.Tile0xDB: return 0xDB;
                case Tile.Tile0xDC: return 0xDC;
                case Tile.Tile0xDD: return 0xDD;
                case Tile.Tile0xDE: return 0xDE;
                case Tile.Tile0xDF: return 0xDF;
                case Tile.Tile0xE0: return 0xE0;
                case Tile.Tile0xE1: return 0xE1;
                case Tile.Tile0xE2: return 0xE2;
                case Tile.Tile0xE3: return 0xE3;
                case Tile.Tile0xE4: return 0xE4;
                case Tile.Tile0xE5: return 0xE5;
                case Tile.Tile0xE6: return 0xE6;
                case Tile.Tile0xE7: return 0xE7;
                case Tile.Tile0xE8: return 0xE8;
                case Tile.Tile0xE9: return 0xE9;
                case Tile.Tile0xEA: return 0xEA;
                case Tile.Tile0xEB: return 0xEB;
                case Tile.Tile0xEC: return 0xEC;
                case Tile.Tile0xED: return 0xED;
                case Tile.Tile0xEE: return 0xEE;
                case Tile.Tile0xEF: return 0xEF;
                case Tile.Tile0xF0: return 0xF0;
                case Tile.Tile0xF1: return 0xF1;
                case Tile.Tile0xF2: return 0xF2;
                case Tile.Tile0xF3: return 0xF3;
                case Tile.Tile0xF4: return 0xF4;
                case Tile.Tile0xF5: return 0xF5;
                case Tile.Tile0xF6: return 0xF6;
                case Tile.Tile0xF7: return 0xF7;
                case Tile.Tile0xF8: return 0xF8;
                case Tile.Tile0xF9: return 0xF9;
                case Tile.Tile0xFA: return 0xFA;
                case Tile.Tile0xFB: return 0xFB;
                case Tile.Tile0xFC: return 0xFC;
                case Tile.Tile0xFD: return 0xFD;
                case Tile.Tile0xFE: return 0xFE;



                default: return 0x00;
            }
        }

        public static Tile GetTileFromValue(byte value)
        {
            switch (value)
            {


                case 0x00: return Tile.Tile0x00;
                case 0x01: return Tile.Tile0x01;
                case 0x02: return Tile.Tile0x02;
                case 0x03: return Tile.Tile0x03;
                case 0x04: return Tile.Tile0x04;
                case 0x05: return Tile.Tile0x05;
                case 0x06: return Tile.Tile0x06;
                case 0x07: return Tile.Tile0x07;
                case 0x08: return Tile.Tile0x08;
                case 0x09: return Tile.Tile0x09;
                case 0x0A: return Tile.Tile0x0A;
                case 0x0B: return Tile.Tile0x0B;
                case 0x0C: return Tile.Tile0x0C;
                case 0x0D: return Tile.Tile0x0D;
                case 0x0E: return Tile.Tile0x0E;
                case 0x0F: return Tile.Tile0x0F;
                case 0x10: return Tile.Tile0x10;
                case 0x11: return Tile.Tile0x11;
                case 0x12: return Tile.Tile0x12;
                case 0x13: return Tile.Tile0x13;
                case 0x14: return Tile.Tile0x14;
                case 0x15: return Tile.Tile0x15;
                case 0x16: return Tile.Tile0x16;
                case 0x17: return Tile.Tile0x17;
                case 0x18: return Tile.Tile0x18;
                case 0x19: return Tile.Tile0x19;
                case 0x1A: return Tile.Tile0x1A;
                case 0x1B: return Tile.Tile0x1B;
                case 0x1C: return Tile.Tile0x1C;
                case 0x1D: return Tile.Tile0x1D;
                case 0x1E: return Tile.Tile0x1E;
                case 0x1F: return Tile.Tile0x1F;
                case 0x20: return Tile.Tile0x20;
                case 0x21: return Tile.Tile0x21;
                case 0x22: return Tile.Tile0x22;
                case 0x23: return Tile.DesertTrees;
                case 0x24: return Tile.Tile0x24;
                case 0x25: return Tile.Tile0x25;
                case 0x26: return Tile.Tile0x26;
                case 0x27: return Tile.Tile0x27;
                case 0x28: return Tile.Tile0x28;
                case 0x29: return Tile.Tile0x29;
                case 0x2A: return Tile.Tile0x2A;
                case 0x2B: return Tile.Tile0x2B;
                case 0x2C: return Tile.Tile0x2C;
                case 0x2D: return Tile.Tile0x2D;
                case 0x2E: return Tile.Tile0x2E;
                case 0x2F: return Tile.Tile0x2F;
                case 0x30: return Tile.Tile0x30;
                case 0x31: return Tile.Tile0x31;
                case 0x32: return Tile.Tile0x32;
                case 0x33: return Tile.Tile0x33;
                case 0x34: return Tile.Tile0x34;
                case 0x35: return Tile.Tile0x35;
                case 0x36: return Tile.Tile0x36;
                case 0x37: return Tile.Tile0x37;
                case 0x38: return Tile.Tile0x38;
                case 0x39: return Tile.Tile0x39;
                case 0x3A: return Tile.Tile0x3A;
                case 0x3B: return Tile.Tile0x3B;
                case 0x3C: return Tile.Tile0x3C;
                case 0x3D: return Tile.Tile0x3D;
                case 0x3E: return Tile.Tile0x3E;
                case 0x3F: return Tile.Water;
                case 0x40: return Tile.Tile0x40;
                case 0x41: return Tile.Tile0x41;
                case 0x42: return Tile.Lava;
                case 0x43: return Tile.Desert;
                case 0x44: return Tile.Tile0x44;
                case 0x45: return Tile.Tile0x45;
                case 0x46: return Tile.Grass;
                case 0x47: return Tile.Tree;
                case 0x48: return Tile.Tile0x48;
                case 0x49: return Tile.Tile0x49;
                case 0x4A: return Tile.Tile0x4A;
                case 0x4B: return Tile.Tile0x4B;
                case 0x4C: return Tile.Tile0x4C;
                case 0x4D: return Tile.Tile0x4D;
                case 0x4E: return Tile.Tile0x4E;
                case 0x4F: return Tile.Tile0x4F;
                case 0x50: return Tile.Tile0x50;
                case 0x51: return Tile.Tile0x51;
                case 0x52: return Tile.Tile0x52;
                case 0x53: return Tile.Tile0x53;
                case 0x54: return Tile.Tile0x54;
                case 0x55: return Tile.Tile0x55;
                case 0x56: return Tile.Tile0x56;
                case 0x57: return Tile.Tile0x57;
                case 0x58: return Tile.Tile0x58;
                case 0x59: return Tile.Tile0x59;
                case 0x5A: return Tile.Tile0x5A;
                case 0x5B: return Tile.Tile0x5B;
                case 0x5C: return Tile.Tile0x5C;
                case 0x5D: return Tile.Tile0x5D;
                case 0x5E: return Tile.Tile0x5E;
                case 0x5F: return Tile.Tile0x5F;
                case 0x60: return Tile.Tile0x60;
                case 0x61: return Tile.Tile0x61;
                case 0x62: return Tile.Tile0x62;
                case 0x63: return Tile.Tile0x63;
                case 0x64: return Tile.Tile0x64;
                case 0x65: return Tile.Tile0x65;
                case 0x66: return Tile.Tile0x66;
                case 0x67: return Tile.Tile0x67;
                case 0x68: return Tile.Tile0x68;
                case 0x69: return Tile.Tile0x69;
                case 0x6A: return Tile.Tile0x6A;
                case 0x6B: return Tile.Tile0x6B;
                case 0x6C: return Tile.Tile0x6C;
                case 0x6D: return Tile.Tile0x6D;
                case 0x6E: return Tile.Tile0x6E;
                case 0x6F: return Tile.WaterTopEdge;
                case 0x70: return Tile.Tile0x70;
                case 0x71: return Tile.Tile0x71;
                case 0x72: return Tile.Tile0x72;
                case 0x73: return Tile.Tile0x73;
                case 0x74: return Tile.Tile0x74;
                case 0x75: return Tile.Tile0x75;
                case 0x76: return Tile.Tile0x76;
                case 0x77: return Tile.Tile0x77;
                case 0x78: return Tile.Tile0x78;
                case 0x79: return Tile.Tile0x79;
                case 0x7A: return Tile.Tile0x7A;
                case 0x7B: return Tile.Tile0x7B;
                case 0x7C: return Tile.Tile0x7C;
                case 0x7D: return Tile.Tile0x7D;
                case 0x7E: return Tile.Tile0x7E;
                case 0x7F: return Tile.Tile0x7F;
                case 0x80: return Tile.Tile0x80;
                case 0x81: return Tile.Tile0x81;
                case 0x82: return Tile.Tile0x82;
                case 0x83: return Tile.Tile0x83;
                case 0x84: return Tile.Tile0x84;
                case 0x85: return Tile.Tile0x85;
                case 0x86: return Tile.Tile0x86;
                case 0x87: return Tile.Tile0x87;
                case 0x88: return Tile.Tile0x88;
                case 0x89: return Tile.Tile0x89;
                case 0x8A: return Tile.Tile0x8A;
                case 0x8B: return Tile.Tile0x8B;
                case 0x8C: return Tile.Tile0x8C;
                case 0x8D: return Tile.Tile0x8D;
                case 0x8E: return Tile.Tile0x8E;
                case 0x8F: return Tile.Tile0x8F;
                case 0x90: return Tile.Tile0x90;
                case 0x91: return Tile.Tile0x91;
                case 0x92: return Tile.Tile0x92;
                case 0x93: return Tile.Tile0x93;
                case 0x94: return Tile.Tile0x94;
                case 0x95: return Tile.Tile0x95;
                case 0x96: return Tile.Tile0x96;
                case 0x97: return Tile.Tile0x97;
                case 0x98: return Tile.Tile0x98;
                case 0x99: return Tile.Tile0x99;
                case 0x9A: return Tile.Tile0x9A;
                case 0x9B: return Tile.Tile0x9B;
                case 0x9C: return Tile.Tile0x9C;
                case 0x9D: return Tile.Tile0x9D;
                case 0x9E: return Tile.Tile0x9E;
                case 0x9F: return Tile.Tile0x9F;
                case 0xA0: return Tile.Tile0xA0;
                case 0xA1: return Tile.Tile0xA1;
                case 0xA2: return Tile.Tile0xA2;
                case 0xA3: return Tile.Tile0xA3;
                case 0xA4: return Tile.Tile0xA4;
                case 0xA5: return Tile.Tile0xA5;
                case 0xA6: return Tile.Tile0xA6;
                case 0xA7: return Tile.Tile0xA7;
                case 0xA8: return Tile.Tile0xA8;
                case 0xA9: return Tile.Tile0xA9;
                case 0xAA: return Tile.Tile0xAA;
                case 0xAB: return Tile.Tile0xAB;
                case 0xAC: return Tile.Tile0xAC;
                case 0xAD: return Tile.Tile0xAD;
                case 0xAE: return Tile.Tile0xAE;
                case 0xAF: return Tile.Tile0xAF;
                case 0xB0: return Tile.Tile0xB0;
                case 0xB1: return Tile.Tile0xB1;
                case 0xB2: return Tile.Tile0xB2;
                case 0xB3: return Tile.Tile0xB3;
                case 0xB4: return Tile.Tile0xB4;
                case 0xB5: return Tile.Tile0xB5;
                case 0xB6: return Tile.Tile0xB6;
                case 0xB7: return Tile.Tile0xB7;
                case 0xB8: return Tile.Tile0xB8;
                case 0xB9: return Tile.Tile0xB9;
                case 0xBA: return Tile.Tile0xBA;
                case 0xBB: return Tile.Tile0xBB;
                case 0xBC: return Tile.Tile0xBC;
                case 0xBD: return Tile.Tile0xBD;
                case 0xBE: return Tile.Tile0xBE;
                case 0xBF: return Tile.Tile0xBF;
                case 0xC0: return Tile.Tile0xC0;
                case 0xC1: return Tile.Tile0xC1;
                case 0xC2: return Tile.Tile0xC2;
                case 0xC3: return Tile.Tile0xC3;
                case 0xC4: return Tile.Tile0xC4;
                case 0xC5: return Tile.Tile0xC5;
                case 0xC6: return Tile.Tile0xC6;
                case 0xC7: return Tile.Tile0xC7;
                case 0xC8: return Tile.Tile0xC8;
                case 0xC9: return Tile.Tile0xC9;
                case 0xCA: return Tile.Tile0xCA;
                case 0xCB: return Tile.Tile0xCB;
                case 0xCC: return Tile.Tile0xCC;
                case 0xCD: return Tile.Tile0xCD;
                case 0xCE: return Tile.Tile0xCE;
                case 0xCF: return Tile.Tile0xCF;
                case 0xD0: return Tile.Tile0xD0;
                case 0xD1: return Tile.Tile0xD1;
                case 0xD2: return Tile.Tile0xD2;
                case 0xD3: return Tile.Tile0xD3;
                case 0xD4: return Tile.Tile0xD4;
                case 0xD5: return Tile.Tile0xD5;
                case 0xD6: return Tile.Tile0xD6;
                case 0xD7: return Tile.Tile0xD7;
                case 0xD8: return Tile.Tile0xD8;
                case 0xD9: return Tile.Tile0xD9;
                case 0xDA: return Tile.Tile0xDA;
                case 0xDB: return Tile.Tile0xDB;
                case 0xDC: return Tile.Tile0xDC;
                case 0xDD: return Tile.Tile0xDD;
                case 0xDE: return Tile.Tile0xDE;
                case 0xDF: return Tile.Tile0xDF;
                case 0xE0: return Tile.Tile0xE0;
                case 0xE1: return Tile.Tile0xE1;
                case 0xE2: return Tile.Tile0xE2;
                case 0xE3: return Tile.Tile0xE3;
                case 0xE4: return Tile.Tile0xE4;
                case 0xE5: return Tile.Tile0xE5;
                case 0xE6: return Tile.Tile0xE6;
                case 0xE7: return Tile.Tile0xE7;
                case 0xE8: return Tile.Tile0xE8;
                case 0xE9: return Tile.Tile0xE9;
                case 0xEA: return Tile.Tile0xEA;
                case 0xEB: return Tile.Tile0xEB;
                case 0xEC: return Tile.Tile0xEC;
                case 0xED: return Tile.Tile0xED;
                case 0xEE: return Tile.Tile0xEE;
                case 0xEF: return Tile.Tile0xEF;
                case 0xF0: return Tile.Tile0xF0;
                case 0xF1: return Tile.Tile0xF1;
                case 0xF2: return Tile.Tile0xF2;
                case 0xF3: return Tile.Tile0xF3;
                case 0xF4: return Tile.Tile0xF4;
                case 0xF5: return Tile.Tile0xF5;
                case 0xF6: return Tile.Tile0xF6;
                case 0xF7: return Tile.Tile0xF7;
                case 0xF8: return Tile.Tile0xF8;
                case 0xF9: return Tile.Tile0xF9;
                case 0xFA: return Tile.Tile0xFA;
                case 0xFB: return Tile.Tile0xFB;
                case 0xFC: return Tile.Tile0xFC;
                case 0xFD: return Tile.Tile0xFD;
                case 0xFE: return Tile.Tile0xFE;

                default: return Tile.UNKNOWN;
            }
        }


        public  enum Tile
        {
            Grass,
            Tree,
            GrassBushes,
            Water,
            WaterTopEdge,
            Desert,
            DesertTrees,
            Lava,

            Tile0x00,
            Tile0x01,
            Tile0x02,
            Tile0x03,
            Tile0x04,
            Tile0x05,
            Tile0x06,
            Tile0x07,
            Tile0x08,
            Tile0x09,
            Tile0x0A,
            Tile0x0B,
            Tile0x0C,
            Tile0x0D,
            Tile0x0E,
            Tile0x0F,
            Tile0x10,
            Tile0x11,
            Tile0x12,
            Tile0x13,
            Tile0x14,
            Tile0x15,
            Tile0x16,
            Tile0x17,
            Tile0x18,
            Tile0x19,
            Tile0x1A,
            Tile0x1B,
            Tile0x1C,
            Tile0x1D,
            Tile0x1E,
            Tile0x1F,
            Tile0x20,
            Tile0x21,
            Tile0x22,
            Tile0x23,
            Tile0x24,
            Tile0x25,
            Tile0x26,
            Tile0x27,
            Tile0x28,
            Tile0x29,
            Tile0x2A,
            Tile0x2B,
            Tile0x2C,
            Tile0x2D,
            Tile0x2E,
            Tile0x2F,
            Tile0x30,
            Tile0x31,
            Tile0x32,
            Tile0x33,
            Tile0x34,
            Tile0x35,
            Tile0x36,
            Tile0x37,
            Tile0x38,
            Tile0x39,
            Tile0x3A,
            Tile0x3B,
            Tile0x3C,
            Tile0x3D,
            Tile0x3E,
            Tile0x3F,
            Tile0x40,
            Tile0x41,
            Tile0x42,
            Tile0x43,
            Tile0x44,
            Tile0x45,
            Tile0x46,
            Tile0x47,
            Tile0x48,
            Tile0x49,
            Tile0x4A,
            Tile0x4B,
            Tile0x4C,
            Tile0x4D,
            Tile0x4E,
            Tile0x4F,
            Tile0x50,
            Tile0x51,
            Tile0x52,
            Tile0x53,
            Tile0x54,
            Tile0x55,
            Tile0x56,
            Tile0x57,
            Tile0x58,
            Tile0x59,
            Tile0x5A,
            Tile0x5B,
            Tile0x5C,
            Tile0x5D,
            Tile0x5E,
            Tile0x5F,
            Tile0x60,
            Tile0x61,
            Tile0x62,
            Tile0x63,
            Tile0x64,
            Tile0x65,
            Tile0x66,
            Tile0x67,
            Tile0x68,
            Tile0x69,
            Tile0x6A,
            Tile0x6B,
            Tile0x6C,
            Tile0x6D,
            Tile0x6E,
            Tile0x6F,
            Tile0x70,
            Tile0x71,
            Tile0x72,
            Tile0x73,
            Tile0x74,
            Tile0x75,
            Tile0x76,
            Tile0x77,
            Tile0x78,
            Tile0x79,
            Tile0x7A,
            Tile0x7B,
            Tile0x7C,
            Tile0x7D,
            Tile0x7E,
            Tile0x7F,
            Tile0x80,
            Tile0x81,
            Tile0x82,
            Tile0x83,
            Tile0x84,
            Tile0x85,
            Tile0x86,
            Tile0x87,
            Tile0x88,
            Tile0x89,
            Tile0x8A,
            Tile0x8B,
            Tile0x8C,
            Tile0x8D,
            Tile0x8E,
            Tile0x8F,
            Tile0x90,
            Tile0x91,
            Tile0x92,
            Tile0x93,
            Tile0x94,
            Tile0x95,
            Tile0x96,
            Tile0x97,
            Tile0x98,
            Tile0x99,
            Tile0x9A,
            Tile0x9B,
            Tile0x9C,
            Tile0x9D,
            Tile0x9E,
            Tile0x9F,
            Tile0xA0,
            Tile0xA1,
            Tile0xA2,
            Tile0xA3,
            Tile0xA4,
            Tile0xA5,
            Tile0xA6,
            Tile0xA7,
            Tile0xA8,
            Tile0xA9,
            Tile0xAA,
            Tile0xAB,
            Tile0xAC,
            Tile0xAD,
            Tile0xAE,
            Tile0xAF,
            Tile0xB0,
            Tile0xB1,
            Tile0xB2,
            Tile0xB3,
            Tile0xB4,
            Tile0xB5,
            Tile0xB6,
            Tile0xB7,
            Tile0xB8,
            Tile0xB9,
            Tile0xBA,
            Tile0xBB,
            Tile0xBC,
            Tile0xBD,
            Tile0xBE,
            Tile0xBF,
            Tile0xC0,
            Tile0xC1,
            Tile0xC2,
            Tile0xC3,
            Tile0xC4,
            Tile0xC5,
            Tile0xC6,
            Tile0xC7,
            Tile0xC8,
            Tile0xC9,
            Tile0xCA,
            Tile0xCB,
            Tile0xCC,
            Tile0xCD,
            Tile0xCE,
            Tile0xCF,
            Tile0xD0,
            Tile0xD1,
            Tile0xD2,
            Tile0xD3,
            Tile0xD4,
            Tile0xD5,
            Tile0xD6,
            Tile0xD7,
            Tile0xD8,
            Tile0xD9,
            Tile0xDA,
            Tile0xDB,
            Tile0xDC,
            Tile0xDD,
            Tile0xDE,
            Tile0xDF,
            Tile0xE0,
            Tile0xE1,
            Tile0xE2,
            Tile0xE3,
            Tile0xE4,
            Tile0xE5,
            Tile0xE6,
            Tile0xE7,
            Tile0xE8,
            Tile0xE9,
            Tile0xEA,
            Tile0xEB,
            Tile0xEC,
            Tile0xED,
            Tile0xEE,
            Tile0xEF,
            Tile0xF0,
            Tile0xF1,
            Tile0xF2,
            Tile0xF3,
            Tile0xF4,
            Tile0xF5,
            Tile0xF6,
            Tile0xF7,
            Tile0xF8,
            Tile0xF9,
            Tile0xFA,
            Tile0xFB,
            Tile0xFC,
            Tile0xFD,
            Tile0xFE,

            //todo: remove unidentified instance for known values






            UNKNOWN
        }

        #endregion Tiles

    }

}

    





