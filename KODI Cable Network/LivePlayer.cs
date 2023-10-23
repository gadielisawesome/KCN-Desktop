﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;

namespace KODI_Cable_Network
{
    public partial class LivePlayer : Form
    {
        private readonly LibVLC _libVLC;
        private readonly MediaPlayer _mediaPlayer;

        public LivePlayer()
        {
            InitializeComponent();
            try
            {
                Core.Initialize();
                _libVLC = new LibVLC();
                _mediaPlayer = new MediaPlayer(_libVLC);

                var videoView = new VideoView
                {
                    Dock = DockStyle.Fill,
                    MediaPlayer = _mediaPlayer,
                };

                Controls.Add(videoView);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred.\n" + ex.Message);
                this.Close();
            }
            KeyPreview = true;
            this.KeyDown += (sender, e) =>
            {
                switch (e.KeyCode)
                {
                    case Keys.Space:
                        _mediaPlayer.Pause();
                        break;
                    case Keys.Up:
                        if (_mediaPlayer.Volume != 100) _mediaPlayer.Volume += 10;
                        break;
                    case Keys.Down:
                        if (_mediaPlayer.Volume != 0) _mediaPlayer.Volume -= 10;
                        break;
                    case Keys.M:
                        _mediaPlayer.ToggleMute();
                        break;
                    case Keys.F:
                        TriggerFullScreen();
                        break;
                    case Keys.OemPeriod:
                        _mediaPlayer.NextFrame();
                        break;
                    case Keys.Oemcomma:
                        // Future use - Reserved
                        break;
                }
            };
        }

        public string channel_title = "Not Provided";
        public string channel_rating = "Not Provided";

        private string GetMedia()
        {
            //string input = "https://certainurl.com/example.html|This is some title text|M";
            string[] parts = this.Tag.ToString().Split('|');

            if (parts.Length == 3)
            {
                string link = parts[0];
                string title = parts[1];
                string rating = parts[2];

                channel_title = title;
                channel_rating = rating;

                Console.WriteLine($"Opening {link}");
                Console.WriteLine($"Title:");
                Console.WriteLine($"{title}");
                Console.WriteLine($"Rating:");
                Console.WriteLine($"{rating}");
                switch (rating.ToUpper())
                {
                    case "?":
                        Console.WriteLine("Friendly Rating:");
                        Console.WriteLine("Pending Rating (?+)");
                        Console.WriteLine("User prompted with pending content warning.");
                        Console.WriteLine();
                        if (MessageBox.Show("The channel you selected is not rated.\nDo you want to continue?", "Content Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        {
                            DoNotContinueInit = true;
                        }
                        return parts[0];
                    case "E":
                        Console.WriteLine("Friendly Rating:");
                        Console.WriteLine("Everyone (0+)");
                        Console.WriteLine();
                        return parts[0];
                    case "P":
                        Console.WriteLine("Friendly Rating:");
                        Console.WriteLine("Parental Guidance (7+)");
                        Console.WriteLine();
                        return parts[0];
                    case "S":
                        Console.WriteLine("Friendly Rating:");
                        Console.WriteLine("Suggestive (13+)");
                        Console.WriteLine();
                        return parts[0];
                    case "M":
                        Console.WriteLine("Friendly Rating:");
                        Console.WriteLine("Mature (18+)");
                        Console.WriteLine("User prompted with mature content warning.");
                        Console.WriteLine();
                        if (MessageBox.Show("The channel you selected is rated mature.\nDo you want to continue?", "Content Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        {
                            DoNotContinueInit = true;
                        }
                        return parts[0];
                    default:
                        Console.WriteLine("Friendly Rating:");
                        Console.WriteLine("Unknown (?+)");
                        Console.WriteLine("User prompted with unknown rating content warning.");
                        Console.WriteLine();
                        if (MessageBox.Show("The channel you selected has an unknown rating.\nDo you want to continue?", "Content Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        {
                            DoNotContinueInit = true;
                        }
                        return parts[0];
                }
            }
            else
            {
                MessageBox.Show("The channel selected is unsupported.", "Unsupported Content", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DoNotContinueInit = true;
                return "https://localhost/index.m3u8";
            }
        }

        private bool DoNotContinueInit = false;

        private void LivePlayer_Load(object sender, EventArgs e)
        {
            _mediaPlayer.EncounteredError += PlaybackError;
            _mediaPlayer.Buffering += BufferMedia;
            _mediaPlayer.EndReached += EndMedia;
            _mediaPlayer.Opening += OpeningMedia;
            _mediaPlayer.Playing += PlayingMedia;
            _mediaPlayer.EnableMouseInput = true;
            _mediaPlayer.EnableKeyInput = false;
            try
            {
                _mediaPlayer.Media = new Media(_libVLC, new Uri(GetMedia()));
            }
            catch (UriFormatException)
            {
                Console.WriteLine("The URL provided is invalid. Cannot continue.");
                Console.WriteLine();
                MessageBox.Show("The URL provided from the server was invalid.", "Invalid URL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (DoNotContinueInit)
            {
                this.Close();
                return;
            }
            _mediaPlayer.Play();
            DisplayText.Hide();
            //_mediaPlayer.SetMarqueeString(VideoMarqueeOption.Enable | VideoMarqueeOption.Text | VideoMarqueeOption.Refresh, "This is scrolling text at the top.");
        }

        private void BufferMedia(object sender, MediaPlayerBufferingEventArgs e)
        {
            //this.Invoke(new MethodInvoker(() =>
            //{
                //while (_mediaPlayer.State == VLCState.Buffering)
                //{
                //    LoadingAnimation.Show();
                //}
                //LoadingAnimation.Hide();
            //}));
        }

        private void EndMedia(object sender, EventArgs e)
        {
            
        }

        private void OpeningMedia(object sender, EventArgs e)
        {
            DisplayText.Invoke(new MethodInvoker(() =>
            {
                //DisplayText.Text = "Connecting";
                //DisplayText.Show();
                LoadingAnimation.Show();
            }));
        }

        private void PlayingMedia(object sender, EventArgs e)
        {
            DisplayText.Invoke(new MethodInvoker(() =>
            {
                //DisplayText.Text = "";
                //DisplayText.Hide();
                LoadingAnimation.Hide();
            }));
        }

        // https://live.kodicable.net/hlscfsp/cfsp/index.m3u8

        private void PlaybackError(object sender, EventArgs e)
        {
            DisplayText.Invoke(new MethodInvoker(() =>
            {
                //DisplayText.Text = "Could not connect";
                //DisplayText.Show();
                LoadingAnimation.Show();
                MessageBox.Show("This stream is currently unavailable.", "KODI Cable Network", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose(true);
            }));
        }

        private void BtnPlayPause_Click(object sender, EventArgs e)
        {
            if (_mediaPlayer.IsPlaying)
            {
                _mediaPlayer.SetPause(true);
            }
            else
            {
                _mediaPlayer.Time = _mediaPlayer.Media.Duration;
                _mediaPlayer.SetPause(false);
            }
        }

        bool FullScreenState = false;
        bool WasMaximized = true;

        private void BtnFullScreen_Click(object sender, EventArgs e)
        {
            BtnPlayPause.Focus();
            TriggerFullScreen();
        }

        private void TriggerFullScreen()
        {
            if (!FullScreenState)
            {
                if (this.WindowState == FormWindowState.Maximized) WasMaximized = true;
                else WasMaximized = false;

                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Normal;
                this.Size = Screen.FromHandle(IntPtr.Zero).Bounds.Size;
                this.Location = new Point(0, 0);
                FullScreenState = true;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                if (WasMaximized) this.WindowState = FormWindowState.Maximized;
                else this.WindowState = FormWindowState.Normal;
                FullScreenState = false;
            }
        }

        private void LivePlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            TitleBar.Stop();
            this.Text = "KODI Cable Network";
            _mediaPlayer.Stop();
            _mediaPlayer.Dispose();
        }

        private async void TitleBar_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_mediaPlayer.State != VLCState.Ended && _mediaPlayer.State != VLCState.Stopped && _mediaPlayer.State != VLCState.Error && _mediaPlayer.State != VLCState.NothingSpecial)
                {
                    this.Text = $"{channel_title} - {_mediaPlayer.State} at {_mediaPlayer.Fps} FPS - {_mediaPlayer.Time}ms";
                }
                else
                {
                    this.Text = "KODI Cable Network";
                }
            }
            catch (Exception)
            {
                this.Text = "KODI Cable Network";
            }
        }

        private void BtnSnapshot_Click(object sender, EventArgs e)
        {
            BtnPlayPause.Focus();
            _mediaPlayer.TakeSnapshot(0, $"{DateTime.Now.ToString("s").Replace(":", "-")}.png", 0, 0);
            Thread.Sleep(1000);
        }
    }
}