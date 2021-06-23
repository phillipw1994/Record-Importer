using System.Windows.Forms;

namespace GUIObjectsLib.Option
{
    partial class SelectionForm : Form
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
            this.lbxOption = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbxOption
            // 
            this.lbxOption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxOption.FormattingEnabled = true;
            this.lbxOption.Location = new System.Drawing.Point(12, 12);
            this.lbxOption.Name = "lbxOption";
            this.lbxOption.Size = new System.Drawing.Size(214, 160);
            this.lbxOption.Sorted = true;
            this.lbxOption.TabIndex = 0;
            // 
            // SelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 182);
            this.Controls.Add(this.lbxOption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(327, 335);
            this.MinimumSize = new System.Drawing.Size(178, 135);
            this.Name = "SelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Your Option";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxOption;
    }
}