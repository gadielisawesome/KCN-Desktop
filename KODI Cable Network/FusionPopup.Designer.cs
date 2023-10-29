namespace KODI_Cable_Network
{
    partial class FusionPopup
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FusionPopup));
            this.ContentHolderPanel = new System.Windows.Forms.Panel();
            this.CloseThisWindow = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ContentHolderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ContentHolderPanel
            // 
            this.ContentHolderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContentHolderPanel.Controls.Add(this.pictureBox1);
            this.ContentHolderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentHolderPanel.Location = new System.Drawing.Point(0, 0);
            this.ContentHolderPanel.Name = "ContentHolderPanel";
            this.ContentHolderPanel.Size = new System.Drawing.Size(768, 512);
            this.ContentHolderPanel.TabIndex = 0;
            // 
            // CloseThisWindow
            // 
            this.CloseThisWindow.Enabled = true;
            this.CloseThisWindow.Interval = 1000;
            this.CloseThisWindow.Tick += new System.EventHandler(this.CloseThisWindow_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::KODI_Cable_Network.Properties.Resources.desktopbanner;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(766, 510);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.WaitOnLoad = true;
            // 
            // FusionPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(768, 512);
            this.ControlBox = false;
            this.Controls.Add(this.ContentHolderPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FusionPopup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KCN Desktop";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FusionPopup_FormClosing);
            this.Load += new System.EventHandler(this.FusionPopup_Load);
            this.ContentHolderPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ContentHolderPanel;
        private System.Windows.Forms.Timer CloseThisWindow;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}