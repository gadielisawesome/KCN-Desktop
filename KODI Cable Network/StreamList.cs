using System.Drawing;
using System.Windows.Forms;

namespace KODI_Cable_Network
{
    public partial class StreamList : Form
    {
        public StreamList()
        {
            InitializeComponent();
        }

        private void StreamList_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void IsLoading(bool loading)
        {
            loadPictureBox.Visible = loading;
            Application.DoEvents();
        }

        public void PlayContent(string BNUPipeFormat, Icon icon, Bitmap bitmap)
        {
            this.Invoke((MethodInvoker)(() => this.Hide()));
            this.Invoke((MethodInvoker)(() => new LivePlayer { Tag = BNUPipeFormat, Icon = icon, logo = bitmap }.ShowDialog()));
            this.Invoke((MethodInvoker)(() => this.Show()));
            this.Invoke((MethodInvoker)(() => this.AutoScroll = false));
            this.Invoke((MethodInvoker)(() => this.IsLoading(true)));
            Application.DoEvents();
            Program.OpenStreamUI();
        }
    }
}
