using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace KODI_Cable_Network
{
    public partial class LivePlayer : Form
    {
        private static LibVLC _libVLC;
        private static MediaPlayer _mediaPlayer;
        private static readonly VideoView videoView = new VideoView
        {
            Dock = DockStyle.Fill,
        };

        public LivePlayer()
        {
            InitializeComponent();
            try
            {
                Core.Initialize();
                _libVLC = new LibVLC();
                _mediaPlayer = new MediaPlayer(_libVLC);
                videoView.MediaPlayer = _mediaPlayer;
                this.Controls.Add(videoView);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred.\n" + ex.Message, MessageBox.Event.Error, MessageBoxButtons.OK);
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
            Program.PlayerOpen = true;
        }

        private string channel_title = "Not Provided";
        private string channel_rating = "Not Provided";
        public Bitmap logo = Properties.Resources.KCN;

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
                channel_rating = rating.ToUpper();

                Console.WriteLine($"Opening {link}");
                Console.WriteLine($"Title:");
                Console.WriteLine($"{title}");
                Console.WriteLine($"Rating:");
                Console.WriteLine($"{rating.ToUpper()}");
                switch (rating.ToUpper())
                {
                    case "?":
                        Console.WriteLine("Friendly Rating:");
                        Console.WriteLine("Pending Rating (?+)");
                        Console.WriteLine("User prompted with pending content warning.");
                        Console.WriteLine();
                        if (MessageBox.Show("The channel you selected is unrated.\nDo you want to continue?", MessageBox.Event.Question, MessageBoxButtons.YesNo) != DialogResult.Yes)
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
                        if (MessageBox.Show("The channel you selected is rated mature.\nDo you want to continue?", MessageBox.Event.Question, MessageBoxButtons.YesNo) != DialogResult.Yes)
                        {
                            DoNotContinueInit = true;
                        }
                        return parts[0];
                    default:
                        Console.WriteLine("Friendly Rating:");
                        Console.WriteLine("Unknown (?+)");
                        Console.WriteLine("User prompted with unknown rating content warning.");
                        Console.WriteLine();
                        if (MessageBox.Show("The channel you selected has an unknown rating.\nDo you want to continue?", MessageBox.Event.Question, MessageBoxButtons.YesNo) != DialogResult.Yes)
                        {
                            DoNotContinueInit = true;
                        }
                        return parts[0];
                }
            }
            else
            {
                MessageBox.Show("The channel selected is unsupported.", MessageBox.Event.Error, MessageBoxButtons.OK);
                DoNotContinueInit = true;
                return "https://localhost/index.m3u8";
            }
        }

        private bool DoNotContinueInit = false;

        private void LivePlayer_Load(object sender, EventArgs e)
        {
            int targetSize = 128;
            double aspectRatio = (double)logo.Width / logo.Height;
            int width = aspectRatio > 1 ? targetSize : (int)(targetSize * aspectRatio);
            int height = aspectRatio > 1 ? (int)(targetSize / aspectRatio) : targetSize;

            Bitmap logo_resized = new Bitmap(targetSize, targetSize);
            using (Graphics g = Graphics.FromImage(logo_resized))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                int x = (targetSize - width) / 2, y = (targetSize - height) / 2;
                g.DrawImage(logo, new Rectangle(x, y, width, height));
            }

            LoadingAnimation.Image = logo_resized;

            _mediaPlayer.EncounteredError += PlaybackError;
            _mediaPlayer.Buffering += BufferMedia;
            _mediaPlayer.EndReached += EndMedia;
            _mediaPlayer.Opening += OpeningMedia;
            _mediaPlayer.Playing += PlayingMedia;
            _mediaPlayer.EnableMouseInput = false;
            _mediaPlayer.EnableKeyInput = false;
            videoView.MouseClick += (sending, f) =>
            {
                switch (f.Button)
                {
                    case MouseButtons.Left:
                        if (_mediaPlayer.IsPlaying)
                        {
                            _mediaPlayer.SetPause(true);
                        }
                        else
                        {
                            _mediaPlayer.Time = _mediaPlayer.Media.Duration;
                            _mediaPlayer.SetPause(false);
                        }
                        break;
                    case MouseButtons.Right:
                        break;
                    default:
                        break;
                }
            };
            videoView.MouseHover += (sending, f) =>
            {
                //HideControls.Stop();
                //BtnPlayPause.Show();
                //BtnFullScreen.Show();
                //BtnClosedCaptions.Show();
                //HideControls.Start();
            };
            try
            {
                _mediaPlayer.Media = new Media(_libVLC, new Uri(GetMedia()));
            }
            catch (UriFormatException)
            {
                Console.WriteLine("The URL provided is invalid. Cannot continue.");
                Console.WriteLine();
                MessageBox.Show("The URL provided from the server was invalid.", MessageBox.Event.Error, MessageBoxButtons.OK);
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

        private void HideControls_Tick(object sender, EventArgs e)
        {
            BtnPlayPause.Hide();
            BtnFullScreen.Hide();
            BtnClosedCaptions.Hide();
            HideControls.Stop();
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
            DisplayText.Invoke(new MethodInvoker(() =>
            {
                //DisplayText.Text = "Connecting";
                //DisplayText.Show();
                LoadingAnimation.Show();
                Thread.Sleep(1000);
                this.Dispose(true);
            }));
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
                MessageBox.Show("This stream is currently unavailable.", MessageBox.Event.Error, MessageBoxButtons.OK);
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
            if (_mediaPlayer != null)
            {
                _mediaPlayer = null;
            }
            Program.PlayerOpen = false;
        }

        private async void TitleBar_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_mediaPlayer.State != VLCState.Ended && _mediaPlayer.State != VLCState.Stopped && _mediaPlayer.State != VLCState.Error && _mediaPlayer.State != VLCState.NothingSpecial)
                {
                    this.Text = $"{channel_title} | Rated {channel_rating} | {_mediaPlayer.State} at {_mediaPlayer.Fps} FPS - {_mediaPlayer.Time}ms";
                }
                else
                {
                    this.Text = $"{channel_title} | Disconnected";
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

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This project uses LibVLC for playback.", MessageBox.Event.Information, MessageBoxButtons.OK);
        }

        private void fullscreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BtnPlayPause.Focus();
            TriggerFullScreen();
        }

        private void snapshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BtnPlayPause.Focus();
            _mediaPlayer.TakeSnapshot(0, $"{DateTime.Now.ToString("s").Replace(":", "-")}.png", 0, 0);
            Thread.Sleep(1000);
        }
    }
}
