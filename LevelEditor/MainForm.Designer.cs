namespace LevelEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.createButton = new System.Windows.Forms.Button();
            this.heightTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.widthTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.loadButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.createButton);
            this.groupBox1.Controls.Add(this.heightTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.widthTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 152);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create New Map";
            // 
            // createButton
            // 
            this.createButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.createButton.Location = new System.Drawing.Point(6, 71);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(188, 74);
            this.createButton.TabIndex = 4;
            this.createButton.Text = "Create Map";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // heightTextBox
            // 
            this.heightTextBox.Location = new System.Drawing.Point(94, 45);
            this.heightTextBox.Name = "heightTextBox";
            this.heightTextBox.Size = new System.Drawing.Size(100, 20);
            this.heightTextBox.TabIndex = 3;
            this.heightTextBox.Text = "20";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Height (in tiles)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // widthTextBox
            // 
            this.widthTextBox.Location = new System.Drawing.Point(94, 19);
            this.widthTextBox.Name = "widthTextBox";
            this.widthTextBox.Size = new System.Drawing.Size(100, 20);
            this.widthTextBox.TabIndex = 1;
            this.widthTextBox.Text = "20";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Width (in tiles)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // loadButton
            // 
            this.loadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.loadButton.Location = new System.Drawing.Point(12, 12);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(200, 74);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "Load Map";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 253);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Level Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.TextBox heightTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox widthTextBox;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button loadButton;

        private System.Windows.Forms.GroupBox groupBox1;

        #endregion
    }
}