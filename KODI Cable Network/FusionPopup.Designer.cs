﻿namespace KODI_Cable_Network
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
            this.ContentHolderPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.titlebar = new System.Windows.Forms.Label();
            this.CloseThisWindow = new System.Windows.Forms.Timer(this.components);
            this.ContentHolderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentHolderPanel
            // 
            this.ContentHolderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContentHolderPanel.Controls.Add(this.label1);
            this.ContentHolderPanel.Controls.Add(this.label2);
            this.ContentHolderPanel.Controls.Add(this.titlebar);
            this.ContentHolderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentHolderPanel.Location = new System.Drawing.Point(0, 0);
            this.ContentHolderPanel.Name = "ContentHolderPanel";
            this.ContentHolderPanel.Size = new System.Drawing.Size(440, 298);
            this.ContentHolderPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(438, 181);
            this.label1.TabIndex = 1;
            this.label1.Text = "This project was not affiliated with KCN at the time of this build. Find updates " +
    "in Fusion or following servers for updates.\r\n\r\nKCN Desktop - Created by BunnyTub" +
    "\r\nSeal of approval from TDC";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 234);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(438, 62);
            this.label2.TabIndex = 2;
            this.label2.Text = "Continuing in 10...";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titlebar
            // 
            this.titlebar.BackColor = System.Drawing.Color.Red;
            this.titlebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlebar.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titlebar.Location = new System.Drawing.Point(0, 0);
            this.titlebar.Margin = new System.Windows.Forms.Padding(0);
            this.titlebar.Name = "titlebar";
            this.titlebar.Size = new System.Drawing.Size(438, 53);
            this.titlebar.TabIndex = 0;
            this.titlebar.Text = "DISCLAIMER";
            this.titlebar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.titlebar.Click += new System.EventHandler(this.titlebar_Click);
            // 
            // CloseThisWindow
            // 
            this.CloseThisWindow.Enabled = true;
            this.CloseThisWindow.Interval = 1000;
            this.CloseThisWindow.Tick += new System.EventHandler(this.CloseThisWindow_Tick);
            // 
            // FusionPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(440, 298);
            this.ControlBox = false;
            this.Controls.Add(this.ContentHolderPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FusionPopup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EASEncoder Fusion";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FusionPopup_FormClosing);
            this.Load += new System.EventHandler(this.FusionPopup_Load);
            this.ContentHolderPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ContentHolderPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label titlebar;
        private System.Windows.Forms.Timer CloseThisWindow;
        private System.Windows.Forms.Label label2;
    }
}