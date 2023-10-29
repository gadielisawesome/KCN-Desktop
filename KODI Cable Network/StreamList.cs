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
    }
}
