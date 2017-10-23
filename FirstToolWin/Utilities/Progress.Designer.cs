namespace FirstToolWin.Utilities
{
    partial class Progress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Progress));
            this.rtb_Progress = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtb_Progress
            // 
            this.rtb_Progress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_Progress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_Progress.Location = new System.Drawing.Point(0, 0);
            this.rtb_Progress.Name = "rtb_Progress";
            this.rtb_Progress.ReadOnly = true;
            this.rtb_Progress.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtb_Progress.Size = new System.Drawing.Size(320, 203);
            this.rtb_Progress.TabIndex = 0;
            this.rtb_Progress.Text = "";
            // 
            // Progress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 204);
            this.Controls.Add(this.rtb_Progress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Progress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Progress";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Progress_FormClosing);
            this.Load += new System.EventHandler(this.MM_Progress_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_Progress;
    }
}