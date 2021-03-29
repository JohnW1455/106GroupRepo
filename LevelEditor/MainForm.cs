using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelEditor
{
    //Author: Nathan Caron
    //Date: 2/12/21
    //Purpose: Creates and loads maps that can be saved to .level files
    public partial class MainForm : Form
    {
        private const int MinSize = 10;
        private const int MaxSize = 30;
        private const int MaxWidth = byte.MaxValue;
        private int _mapWidth;
        private int _mapHeight;
        
        private Queue<string> _errors;
        
        /// <summary>
        /// Creates and initializes a new MainForm
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            _errors = new Queue<string>();
        }
        
        /// <summary>
        /// Event raised to load an existing map.
        /// Opens an OpenFileDialog and creates a new EditorForm with the chosen .level
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
            if (result == DialogResult.OK)
            {
                EditorForm editorForm = new EditorForm(openDialog.FileName);
                editorForm.ShowDialog();
            }
        }

        /// <summary>
        /// Event raised to create a new map.
        /// Will display errors if there is a problem with the map size.
        /// Otherwise creates a new EditorForm with a map of given size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createButton_Click(object sender, EventArgs e)
        {
            VerifyMapSize();
            if (_errors.Count > 0)
            {
                DisplayErrors();
                return;
            }
            
            EditorForm editorForm = new EditorForm(_mapWidth, _mapHeight);
            editorForm.ShowDialog();
        }

        /// <summary>
        /// Checks that the given width and height are valid
        /// </summary>
        private void VerifyMapSize()
        {
            if (!int.TryParse(widthTextBox.Text, out _mapWidth))
            {
                _errors.Enqueue("'Width' is not a number");
                return;
            }

            if (_mapWidth < MinSize)
            {
                _errors.Enqueue("'Width' too small. Minimum is " + MinSize);
            }

            if (_mapWidth > MaxWidth)
            {
                _errors.Enqueue("'Width' too big. Maximum is " + MaxWidth);
            }
            
            if (!int.TryParse(heightTextBox.Text, out _mapHeight))
            {
                _errors.Enqueue("'Height' is not a number");
                return;
            }

            if (_mapHeight < MinSize)
            {
                _errors.Enqueue("'Height' too small. Minimum is " + MinSize);
            }

            if (_mapHeight > MaxSize)
            {
                _errors.Enqueue("'Height' too big. Maximum is " + MaxSize);
            }
        }

        /// <summary>
        /// Creates a MessageBox with current errors if any
        /// </summary>
        private void DisplayErrors()
        {
            string errorString = "Errors:\n";
            while (_errors.Count > 0)
            {
                string error = _errors.Dequeue();
                errorString += "- " + error + "\n";
            }

            MessageBox.Show(errorString, "Error creating map", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}