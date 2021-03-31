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
        public struct LevelData
        {
            public int Width;
            public int Height;
            public string[] Objects;
            public byte[] Indices;
        }
        
        private int _mapWidth;
        private int _mapHeight;
        private PictureBox[,] _tiles;
        private bool _unsavedChanges;
        private Dictionary<string, Image> _images;
        private Dictionary<Image, string> _image2Name;
        
        private EditorForm()
        {
            InitializeComponent();
            
            _images = new Dictionary<string, Image>();
            _image2Name = new Dictionary<Image, string>();
            string[] paths = Directory.EnumerateFiles(
                "Content/", 
                "*.png", 
                SearchOption.TopDirectoryOnly).ToArray();
            
            foreach (string path in paths)
            {
                imagesList.Items.Add(path);
                _images[path] = Image.FromFile(path);

                string name = path.Split('/').Last().Replace(".png", "");
                _image2Name[_images[path]] = name;
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
                    tile.Image = _images["Content/empty.png"];
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
            LevelData mapData = JsonConvert.DeserializeObject<LevelData>(data);

            _mapWidth = mapData.Width;
            _mapHeight = mapData.Height;
            AddTiles();
            
            for (int y = 0; y < _mapHeight; y++)
            {
                for (int x = 0; x < _mapWidth; x++)
                {
                    byte index = mapData.Indices[y * _mapWidth + x];
                    string imageName = $"Content/{mapData.Objects[index]}.png";
                    Image image = _images[imageName];
                    _tiles[x, y].Image = image;
                }
            }
        }

        /// <summary>
        /// Writes the current map data to the given file
        /// </summary>
        /// <param name="fileName">The path to be written to</param>
        private void WriteMapData(string fileName)
        {

            LevelData mapData = new LevelData()
            {
                Width = _mapWidth,
                Height = _mapHeight,
                Indices = new byte[Width * Height]
            };
            
            Dictionary<string, byte> images = new Dictionary<string, byte>();
            for (int y = 0; y < _mapHeight; y++)
            {
                for (int x = 0; x < _mapWidth; x++)
                {
                    string imageName = _image2Name[_tiles[x, y].Image];
                    if (!images.ContainsKey(imageName))
                    {
                        images[imageName] = (byte)images.Count;
                    }

                    mapData.Indices[y * _mapWidth + x] = images[imageName];
                }
            }

            mapData.Objects = images.Keys.ToArray();
            string mapString = JsonConvert.SerializeObject(mapData);
            File.WriteAllText(fileName, mapString);

            // Write the JSON struct to the .level file

        }

        private void imagesList_SelectedValueChanged(object sender, EventArgs e)
        {
            currentTile.Image = _images[(string) imagesList.SelectedItem];
        }
    }
}