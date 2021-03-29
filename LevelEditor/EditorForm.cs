using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace LevelEditor
{
    //Author: Nathan Caron
    //Date: 2/12/21
    //Purpose: Main form used to create/edit levels
    public partial class EditorForm : Form
    {
        // JSON save data structure
        private struct MapData
        {
            public int Width;
            public int Height;
            public int[] Colors;
            // Each byte is an index in the Colors array
            public byte[] Data;
        }
        
        private int _mapWidth;
        private int _mapHeight;
        private PictureBox[,] _tiles;
        private bool _unsavedChanges;
        private Dictionary<string, Image> _images;
        
        private EditorForm()
        {
            InitializeComponent();
            
            _images = new Dictionary<string, Image>();
            string[] paths = Directory.EnumerateFiles(
                "Resources/", 
                "*.png", 
                SearchOption.TopDirectoryOnly).ToArray();
            
            foreach (string path in paths)
            {
                imagesList.Items.Add(path);
                _images[path] = Image.FromFile(path);
            }
        }

        /// <summary>
        /// Creates a blank map of size width and height
        /// </summary>
        /// <param name="mapWidth">The map's width</param>
        /// <param name="mapHeight">The map's height</param>
        public EditorForm(int mapWidth, int mapHeight) : this()
        {
            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
            AddTiles();
        }

        /// <summary>
        /// Load a map from a saved .level file
        /// </summary>
        /// <param name="savedMapFile"></param>
        public EditorForm(string savedMapFile) : this()
        {
            ReadMapData(File.ReadAllText(savedMapFile));
            Text = "Level Editor - " + savedMapFile.Split('\\').Last();
        }

        /// <summary>
        /// Adds and initializes blank PictureBoxes to the Map GroupBox
        /// </summary>
        private void AddTiles()
        {
            tilesPanel.Controls.Clear();
            _tiles = new PictureBox[_mapWidth, _mapHeight];
            int tileSize = tilesPanel.DisplayRectangle.Height / _mapHeight;
            for (int y = 0; y < _mapHeight; y++)
            {
                for (int x = 0; x < _mapWidth; x++)
                {
                    PictureBox tile = new PictureBox
                    { 
                        BackColor = Color.Transparent,
                        Location = new Point(
                            tilesPanel.DisplayRectangle.X + x * tileSize,
                            tilesPanel.DisplayRectangle.Y + y * tileSize),
                        Size = new Size(tileSize, tileSize)
                    };

                    tile.BorderStyle = BorderStyle.FixedSingle;
                    tile.SizeMode = PictureBoxSizeMode.Zoom;
                    tile.MouseDown += OnMapTileClick;
                    tile.MouseEnter += OnMapTileClick;
                    tilesPanel.Controls.Add(tile);
                    // Store (x, y) reference to PictureBox
                    _tiles[x, y] = tile;
                }
            }
        }
        
        /// <summary>
        /// Event raised to color a PictureBox with the currentTile color
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event args</param>
        private void OnMapTileClick(object sender, EventArgs e)
        {
            if (sender is PictureBox tile && MouseButtons == MouseButtons.Left)
            {
                tile.Capture = false;
                tile.Image = currentTile.Image;

                if (!_unsavedChanges)
                {
                    SetUnsavedChanges(true);
                }
            }
        }

        /// <summary>
        /// Event raised to set the color for the currentTile
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event args</param>
        private void OnColorTileClick(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                currentTile.BackColor = button.BackColor;
            }
        }

        /// <summary>
        /// Setter for _unsavedChanges that also sets the title accordingly
        /// </summary>
        /// <param name="unsavedChanges">Are there unsaved changes</param>
        private void SetUnsavedChanges(bool unsavedChanges)
        {
            if (unsavedChanges)
            {
                Text = Text + " *";
            }
            else
            {
                Text = Text.Replace(" *", "");
            }
            
            _unsavedChanges = unsavedChanges;
        }
        
        /// <summary>
        /// Event raised to save the current map
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event args</param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// Opens a SaveFileDialog and writes the current map data to a file if possible
        /// </summary>
        private void Save()
        {
            SaveFileDialog saveDialog = new SaveFileDialog()
            {
                Title = "Save a level file.",
                Filter = "Level Files|*.level"
            };
            
            DialogResult result = saveDialog.ShowDialog();
            if (result == DialogResult.OK && saveDialog.FileName != "")
            {
                WriteMapData(saveDialog.FileName);
                SetUnsavedChanges(false);

                MessageBox.Show("File saved successfully", "File saved", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                Text = "Level Editor - " + saveDialog.FileName.Split('\\').Last();
            }
        }
        
        /// <summary>
        /// Event raised to load an existing map.
        /// Opens an OpenFileDialog and reads the given map if possible.
        /// Will prompt the user with an Save if there are unsaved changes.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event args</param>
        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog()
            {
                Title = "Open a level file",
                Filter = "Level Files|*.level"
            };

            DialogResult result = openDialog.ShowDialog();
            if (result == DialogResult.OK && openDialog.FileName != "")
            {
                if (_unsavedChanges)
                {
                    DialogResult saveResult = MessageBox.Show(
                        "There are unsaved changes. Are you sure you want to continue without saving?",
                        "Unsaved Changes", 
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Question);

                    if (saveResult == DialogResult.No)
                    {
                        Save();
                    }
                }
                
                ReadMapData(File.ReadAllText(openDialog.FileName));
                SetUnsavedChanges(false);

                MessageBox.Show("File loaded successfully", "File loaded",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                Text = "Level Editor - " + openDialog.FileName.Split('\\').Last();
            }
        }

        /// <summary>
        /// Event raised before the form is about to close.
        /// Will ask to save changes if there are unsaved changes.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Form closing event args</param>
        private void EditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_unsavedChanges)
            {
                DialogResult result = MessageBox.Show(
                    "There are unsaved changes. Are you sure you want to quit?",
                    "Unsaved Changes", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Loads a map from an existing file
        /// </summary>
        /// <param name="data">JSON formatted map data</param>
        private void ReadMapData(string data)
        {
            MapData mapData = JsonConvert.DeserializeObject<MapData>(data);

            _mapWidth = mapData.Width;
            _mapHeight = mapData.Height;
            AddTiles();
            
            using (BinaryReader rdr = new BinaryReader(new MemoryStream(mapData.Data)))
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    for (int x = 0; x < _mapWidth; x++)
                    {
                        Color tileColor = Color.FromArgb(mapData.Colors[rdr.ReadByte()]);
                        _tiles[x, y].BackColor = tileColor;
                    }
                }
            }
        }

        /// <summary>
        /// Writes the current map data to the given file
        /// </summary>
        /// <param name="fileName">The path to be written to</param>
        private void WriteMapData(string fileName)
        {

            MapData mapData = new MapData()
            {
                Width = _mapWidth,
                Height = _mapHeight,
                Data = new byte[Width * Height]
            };

            // Write data to the JSON struct
            using (BinaryWriter wtr = new BinaryWriter(new MemoryStream(mapData.Data)))
            {
                Dictionary<int, byte> colors = new Dictionary<int, byte>();
                for (int y = 0; y < _mapHeight; y++)
                {
                    for (int x = 0; x < _mapWidth; x++)
                    {
                        int tileColor = _tiles[x, y].BackColor.ToArgb();
                        if (!colors.ContainsKey(tileColor))
                        {
                            colors[tileColor] = (byte)colors.Count;
                        }

                        wtr.Write(colors[tileColor]);
                    }
                }
                
                mapData.Colors = colors.Keys.ToArray();
            }

            // Write the JSON struct to the .level file
            string mapString = JsonConvert.SerializeObject(mapData);
            File.WriteAllText(fileName, mapString);
        }

        private void imagesList_SelectedValueChanged(object sender, EventArgs e)
        {
            currentTile.Image = _images[(string) imagesList.SelectedItem];
        }
    }
}