namespace RaceGUIObjectsLib
{
    partial class NiceButton
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
            this.pbNBtn = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbNBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // pbNBtn
            // 
            this.pbNBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbNBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbNBtn.Location = new System.Drawing.Point(0, 0);
            this.pbNBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbNBtn.Name = "pbNBtn";
            this.pbNBtn.Size = new System.Drawing.Size(80, 80);
            this.pbNBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbNBtn.TabIndex = 0;
            this.pbNBtn.TabStop = false;
            this.pbNBtn.Click += new System.EventHandler(this.pbNBtn_Click);
            this.pbNBtn.Paint += new System.Windows.Forms.PaintEventHandler(this.pbNBtn_Paint);
            this.pbNBtn.DoubleClick += new System.EventHandler(this.pbNBtn_DoubleClick);
            this.pbNBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbNBtn_MouseDown);
            this.pbNBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbNBtn_MouseUp);
            // 
            // NiceButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pbNBtn);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "NiceButton";
            this.Size = new System.Drawing.Size(80, 80);
            this.Load += new System.EventHandler(this.NiceButton_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbNBtn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbNBtn;
    }
}
