﻿namespace KODI_Cable_Network
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LivePlayer));
            this.DisplayText = new System.Windows.Forms.Label();
            this.BtnFullScreen = new System.Windows.Forms.Button();
            this.BtnClosedCaptions = new System.Windows.Forms.Button();
            this.TitleBar = new System.Windows.Forms.Timer(this.components);
            this.HideControls = new System.Windows.Forms.Timer(this.components);
            this.Checker = new System.Windows.Forms.Timer(this.components);
            this.BtnPlayPause = new System.Windows.Forms.PictureBox();
            this.LoadingAnimation = new System.Windows.Forms.PictureBox();
            this.AckStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.informationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.fullscreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snapshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.BtnPlayPause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoadingAnimation)).BeginInit();
            this.AckStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // DisplayText
            // 
            this.DisplayText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayText.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayText.Location = new System.Drawing.Point(0, 0);
            this.DisplayText.Name = "DisplayText";
            this.DisplayText.Size = new System.Drawing.Size(624, 321);
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
            this.BtnFullScreen.Location = new System.Drawing.Point(485, 286);
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
            this.BtnClosedCaptions.Location = new System.Drawing.Point(415, 286);
            this.BtnClosedCaptions.Name = "BtnClosedCaptions";
            this.BtnClosedCaptions.Size = new System.Drawing.Size(64, 30);
            this.BtnClosedCaptions.TabIndex = 4;
            this.BtnClosedCaptions.TabStop = false;
            this.BtnClosedCaptions.Text = "SNAP";
            this.BtnClosedCaptions.UseVisualStyleBackColor = false;
            this.BtnClosedCaptions.Click += new System.EventHandler(this.BtnSnapshot_Click);
            // 
            // TitleBar
            // 
            this.TitleBar.Enabled = true;
            this.TitleBar.Interval = 1000;
            this.TitleBar.Tick += new System.EventHandler(this.TitleBar_Tick);
            // 
            // HideControls
            // 
            this.HideControls.Interval = 5000;
            this.HideControls.Tick += new System.EventHandler(this.HideControls_Tick);
            // 
            // Checker
            // 
            this.Checker.Enabled = true;
            this.Checker.Interval = 5000;
            // 
            // BtnPlayPause
            // 
            this.BtnPlayPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPlayPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.BtnPlayPause.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BtnPlayPause.Image = global::KODI_Cable_Network.Properties.Resources.PLPA;
            this.BtnPlayPause.Location = new System.Drawing.Point(555, 252);
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
            this.LoadingAnimation.Size = new System.Drawing.Size(624, 321);
            this.LoadingAnimation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.LoadingAnimation.TabIndex = 1;
            this.LoadingAnimation.TabStop = false;
            // 
            // AckStrip
            // 
            this.AckStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informationToolStripMenuItem,
            this.toolStripSeparator1,
            this.fullscreenToolStripMenuItem,
            this.snapshotToolStripMenuItem});
            this.AckStrip.Name = "RightClick";
            this.AckStrip.Size = new System.Drawing.Size(181, 98);
            // 
            // informationToolStripMenuItem
            // 
            this.informationToolStripMenuItem.Name = "informationToolStripMenuItem";
            this.informationToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.informationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.informationToolStripMenuItem.Text = "&Information";
            this.informationToolStripMenuItem.Click += new System.EventHandler(this.informationToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // fullscreenToolStripMenuItem
            // 
            this.fullscreenToolStripMenuItem.Name = "fullscreenToolStripMenuItem";
            this.fullscreenToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fullscreenToolStripMenuItem.Text = "Fullscreen";
            this.fullscreenToolStripMenuItem.Click += new System.EventHandler(this.fullscreenToolStripMenuItem_Click);
            // 
            // snapshotToolStripMenuItem
            // 
            this.snapshotToolStripMenuItem.Name = "snapshotToolStripMenuItem";
            this.snapshotToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.snapshotToolStripMenuItem.Text = "Snapshot";
            this.snapshotToolStripMenuItem.Click += new System.EventHandler(this.snapshotToolStripMenuItem_Click);
            // 
            // LivePlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.ClientSize = new System.Drawing.Size(624, 321);
            this.Controls.Add(this.BtnPlayPause);
            this.Controls.Add(this.BtnClosedCaptions);
            this.Controls.Add(this.BtnFullScreen);
            this.Controls.Add(this.LoadingAnimation);
            this.Controls.Add(this.DisplayText);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(640, 360);
            this.Name = "LivePlayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "https://localhost/index.m3u8";
            this.Text = "KODI Cable Network";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LivePlayer_FormClosing);
            this.Load += new System.EventHandler(this.LivePlayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BtnPlayPause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoadingAnimation)).EndInit();
            this.AckStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label DisplayText;
        private System.Windows.Forms.PictureBox LoadingAnimation;
        private System.Windows.Forms.Button BtnFullScreen;
        private System.Windows.Forms.Button BtnClosedCaptions;
        private System.Windows.Forms.Timer TitleBar;
        private System.Windows.Forms.PictureBox BtnPlayPause;
        private System.Windows.Forms.Timer HideControls;
        private System.Windows.Forms.Timer Checker;
        private System.Windows.Forms.ContextMenuStrip AckStrip;
        private System.Windows.Forms.ToolStripMenuItem informationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem fullscreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem snapshotToolStripMenuItem;
    }
}