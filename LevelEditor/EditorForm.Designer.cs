using System.ComponentModel;

namespace LevelEditor
{
    partial class EditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.currentTile = new System.Windows.Forms.PictureBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.mapGroupBox = new System.Windows.Forms.GroupBox();
            this.tilesPanel = new System.Windows.Forms.Panel();
            this.imagesList = new System.Windows.Forms.ListBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.currentTile)).BeginInit();
            this.mapGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.currentTile);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 161);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Current Tile";
            // 
            // currentTile
            // 
            this.currentTile.BackColor = System.Drawing.Color.Transparent;
            this.currentTile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.currentTile.Location = new System.Drawing.Point(32, 19);
            this.currentTile.Name = "currentTile";
            this.currentTile.Size = new System.Drawing.Size(136, 136);
            this.currentTile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.currentTile.TabIndex = 0;
            this.currentTile.TabStop = false;
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.saveButton.Location = new System.Drawing.Point(112, 449);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(100, 100);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save File";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.loadButton.Location = new System.Drawing.Point(12, 449);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(100, 100);
            this.loadButton.TabIndex = 3;
            this.loadButton.Text = "Load File";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // mapGroupBox
            // 
            this.mapGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mapGroupBox.Controls.Add(this.tilesPanel);
            this.mapGroupBox.Location = new System.Drawing.Point(215, 12);
            this.mapGroupBox.Margin = new System.Windows.Forms.Padding(0);
            this.mapGroupBox.Name = "mapGroupBox";
            this.mapGroupBox.Size = new System.Drawing.Size(524, 537);
            this.mapGroupBox.TabIndex = 4;
            this.mapGroupBox.TabStop = false;
            this.mapGroupBox.Text = "Map";
            // 
            // tilesPanel
            // 
            this.tilesPanel.AutoScroll = true;
            this.tilesPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tilesPanel.Location = new System.Drawing.Point(6, 19);
            this.tilesPanel.Name = "tilesPanel";
            this.tilesPanel.Size = new System.Drawing.Size(512, 512);
            this.tilesPanel.TabIndex = 0;
            // 
            // imagesList
            // 
            this.imagesList.FormattingEnabled = true;
            this.imagesList.Location = new System.Drawing.Point(12, 179);
            this.imagesList.Name = "imagesList";
            this.imagesList.Size = new System.Drawing.Size(200, 264);
            this.imagesList.TabIndex = 5;
            this.imagesList.SelectedValueChanged += new System.EventHandler(this.imagesList_SelectedValueChanged);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(744, 562);
            this.Controls.Add(this.imagesList);
            this.Controls.Add(this.mapGroupBox);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EditorForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Level Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditorForm_FormClosing);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.currentTile)).EndInit();
            this.mapGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel tilesPanel;

        private System.Windows.Forms.ListBox imagesList;

        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.GroupBox mapGroupBox;
        private System.Windows.Forms.Button saveButton;

        private System.Windows.Forms.PictureBox currentTile;
        private System.Windows.Forms.GroupBox groupBox2;

        #endregion
    }
}