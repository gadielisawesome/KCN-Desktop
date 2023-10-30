using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace KODI_Cable_Network
{
    public partial class FusionFirstTime : Form
    {
        private static LibVLC _libVLC;
        private static MediaPlayer _mediaPlayer;
        private static readonly VideoView videoView = new VideoView
        {
            Dock = DockStyle.Fill,
            
        };

        private bool AllowExit = false;

        public FusionFirstTime()
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
                AllowExit = true;
                this.Close();
            }
        }

        private void FusionFirstTime_Load(object sender, EventArgs e)
        {
            //this.BackColor = Color.FromArgb(255, 22, 255, 11);
            //this.TransparencyKey = Color.FromArgb(255, 22, 255, 11);
            _mediaPlayer.EncounteredError += (sender_, ev) =>
            {
                Console.WriteLine("An error occurred. Skipping.");
                Console.WriteLine();
                this.Invoke(new MethodInvoker(() =>
                {
                    AllowExit = true;
                    this.Dispose(true);
                }));
            };
            _mediaPlayer.EndReached += (sender_, ev) =>
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    AllowExit = true;
                    this.Dispose(true);
                }));
            };
            _mediaPlayer.EnableMouseInput = false;
            _mediaPlayer.EnableKeyInput = false;
            try
            {
                if (DateTime.Now.Month == 10 && DateTime.Now.Day == 31)
                {
                    _mediaPlayer.Media = new Media(_libVLC, "KCNStinger_Halloween.mp4", FromType.FromPath);
                }
                else _mediaPlayer.Media = new Media(_libVLC, "KCNStinger.mp4", FromType.FromPath);
            }
            catch (Exception)
            {
                Console.WriteLine("An error occurred. Skipping.");
                Console.WriteLine();
                AllowExit = true;
                this.Dispose(true);
                return;
            }
            _mediaPlayer.Play();
        }

        private void FusionFirstTime_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !AllowExit;
            if (AllowExit) this.Dispose(true);
        }
    }
}
