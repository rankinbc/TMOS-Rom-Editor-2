using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tmos.Romhacks.Core;
using Tmos.Romhacks.Mods;
using static Tmos.Romhacks.Core.TmosData;
using static Tmos.Romhacks.Mods.RandomizerModWorldScreen;

namespace Tmos.Romhacks.UI
{


    public partial class Form1 : Form
    {

        TmosRom _currentRom = null;

        TmosWorldScreen[] _worldScreens = new TmosWorldScreen[DataStructureCounts.WorldScreen_Count];

        RandomizerMod _randomizerMod = new RandomizerMod();

        int _selectedWorldScreenIndex;



        Dictionary<Tile, string> TileImagePaths = new Dictionary<Tile, string>()
        {
                { Tile.Grass,"46.png" },
                { Tile.Tree,"47.png" },
                { Tile.Desert,"43.png" },
                { Tile.DesertTrees,"23.png" },
                { Tile.Water,"3f.png" },
                { Tile.WaterTopEdge,"6f.png" },
                { Tile.Lava,"42.png" },

                {Tile.Tile0x00,"00.png"},
{Tile.Tile0x01,"01.png"},
{Tile.Tile0x02,"02.png"},
{Tile.Tile0x03,"03.png"},
{Tile.Tile0x04,"04.png"},
{Tile.Tile0x05,"05.png"},
{Tile.Tile0x06,"06.png"},
{Tile.Tile0x07,"07.png"},
{Tile.Tile0x08,"08.png"},
{Tile.Tile0x09,"09.png"},
{Tile.Tile0x0A,"0A.png"},
{Tile.Tile0x0B,"0B.png"},
{Tile.Tile0x0C,"0C.png"},
{Tile.Tile0x0D,"0D.png"},
{Tile.Tile0x0E,"0D.png"},
{Tile.Tile0x0F,"0F.png"},
{Tile.Tile0x10,"10.png"},
{Tile.Tile0x11,"11.png"},
{Tile.Tile0x12,"12.png"},
{Tile.Tile0x13,"13.png"},
{Tile.Tile0x14,"0D.png"},
{Tile.Tile0x15,"15.png"},
{Tile.Tile0x16,"0D.png"},
{Tile.Tile0x17,"0D.png"},
{Tile.Tile0x18,"18.png"},
{Tile.Tile0x19,"19.png"},
{Tile.Tile0x1A,"1A.png"},
{Tile.Tile0x1B,"1B.png"},
{Tile.Tile0x1C,"1C.png"},
{Tile.Tile0x1D,"1D.png"},
{Tile.Tile0x1E,"1E.png"},
{Tile.Tile0x1F,"1F.png"},
{Tile.Tile0x20,"20.png"},
{Tile.Tile0x21,"21.png"},
{Tile.Tile0x22,"22.png"},
{Tile.Tile0x23,"23.png"},
{Tile.Tile0x24,"25.png"},
{Tile.Tile0x25,"25.png"},
{Tile.Tile0x26,"26.png"},
{Tile.Tile0x27,"27.png"},
{Tile.Tile0x28,"28.png"},
{Tile.Tile0x29,"29.png"},
{Tile.Tile0x2A,"2A.png"},
{Tile.Tile0x2B,"2B.png"},
{Tile.Tile0x2C,"2C.png"},
{Tile.Tile0x2D,"2D.png"},
{Tile.Tile0x2E,"2E.png"},
{Tile.Tile0x2F,"2F.png"},
{Tile.Tile0x30,"30.png"},
{Tile.Tile0x31,"31.png"},
{Tile.Tile0x32,"32.png"},
{Tile.Tile0x33,"33.png"},
{Tile.Tile0x34,"34.png"},
{Tile.Tile0x35,"35.png"},
{Tile.Tile0x36,"36.png"},
{Tile.Tile0x37,"37.png"},
{Tile.Tile0x38,"38.png"},
{Tile.Tile0x39,"39.png"},
{Tile.Tile0x3A,"3A.png"},
{Tile.Tile0x3B,"3B.png"},
{Tile.Tile0x3C,"3C.png"},
{Tile.Tile0x3D,"3D.png"},
{Tile.Tile0x3E,"3E.png"},
{Tile.Tile0x3F,"3F.png"},
{Tile.Tile0x40,"40.png"},
{Tile.Tile0x41,"41.png"},
{Tile.Tile0x42,"42.png"},
{Tile.Tile0x43,"43.png"},
{Tile.Tile0x44,"44.png"},
{Tile.Tile0x45,"45.png"},
{Tile.Tile0x46,"46.png"},
{Tile.Tile0x47,"47.png"},
{Tile.Tile0x48,"48.png"},
{Tile.Tile0x49,"46.png"},
{Tile.Tile0x4A,"4A.png"},
{Tile.Tile0x4B,"46.png"},
{Tile.Tile0x4C,"4C.png"},
{Tile.Tile0x4D,"4D.png"},
{Tile.Tile0x4E,"4E.png"},
{Tile.Tile0x4F,"4F.png"},
{Tile.Tile0x50,"50.png"},
{Tile.Tile0x51,"51.png"},
{Tile.Tile0x52,"52.png"},
{Tile.Tile0x53,"53.png"},
{Tile.Tile0x54,"54.png"},
{Tile.Tile0x55,"55.png"},
{Tile.Tile0x56,"56.png"},
{Tile.Tile0x57,"57.png"},
{Tile.Tile0x58,"58.png"},
{Tile.Tile0x59,"59.png"},
{Tile.Tile0x5A,"5A.png"},
{Tile.Tile0x5B,"5B.png"},
{Tile.Tile0x5C,"5C.png"},
{Tile.Tile0x5D,"5D.png"},
{Tile.Tile0x5E,"5E.png"},
{Tile.Tile0x5F,"5F.png"},
{Tile.Tile0x60,"60.png"},
{Tile.Tile0x61,"61.png"},
{Tile.Tile0x62,"62.png"},
{Tile.Tile0x63,"63.png"},
{Tile.Tile0x64,"64.png"},
{Tile.Tile0x65,"65.png"},
{Tile.Tile0x66,"66.png"},
{Tile.Tile0x67,"67.png"},
{Tile.Tile0x68,"68.png"},
{Tile.Tile0x69,"69.png"},
{Tile.Tile0x6A,"6A.png"},
{Tile.Tile0x6B,"6B.png"},
{Tile.Tile0x6C,"6C.png"},
{Tile.Tile0x6D,"6D.png"},
{Tile.Tile0x6E,"6E.png"},
{Tile.Tile0x6F,"6F.png"},
{Tile.Tile0x70,"70.png"},
{Tile.Tile0x71,"71.png"},
{Tile.Tile0x72,"72.png"},
{Tile.Tile0x73,"73.png"},
{Tile.Tile0x74,"74.png"},
{Tile.Tile0x75,"75.png"},
{Tile.Tile0x76,"76.png"},
{Tile.Tile0x77,"77.png"},
{Tile.Tile0x78,"78.png"},
{Tile.Tile0x79,"79.png"},
{Tile.Tile0x7A,"7A.png"},
{Tile.Tile0x7B,"7B.png"},
{Tile.Tile0x7C,"7C.png"},
{Tile.Tile0x7D,"7D.png"},
{Tile.Tile0x7E,"7E.png"},
{Tile.Tile0x7F,"7F.png"},
{Tile.Tile0x80,"80.png"},
{Tile.Tile0x81,"81.png"},
{Tile.Tile0x82,"82.png"},
{Tile.Tile0x83,"83.png"},
{Tile.Tile0x84,"84.png"},
{Tile.Tile0x85,"85.png"},
{Tile.Tile0x86,"86.png"},
{Tile.Tile0x87,"87.png"},
{Tile.Tile0x88,"88.png"},
{Tile.Tile0x89,"89.png"},
{Tile.Tile0x8A,"8A.png"},
{Tile.Tile0x8B,"8B.png"},
{Tile.Tile0x8C,"8C.png"},
{Tile.Tile0x8D,"8D.png"},
{Tile.Tile0x8E,"8E.png"},
{Tile.Tile0x8F,"8F.png"},
{Tile.Tile0x90,"90.png"},
{Tile.Tile0x91,"91.png"},
{Tile.Tile0x92,"92.png"},
{Tile.Tile0x93,"93.png"},
{Tile.Tile0x94,"94.png"},
{Tile.Tile0x95,"95.png"},
{Tile.Tile0x96,"96.png"},
{Tile.Tile0x97,"97.png"},
{Tile.Tile0x98,"98.png"},
{Tile.Tile0x99,"99.png"},
{Tile.Tile0x9A,"9A.png"},
{Tile.Tile0x9B,"9B.png"},
{Tile.Tile0x9C,"9C.png"},
{Tile.Tile0x9D,"9D.png"},
{Tile.Tile0x9E,"9E.png"},
{Tile.Tile0x9F,"9F.png"},
{Tile.Tile0xA0,"A0.png"},
{Tile.Tile0xA1,"A1.png"},
{Tile.Tile0xA2,"A2.png"},
{Tile.Tile0xA3,"A3.png"},
{Tile.Tile0xA4,"A4.png"},
{Tile.Tile0xA5,"A5.png"},
{Tile.Tile0xA6,"A6.png"},
{Tile.Tile0xA7,"A7.png"},
{Tile.Tile0xA8,"A8.png"},
{Tile.Tile0xA9,"A9.png"},
{Tile.Tile0xAA,"AA.png"},
{Tile.Tile0xAB,"AB.png"},
{Tile.Tile0xAC,"AC.png"},
{Tile.Tile0xAD,"AD.png"},
{Tile.Tile0xAE,"AE.png"},
{Tile.Tile0xAF,"AF.png"},
{Tile.Tile0xB0,"B0.png"},
{Tile.Tile0xB1,"B1.png"},
{Tile.Tile0xB2,"B2.png"},
{Tile.Tile0xB3,"B3.png"},
{Tile.Tile0xB4,"B4.png"},
{Tile.Tile0xB5,"B5.png"},
{Tile.Tile0xB6,"B6.png"},
{Tile.Tile0xB7,"B7.png"},
{Tile.Tile0xB8,"B8.png"},
{Tile.Tile0xB9,"B9.png"},
{Tile.Tile0xBA,"BA.png"},
{Tile.Tile0xBB,"BB.png"},
{Tile.Tile0xBC,"BC.png"},
{Tile.Tile0xBD,"BD.png"},
{Tile.Tile0xBE,"BE.png"},
{Tile.Tile0xBF,"BF.png"},
{Tile.Tile0xC0,"C0.png"},
{Tile.Tile0xC1,"C1.png"},
{Tile.Tile0xC2,"C2.png"},
{Tile.Tile0xC3,"C3.png"},
{Tile.Tile0xC4,"C4.png"},
{Tile.Tile0xC5,"C5.png"},
{Tile.Tile0xC6,"C6.png"},
{Tile.Tile0xC7,"C7.png"},
{Tile.Tile0xC8,"C8.png"},
{Tile.Tile0xC9,"C9.png"},
{Tile.Tile0xCA,"CA.png"},
{Tile.Tile0xCB,"CB.png"},
{Tile.Tile0xCC,"CC.png"},
{Tile.Tile0xCD,"CD.png"},
{Tile.Tile0xCE,"CE.png"},
{Tile.Tile0xCF,"CF.png"},
{Tile.Tile0xD0,"D0.png"},
{Tile.Tile0xD1,"D1.png"},
{Tile.Tile0xD2,"D2.png"},
{Tile.Tile0xD3,"D3.png"},
{Tile.Tile0xD4,"D4.png"},
{Tile.Tile0xD5,"D5.png"},
{Tile.Tile0xD6,"D6.png"},
{Tile.Tile0xD7,"D7.png"},
{Tile.Tile0xD8,"D8.png"},
{Tile.Tile0xD9,"D9.png"},
{Tile.Tile0xDA,"DA.png"},
{Tile.Tile0xDB,"DB.png"},
{Tile.Tile0xDC,"DC.png"},
{Tile.Tile0xDD,"DD.png"},
{Tile.Tile0xDE,"DE.png"},
{Tile.Tile0xDF,"DF.png"},
{Tile.Tile0xE0,"E0.png"},
{Tile.Tile0xE1,"E1.png"},
{Tile.Tile0xE2,"E2.png"},
{Tile.Tile0xE3,"E3.png"},
{Tile.Tile0xE4,"E4.png"},
{Tile.Tile0xE5,"E5.png"},
{Tile.Tile0xE6,"E6.png"},
{Tile.Tile0xE7,"E7.png"},
{Tile.Tile0xE8,"E8.png"},
{Tile.Tile0xE9,"E9.png"},
{Tile.Tile0xEA,"EA.png"},
{Tile.Tile0xEB,"EB.png"},
{Tile.Tile0xEC,"EC.png"},
{Tile.Tile0xED,"ED.png"},
{Tile.Tile0xEE,"EE.png"},
{Tile.Tile0xEF,"EF.png"},
{Tile.Tile0xF0,"F0.png"},
{Tile.Tile0xF1,"F1.png"},
{Tile.Tile0xF2,"F2.png"},
{Tile.Tile0xF3,"F3.png"},
{Tile.Tile0xF4,"F4.png"},
{Tile.Tile0xF5,"F5.png"},
{Tile.Tile0xF6,"F6.png"},
{Tile.Tile0xF7,"F7.png"},
{Tile.Tile0xF8,"F8.png"},
{Tile.Tile0xF9,"F9.png"},
{Tile.Tile0xFA,"FA.png"},
{Tile.Tile0xFB,"FB.png"},
{Tile.Tile0xFC,"FC.png"},
{Tile.Tile0xFD,"FD.png"},
{Tile.Tile0xFE,"FE.png" }
        };


       

        public Form1()
        {
            InitializeComponent();
        
            _selectedWorldScreenIndex = 0;

        }

      

        private void btn_load_rom_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;


                _currentRom = new TmosRom();
                _currentRom.LoadRom(filePath);

                _randomizerMod = new RandomizerMod();
                _randomizerMod.LoadDataFromRom(_currentRom);
                
                _randomizerMod.Randomize();

                //Init display stuff
               Init();

            }
            //update initial settings display
        }

        private void btn_save_rom_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                SaveRom(saveFileDialog1.FileName);
            }
            
        }

        private void SaveRom(string path)
        {

            //Update _currentRom
            for (int i = 0; i < _randomizerMod.WorldScreens.Length; i++)
            {
                RandomizerModWorldScreen ws = _randomizerMod.WorldScreens[i];
                _currentRom.SaveWorldScreen(i, RandomizerMod.RandomizerWorldScreenToTmosWorldScreen(ws));
            }
            _currentRom.WriteRom(path);

            // FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
        }

        private void Init()
        {
            UpdateWorldScreenListBox();
            UpdateTileSectionListBoxes();
            SelectWorldScreen(0);
        }

 
        private void UpdateTileSectionListBoxes()
        {
            lb_tileSection_top.Items.Clear();
            for (int i = 0; i < _randomizerMod.TileSections.Length; i++)
            {
                lb_tileSection_top.Items.Add(i);
                lb_tileSection_bottom.Items.Add(i);
            }
        }

        private void UpdateWorldScreenListBox()
        {
            lb_worldScreens.Items.Clear();
            for (int i = 0; i < _randomizerMod.WorldScreens.Length; i++)
            {
                lb_worldScreens.Items.Add(i);
            }
        }

        private void DrawWorldScreenTiles(RandomizerModWorldScreen randomizerWorldScreen)
        {
    
            pb_map.Image = new Bitmap(pb_map.Width, pb_map.Height);

            int tile_size = 100;

            //calculate tile offsets based on ws data pointer
      

            Tile[,] grid = randomizerWorldScreen.GetTileGrid();

            //border screens
            Tile[,] grid_wsRight = _randomizerMod.WorldScreens[randomizerWorldScreen.ScreenIndexRight].GetTileGrid();


            using (var g = Graphics.FromImage(pb_map.Image))
            {
                g.Clear(Color.LightGray);

                for (int y = 0; y < 6; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        Point location = new Point(tile_size * x, tile_size * y);
                        Size size = new Size(tile_size, tile_size);
                        Rectangle rect = new Rectangle(location, size);

                        g.DrawRectangle(Pens.Black, rect);

                        Font font = new Font("Arial", 7);
                        Brush brush = Pens.Black.Brush;


                        Tile tile = grid[x, y];
                        byte tileValue = RandomizerModWorldScreen.GetTileValue(tile);

                        if (!RandomizerModWorldScreen.TileIsWalkable(tile))
                        {
                            g.DrawRectangle(Pens.Red, rect);
                        }


                        if (TileImagePaths.ContainsKey(grid[x, y]))
                        {
                            try
                            {
                                string tileImagePath = TileImagePaths[tile];
                                Image image = new Bitmap("TileImages/" + TileImagePaths[tile]);
                                g.DrawImage(image, rect);
                            }
                            catch { }
                        }

                        g.DrawString(RandomizerModWorldScreen.GetTileName(grid[x, y]), font, brush, location.X + 20, location.Y + 20);
                        g.DrawString(RandomizerModWorldScreen.GetTileValue(grid[x, y]).ToString("X6"), font, brush, location.X + 20, location.Y + 40);
                    }
                }

            
               

                    pb_map.Refresh();
            }

        }



        /// <summary>
        /// Draws striaght from TMOS ROM instead of using RandomizerMod classes
        /// </summary>
        /// <param name="worldScreenTiles"></param>
        //private void DrawWorldScreenTiles(TmosWorldScreenTiles worldScreenTiles)
        //{
        //    pb_map.Image = new Bitmap(pb_map.Width, pb_map.Height);

        //    int tile_size = 100;
        //    byte[,] grid = worldScreenTiles.GetTileGrid();
        //    using (var g = Graphics.FromImage(pb_map.Image))
        //    {
        //        g.Clear(Color.LightGray);

        //        for (int y = 0; y < 6; y++)
        //        {
        //            for (int x = 0; x < 8; x++)
        //            {
        //                Point location = new Point(tile_size * x, tile_size * y);

        //                Size size = new Size(tile_size, tile_size);
        //                Rectangle rect = new Rectangle(location, size);
        //                g.DrawRectangle(Pens.Black, rect);     


        //                Font font = new Font("Arial", 7);
        //                Brush brush = Pens.Black.Brush;
        //                g.DrawString(grid[x, y].ToString("X6"),font,brush,location.X + 20,location.Y + 20);
        //            }
        //        }
        //        pb_map.Refresh();
        //    }

        //}

        //private void DrawWorldScreenMiniTiles(TmosWorldScreenTiles worldScreenTiles)
        //{
        //    pb_map.Image = new Bitmap(pb_map.Width, pb_map.Height);

        //    int tile_size = 100;
        //    byte[,] grid = worldScreenTiles.GetTileGrid(0);
        //    using (var g = Graphics.FromImage(pb_map.Image))
        //    {
        //        g.Clear(Color.LightGray);

        //        for (int y = 0; y < 6; y++)
        //        {
        //            for (int x = 0; x < 8; x++)
        //            {
        //                Point location = new Point(tile_size * x, tile_size * y);
        //                Size size = new Size(tile_size, tile_size);
        //                Rectangle rect = new Rectangle(location, size);
        //                g.DrawRectangle(Pens.Black, rect);
        //                 g.DrawRectangle(Pens.Black, rect);

        //                Font font = new Font("Arial", 7);
        //                Brush brush = Pens.Black.Brush;
        //                g.DrawString(grid[x, y].ToString("X6"), font, brush, location.X + 20, location.Y + 20);
        //            }
        //        }
        //        pb_map.Refresh();
        //    }

        //}

        private void SelectWorldScreen(int index)
        {
            RandomizerModWorldScreen selectedScreen = _randomizerMod.WorldScreens[index];

            // UpdateWorldScreenDataTextbox(selectedScreen);

            UpdateWorldScreenDataListView(selectedScreen);

            UpdateDirectionSection(selectedScreen);

            UpdateTileSectionListBoxesSelection(selectedScreen);

            DrawWorldScreenTiles(selectedScreen);

        }

        private void RefreshWorldScreenDrawing()
        {
            RandomizerModWorldScreen selectedScreen = _randomizerMod.WorldScreens[_selectedWorldScreenIndex];
            DrawWorldScreenTiles(selectedScreen);
        }

        private void UpdateWorldScreenDataListView(RandomizerModWorldScreen worldScreen)
        {

            lv_variables.Items[0].SubItems[1].Text = worldScreen.ParentWorld.ToString("X2");
            lv_variables.Items[1].SubItems[1].Text = worldScreen.AmbientSound.ToString("X2");
            lv_variables.Items[2].SubItems[1].Text = worldScreen.GetContentValue().ToString("X2");
            lv_variables.Items[3].SubItems[1].Text = worldScreen.ObjectSet.ToString("X2");
            lv_variables.Items[4].SubItems[1].Text = worldScreen.ScreenIndexRight.ToString("X2");
            lv_variables.Items[5].SubItems[1].Text = worldScreen.ScreenIndexLeft.ToString("X2");
            lv_variables.Items[6].SubItems[1].Text = worldScreen.ScreenIndexDown.ToString("X2");
            lv_variables.Items[7].SubItems[1].Text = worldScreen.ScreenIndexUp.ToString("X2");
            lv_variables.Items[8].SubItems[1].Text = worldScreen.DataPointer.ToString("X2");
            lv_variables.Items[9].SubItems[1].Text = worldScreen.ExitPosition.ToString("X2");
            lv_variables.Items[10].SubItems[1].Text = worldScreen.TopTiles.ToString("X2");
            lv_variables.Items[11].SubItems[1].Text = worldScreen.BottomTiles.ToString("X2");
            lv_variables.Items[12].SubItems[1].Text = worldScreen.WorldScreenColor.ToString("X2");
            lv_variables.Items[13].SubItems[1].Text = worldScreen.SpritesColor.ToString("X2");
            lv_variables.Items[14].SubItems[1].Text = worldScreen.Unknown.ToString("X2");
            lv_variables.Items[15].SubItems[1].Text = worldScreen.Event.ToString("X2");



            var CONTENTINDEX = 2;

            //hints
            //content
            lv_variables.Items[CONTENTINDEX].SubItems[2].Text = worldScreen.GetContentName();


            ////objectSets
            //if (KnownObjectSets.ContainsKey(ws.ObjectSet.ToString("X2"))) lv_variables.Items[(int)WorldScreen.DataContent.ObjectSet].SubItems[2].Text = KnownObjectSets[ws.ObjectSet.ToString("X2")];
            //else lv_variables.Items[(int)WorldScreen.DataContent.ObjectSet].SubItems[2].Text = "?";

            ////events
            //if (KnownEvents.ContainsKey(ws.Event.ToString("X2"))) lv_variables.Items[(int)WorldScreen.DataContent.Event].SubItems[2].Text = KnownEvents[ws.Event.ToString("X2")];
            //else lv_variables.Items[(int)WorldScreen.DataContent.Event].SubItems[2].Text = "?";

            ////screenexits
            //if (KnownScreenExits.ContainsKey(ws.ScreenIndexLeft.ToString("X2"))) lv_variables.Items[(int)WorldScreen.DataContent.ScreenIndexLeft].SubItems[2].Text = KnownScreenExits[ws.ScreenIndexLeft.ToString("X2")];
            //else lv_variables.Items[(int)WorldScreen.DataContent.ScreenIndexLeft].SubItems[2].Text = "enter screen " + ws.ScreenIndexLeft.ToString("X2");

            //if (KnownScreenExits.ContainsKey(ws.ScreenIndexRight.ToString("X2"))) lv_variables.Items[(int)WorldScreen.DataContent.ScreenIndexRight].SubItems[2].Text = KnownScreenExits[ws.ScreenIndexRight.ToString("X2")];
            //else lv_variables.Items[(int)WorldScreen.DataContent.ScreenIndexRight].SubItems[2].Text = "enter screen " + ws.ScreenIndexRight.ToString("X2");

            //if (KnownScreenExits.ContainsKey(ws.ScreenIndexUp.ToString("X2"))) lv_variables.Items[(int)WorldScreen.DataContent.ScreenIndexUp].SubItems[2].Text = KnownScreenExits[ws.ScreenIndexUp.ToString("X2")];
            //else lv_variables.Items[(int)WorldScreen.DataContent.ScreenIndexUp].SubItems[2].Text = "enter screen " + ws.ScreenIndexUp.ToString("X2");

            //if (KnownScreenExits.ContainsKey(ws.ScreenIndexDown.ToString("X2"))) lv_variables.Items[(int)WorldScreen.DataContent.ScreenIndexDown].SubItems[2].Text = KnownScreenExits[ws.ScreenIndexDown.ToString("X2")];
            //else lv_variables.Items[(int)WorldScreen.DataContent.ScreenIndexDown].SubItems[2].Text = "enter screen " + ws.ScreenIndexDown.ToString("X2");




            //lv_worldScreens.Items.Clear();
            //string[] data = new string[] {

            //    lv_variables

            //     worldScreen.ParentWorld.ToString("X2"),
            //     worldScreen.AmbientSound.ToString("X2"),
            //     worldScreen.GetContentValue().ToString("X2"),
            //     worldScreen.ObjectSet.ToString("X2"),
            //     worldScreen.ScreenIndexRight.ToString("X2"),
            //     worldScreen.ScreenIndexLeft.ToString("X2"),
            //     worldScreen.ScreenIndexDown.ToString("X2"),
            //     worldScreen.ScreenIndexUp.ToString("X2"),
            //     worldScreen.DataPointer.ToString("X2"),
            //     worldScreen.ExitPosition.ToString("X2"),
            //     worldScreen.TopTiles.ToString("X2"),
            //    worldScreen.BottomTiles.ToString("X2"),
            //    worldScreen.WorldScreenColor.ToString("X2"),
            //    worldScreen.SpritesColor.ToString("X2"),
            //    worldScreen.Unknown.ToString("X2"),
            //    worldScreen.Event.ToString("X2")
            //        };
            //     lv_worldScreens.Items.Add(" ").SubItems.AddRange(data);
        }

        private void UpdateWorldScreenDataTextbox(RandomizerModWorldScreen worldScreen)
        {
            tb_output.Clear();

            tb_output.Text += "ParentWorld: " + worldScreen.ParentWorld + Environment.NewLine;
            tb_output.Text += "AmbientSound: " + worldScreen.AmbientSound + Environment.NewLine;
            tb_output.Text += "Content: " + worldScreen.GetContentValue() + " (" + worldScreen.GetContentName() + ")" + Environment.NewLine;
            tb_output.Text += "ObjectSet: " + worldScreen.ObjectSet + Environment.NewLine;
            tb_output.Text += "ScreenIndexRight: " + worldScreen.ScreenIndexRight + Environment.NewLine;
            tb_output.Text += "ScreenIndexLeft: " + worldScreen.ScreenIndexLeft + Environment.NewLine;
            tb_output.Text += "ScreenIndexDown: " + worldScreen.ScreenIndexDown + Environment.NewLine;
            tb_output.Text += "ScreenIndexUp: " + worldScreen.ScreenIndexUp + Environment.NewLine;
            tb_output.Text += "DataPointer: " + worldScreen.DataPointer + Environment.NewLine;
            tb_output.Text += "ExitPosition: " + worldScreen.ExitPosition + Environment.NewLine;
            tb_output.Text += "TopTiles: " + worldScreen.TopTiles + Environment.NewLine; //Showing just the byte value here, even though we have the TileSection objects which can give more info
            tb_output.Text += "BottomTiles: " + worldScreen.BottomTiles + Environment.NewLine;//Showing just the byte value here, even though we have the TileSection objects which can give more info
            tb_output.Text += "WorldScreenColor: " + worldScreen.WorldScreenColor + Environment.NewLine;
            tb_output.Text += "SpritesColor: " + worldScreen.SpritesColor + Environment.NewLine;
            tb_output.Text += "Unknown: " + worldScreen.Unknown + Environment.NewLine;
            tb_output.Text += "Event: " + worldScreen.Event + Environment.NewLine;
        }


        public void UpdateDirectionSection(RandomizerModWorldScreen worldScreen)
        {
            tb_direction_up.Text = worldScreen.ScreenIndexUp.ToString("X2");
            tb_direction_down.Text = worldScreen.ScreenIndexDown.ToString("X2");
            tb_direction_left.Text = worldScreen.ScreenIndexLeft.ToString("X2");
            tb_direction_right.Text = worldScreen.ScreenIndexRight.ToString("X2");
        }

        private void UpdateTileSectionListBoxesSelection(RandomizerModWorldScreen worldScreen)
        {
        
            lb_tileSection_top.SelectedIndex = Convert.ToInt32(worldScreen.TopTiles);
            lb_tileSection_bottom.SelectedIndex = Convert.ToInt32(worldScreen.BottomTiles);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lb_worldScreens_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedWorldScreenIndex != lb_worldScreens.SelectedIndex)
            {

                _selectedWorldScreenIndex = lb_worldScreens.SelectedIndex;
                SelectWorldScreen(_selectedWorldScreenIndex);
            }

           
        }

        private void btn_move_up_Click(object sender, EventArgs e)
        {
            string tbText = tb_direction_up.Text;
            int worldScreenIndex = Utility.HexStringToInt(tbText);
            lb_worldScreens.SelectedIndex = worldScreenIndex;
           
        }

        private void btn_move_right_Click(object sender, EventArgs e)
        {
            string tbText = tb_direction_right.Text;
            int worldScreenIndex = Utility.HexStringToInt(tbText);
            lb_worldScreens.SelectedIndex = worldScreenIndex;
        
        }

        private void btn_move_left_Click(object sender, EventArgs e)
        {
            string tbText = tb_direction_left.Text;
            int worldScreenIndex = Utility.HexStringToInt(tbText);
            lb_worldScreens.SelectedIndex = worldScreenIndex;
          
        }

    

        private void btn_move_down_Click(object sender, EventArgs e)
        {
            string tbText = tb_direction_down.Text;
            int worldScreenIndex = Utility.HexStringToInt(tbText);
            lb_worldScreens.SelectedIndex = worldScreenIndex;

        }

      

        private void lb_tileSection_top_SelectedIndexChanged(object sender, EventArgs e)
        {
            _randomizerMod.UpdateWorldScreenTopTileSection(_selectedWorldScreenIndex, lb_tileSection_top.SelectedIndex);
            RefreshWorldScreenDrawing();
        }

        private void lb_tileSection_bottom_SelectedIndexChanged(object sender, EventArgs e)
        {
            _randomizerMod.UpdateWorldScreenBottomTileSection(_selectedWorldScreenIndex, lb_tileSection_bottom.SelectedIndex);
            RefreshWorldScreenDrawing();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void tb_direction_up_TextChanged(object sender, EventArgs e)
        {

            tb_direction_up.BackColor = Color.White;
        }

        private void tb_direction_down_TextChanged(object sender, EventArgs e)
        {
            tb_direction_down.BackColor = Color.White;
        }

        private void tb_direction_right_TextChanged(object sender, EventArgs e)
        {
            tb_direction_right.BackColor = Color.White;
        }

        private void tb_direction_left_TextChanged(object sender, EventArgs e)
        {
            tb_direction_left.BackColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Save Modified WS Directions
            try
            {
                RandomizerModWorldScreen currentWS = _randomizerMod.WorldScreens[_selectedWorldScreenIndex];

                currentWS.ScreenIndexUp = Convert.ToByte(tb_direction_up.Text);

                RandomizerModWorldScreen destinationWS = _randomizerMod.WorldScreens[currentWS.ScreenIndexUp];

                if (cb_link_back.Checked)
                {
                    destinationWS.ScreenIndexDown = (byte)_selectedWorldScreenIndex;
                }
            }
            catch { }

            try
            {
                RandomizerModWorldScreen currentWS = _randomizerMod.WorldScreens[_selectedWorldScreenIndex];

                currentWS.ScreenIndexDown = Convert.ToByte(tb_direction_down.Text);

                RandomizerModWorldScreen destinationWS = _randomizerMod.WorldScreens[currentWS.ScreenIndexDown];

                if (cb_link_back.Checked)
                {
                    destinationWS.ScreenIndexUp = (byte)_selectedWorldScreenIndex;
                }
            }
            catch { }

            try
            {
                RandomizerModWorldScreen currentWS = _randomizerMod.WorldScreens[_selectedWorldScreenIndex];

                currentWS.ScreenIndexRight = Convert.ToByte(tb_direction_right.Text);

                RandomizerModWorldScreen destinationWS = _randomizerMod.WorldScreens[currentWS.ScreenIndexRight];

                if (cb_link_back.Checked)
                {
                    destinationWS.ScreenIndexLeft = (byte)_selectedWorldScreenIndex;
                }
            }
            catch { }

            try
            {
                RandomizerModWorldScreen currentWS = _randomizerMod.WorldScreens[_selectedWorldScreenIndex];

                currentWS.ScreenIndexLeft = Convert.ToByte(tb_direction_left.Text);

                RandomizerModWorldScreen destinationWS = _randomizerMod.WorldScreens[currentWS.ScreenIndexLeft];

                if (cb_link_back.Checked)
                {
                    destinationWS.ScreenIndexRight = (byte)_selectedWorldScreenIndex;
                }
            }
            catch { }
        }

        private void btn_testDirections_Click(object sender, EventArgs e)
        {
            RandomizerModWorldScreen currentWS = _randomizerMod.WorldScreens[_selectedWorldScreenIndex];
            RandomizerModWorldScreen rightWS = _randomizerMod.WorldScreens[currentWS.ScreenIndexRight];
            RandomizerModWorldScreen leftWS = _randomizerMod.WorldScreens[currentWS.ScreenIndexLeft];
            RandomizerModWorldScreen topWS = _randomizerMod.WorldScreens[currentWS.ScreenIndexUp];
            RandomizerModWorldScreen bottomWS = _randomizerMod.WorldScreens[currentWS.ScreenIndexDown];

            bool rightScreenIsCompatable = currentWS.CollisionTest_Right_IsCompatable(rightWS);
            bool leftScreenIsCompatable = currentWS.CollisionTest_Left_IsCompatable(leftWS);
            bool topScreenIsCompatable = currentWS.CollisionTest_Up_IsCompatable(topWS);
            bool bottomScreenIsCompatable = currentWS.CollisionTest_Down_IsCompatable(bottomWS);

            if (!rightScreenIsCompatable) tb_direction_right.BackColor = Color.Red;

            if (!leftScreenIsCompatable) tb_direction_left.BackColor = Color.Red;

            if (!topScreenIsCompatable) tb_direction_up.BackColor = Color.Red;

            if (!bottomScreenIsCompatable) tb_direction_down.BackColor = Color.Red;



        }
    }
}
