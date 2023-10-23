using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
