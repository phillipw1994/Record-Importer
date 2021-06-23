namespace RaceGUIObjectsLib
{
    partial class NiceTextbox
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
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbNTBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbNTBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pbNTBox
            // 
            this.pbNTBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbNTBox.Location = new System.Drawing.Point(0, 0);
            this.pbNTBox.Name = "pbNTBox";
            this.pbNTBox.Size = new System.Drawing.Size(64, 64);
            this.pbNTBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbNTBox.TabIndex = 0;
            this.pbNTBox.TabStop = false;
            this.pbNTBox.Click += new System.EventHandler(this.pbNBtn_Click);
            this.pbNTBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pbNBtn_Paint);
            this.pbNTBox.DoubleClick += new System.EventHandler(this.pbNBtn_DoubleClick);
            this.pbNTBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbNBtn_MouseDown);
            this.pbNTBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbNBtn_MouseUp);
            // 
            // NiceTextbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pbNTBox);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "NiceTextbox";
            this.Size = new System.Drawing.Size(64, 64);
            ((System.ComponentModel.ISupportInitialize)(this.pbNTBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbNTBox;
    }
}
