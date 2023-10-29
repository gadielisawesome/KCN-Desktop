using System;
using System.Windows.Forms;

namespace KODI_Cable_Network
{
    public partial class Test : Form
    {
        private DateTime startTime;
        private Timer timer;

        public Test()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 10; // Set the timer interval in milliseconds
            timer.Tick += timer_Tick;
            startTime = DateTime.Now;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            int pixelsPerSecond = 50; // Adjust the speed as needed
            int distance = (int)(pixelsPerSecond * elapsed.TotalSeconds);

            // Move the label to the left based on the elapsed time
            label1.Left =- distance;

            // Reset the label's position when it goes off-screen
            if (label1.Right < 0)
            {
                label1.Left = this.Width;
                startTime = DateTime.Now;
            }
        }
    }
}
