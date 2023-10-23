namespace KODI_Cable_Network
{
    partial class LivePlayer
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
            this.DisplayText = new System.Windows.Forms.Label();
            this.BtnFullScreen = new System.Windows.Forms.Button();
            this.BtnClosedCaptions = new System.Windows.Forms.Button();
            this.TitleBar = new System.Windows.Forms.Timer(this.components);
            this.BtnPlayPause = new System.Windows.Forms.PictureBox();
            this.LoadingAnimation = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPlayPause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoadingAnimation)).BeginInit();
            this.SuspendLayout();
            // 
            // DisplayText
            // 
            this.DisplayText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayText.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayText.Location = new System.Drawing.Point(0, 0);
            this.DisplayText.Name = "DisplayText";
            this.DisplayText.Size = new System.Drawing.Size(624, 441);
            this.DisplayText.TabIndex = 0;
            this.DisplayText.Text = "Waiting";
            this.DisplayText.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // BtnFullScreen
            // 
            this.BtnFullScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnFullScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BtnFullScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFullScreen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.BtnFullScreen.ForeColor = System.Drawing.Color.Black;
            this.BtnFullScreen.Location = new System.Drawing.Point(555, 336);
            this.BtnFullScreen.Name = "BtnFullScreen";
            this.BtnFullScreen.Size = new System.Drawing.Size(64, 30);
            this.BtnFullScreen.TabIndex = 3;
            this.BtnFullScreen.TabStop = false;
            this.BtnFullScreen.Text = "FULL";
            this.BtnFullScreen.UseVisualStyleBackColor = false;
            this.BtnFullScreen.Click += new System.EventHandler(this.BtnFullScreen_Click);
            // 
            // BtnClosedCaptions
            // 
            this.BtnClosedCaptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClosedCaptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BtnClosedCaptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClosedCaptions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.BtnClosedCaptions.ForeColor = System.Drawing.Color.Black;
            this.BtnClosedCaptions.Location = new System.Drawing.Point(555, 300);
            this.BtnClosedCaptions.Name = "BtnClosedCaptions";
            this.BtnClosedCaptions.Size = new System.Drawing.Size(64, 30);
            this.BtnClosedCaptions.TabIndex = 4;
            this.BtnClosedCaptions.TabStop = false;
            this.BtnClosedCaptions.Text = "TEXT";
            this.BtnClosedCaptions.UseVisualStyleBackColor = false;
            this.BtnClosedCaptions.Click += new System.EventHandler(this.BtnSnapshot_Click);
            // 
            // TitleBar
            // 
            this.TitleBar.Enabled = true;
            this.TitleBar.Interval = 1000;
            this.TitleBar.Tick += new System.EventHandler(this.TitleBar_Tick);
            // 
            // BtnPlayPause
            // 
            this.BtnPlayPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPlayPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BtnPlayPause.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BtnPlayPause.Image = global::KODI_Cable_Network.Properties.Resources.PLPA;
            this.BtnPlayPause.Location = new System.Drawing.Point(555, 372);
            this.BtnPlayPause.Name = "BtnPlayPause";
            this.BtnPlayPause.Size = new System.Drawing.Size(64, 64);
            this.BtnPlayPause.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BtnPlayPause.TabIndex = 5;
            this.BtnPlayPause.TabStop = false;
            this.BtnPlayPause.Click += new System.EventHandler(this.BtnPlayPause_Click);
            // 
            // LoadingAnimation
            // 
            this.LoadingAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.LoadingAnimation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoadingAnimation.Image = global::KODI_Cable_Network.Properties.Resources.KCN;
            this.LoadingAnimation.Location = new System.Drawing.Point(0, 0);
            this.LoadingAnimation.Name = "LoadingAnimation";
            this.LoadingAnimation.Size = new System.Drawing.Size(624, 441);
            this.LoadingAnimation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.LoadingAnimation.TabIndex = 1;
            this.LoadingAnimation.TabStop = false;
            // 
            // LivePlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.BtnPlayPause);
            this.Controls.Add(this.BtnClosedCaptions);
            this.Controls.Add(this.BtnFullScreen);
            this.Controls.Add(this.LoadingAnimation);
            this.Controls.Add(this.DisplayText);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "LivePlayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "https://localhost/index.m3u8";
            this.Text = "KODI Cable Network";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LivePlayer_FormClosing);
            this.Load += new System.EventHandler(this.LivePlayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BtnPlayPause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoadingAnimation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label DisplayText;
        private System.Windows.Forms.PictureBox LoadingAnimation;
        private System.Windows.Forms.Button BtnFullScreen;
        private System.Windows.Forms.Button BtnClosedCaptions;
        private System.Windows.Forms.Timer TitleBar;
        private System.Windows.Forms.PictureBox BtnPlayPause;
    }
}