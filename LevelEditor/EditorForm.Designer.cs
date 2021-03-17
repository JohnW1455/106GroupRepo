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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.blackTile = new System.Windows.Forms.Button();
            this.blueTile = new System.Windows.Forms.Button();
            this.redTile = new System.Windows.Forms.Button();
            this.goldTile = new System.Windows.Forms.Button();
            this.greyTile = new System.Windows.Forms.Button();
            this.greenTile = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.currentTile = new System.Windows.Forms.PictureBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.mapGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.currentTile)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.blackTile);
            this.groupBox1.Controls.Add(this.blueTile);
            this.groupBox1.Controls.Add(this.redTile);
            this.groupBox1.Controls.Add(this.goldTile);
            this.groupBox1.Controls.Add(this.greyTile);
            this.groupBox1.Controls.Add(this.greenTile);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(119, 195);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tile Selector";
            // 
            // blackTile
            // 
            this.blackTile.BackColor = System.Drawing.Color.Black;
            this.blackTile.Location = new System.Drawing.Point(62, 131);
            this.blackTile.Name = "blackTile";
            this.blackTile.Size = new System.Drawing.Size(50, 50);
            this.blackTile.TabIndex = 5;
            this.blackTile.UseVisualStyleBackColor = false;
            this.blackTile.Click += new System.EventHandler(this.OnColorTileClick);
            // 
            // blueTile
            // 
            this.blueTile.BackColor = System.Drawing.Color.LightBlue;
            this.blueTile.Location = new System.Drawing.Point(6, 131);
            this.blueTile.Name = "blueTile";
            this.blueTile.Size = new System.Drawing.Size(50, 50);
            this.blueTile.TabIndex = 4;
            this.blueTile.UseVisualStyleBackColor = false;
            this.blueTile.Click += new System.EventHandler(this.OnColorTileClick);
            // 
            // redTile
            // 
            this.redTile.BackColor = System.Drawing.Color.Brown;
            this.redTile.Location = new System.Drawing.Point(62, 75);
            this.redTile.Name = "redTile";
            this.redTile.Size = new System.Drawing.Size(50, 50);
            this.redTile.TabIndex = 3;
            this.redTile.UseVisualStyleBackColor = false;
            this.redTile.Click += new System.EventHandler(this.OnColorTileClick);
            // 
            // goldTile
            // 
            this.goldTile.BackColor = System.Drawing.Color.Goldenrod;
            this.goldTile.Location = new System.Drawing.Point(6, 75);
            this.goldTile.Name = "goldTile";
            this.goldTile.Size = new System.Drawing.Size(50, 50);
            this.goldTile.TabIndex = 2;
            this.goldTile.UseVisualStyleBackColor = false;
            this.goldTile.Click += new System.EventHandler(this.OnColorTileClick);
            // 
            // greyTile
            // 
            this.greyTile.BackColor = System.Drawing.Color.Silver;
            this.greyTile.Location = new System.Drawing.Point(62, 19);
            this.greyTile.Name = "greyTile";
            this.greyTile.Size = new System.Drawing.Size(50, 50);
            this.greyTile.TabIndex = 1;
            this.greyTile.UseVisualStyleBackColor = false;
            this.greyTile.Click += new System.EventHandler(this.OnColorTileClick);
            // 
            // greenTile
            // 
            this.greenTile.BackColor = System.Drawing.Color.ForestGreen;
            this.greenTile.Location = new System.Drawing.Point(6, 19);
            this.greenTile.Name = "greenTile";
            this.greenTile.Size = new System.Drawing.Size(50, 50);
            this.greenTile.TabIndex = 0;
            this.greenTile.UseVisualStyleBackColor = false;
            this.greenTile.Click += new System.EventHandler(this.OnColorTileClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.currentTile);
            this.groupBox2.Location = new System.Drawing.Point(12, 213);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(119, 109);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Current Tile";
            // 
            // currentTile
            // 
            this.currentTile.BackColor = System.Drawing.Color.Transparent;
            this.currentTile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.currentTile.Location = new System.Drawing.Point(21, 19);
            this.currentTile.Name = "currentTile";
            this.currentTile.Size = new System.Drawing.Size(75, 75);
            this.currentTile.TabIndex = 0;
            this.currentTile.TabStop = false;
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.saveButton.Location = new System.Drawing.Point(12, 328);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(119, 119);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save File";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.loadButton.Location = new System.Drawing.Point(12, 453);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(119, 119);
            this.loadButton.TabIndex = 3;
            this.loadButton.Text = "Load File";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // mapGroupBox
            // 
            this.mapGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mapGroupBox.Location = new System.Drawing.Point(137, 12);
            this.mapGroupBox.Margin = new System.Windows.Forms.Padding(0);
            this.mapGroupBox.Name = "mapGroupBox";
            this.mapGroupBox.Size = new System.Drawing.Size(245, 560);
            this.mapGroupBox.TabIndex = 4;
            this.mapGroupBox.TabStop = false;
            this.mapGroupBox.Text = "Map";
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 581);
            this.Controls.Add(this.mapGroupBox);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EditorForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Level Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditorForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize) (this.currentTile)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.GroupBox mapGroupBox;
        private System.Windows.Forms.Button saveButton;

        private System.Windows.Forms.Button blackTile;
        private System.Windows.Forms.PictureBox currentTile;
        private System.Windows.Forms.GroupBox groupBox2;

        private System.Windows.Forms.Button blueTile;

        private System.Windows.Forms.Button redTile;

        private System.Windows.Forms.Button goldTile;

        private System.Windows.Forms.Button greyTile;

        private System.Windows.Forms.Button greenTile;
        private System.Windows.Forms.GroupBox groupBox1;

        #endregion
    }
}