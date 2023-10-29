using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using static KODI_Cable_Network.Program;

namespace KODI_Cable_Network
{
    internal static class Program
    {
        public class Stream
        {
            public string name { get; set; }
            public string url { get; set; }
            public string live { get; set; }
            public string rating { get; set; }
            public string title { get; set; }
            public string thumbnail { get; set; }
            public string description { get; set; }
        }

        public class StreamResponse
        {
            public List<Stream> streams { get; set; }
        }

        //public static void ProcessMessages()
        //{
        //    string apiURL = "https://streams.kodicable.net/api/streams";

        //    using (HttpClient client = new HttpClient())
        //    {
        //        try
        //        {
        //            HttpResponseMessage response = client.GetAsync(apiURL).Result;
        //            response.EnsureSuccessStatusCode();
        //            string responseBody = response.Content.ReadAsStringAsync().Result;
        //            Console.WriteLine(responseBody);
        //            return responseBody;
        //        }
        //        catch (Exception)
        //        {
        //            return "";
        //        }
        //    }
        //}

        public static Timer timer = new Timer();
        public static bool PlayerOpen = false;

        [STAThread]
        static void Main()
        {
            //new Test().ShowDialog();
            //return;

            Application.ThreadException += (sender, e) =>
            {
                //if (MessageBox.Show("KCN Desktop encountered an error.\nSend exception data to the developers? (uses webhook)", "Unfavorable Circumstances", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                //{
                //    return;
                //}
                MessageBox.Show($"An error caused KCN Desktop to shutdown.\n{e.Exception.Message} {e.Exception.Source}");
                Application.Exit();
            };

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FontLoader();

            if (!Directory.Exists("libvlc"))
            {
                MessageBox.Show("libvlc isn't available. Please re-download KCN Desktop.", "Required Reference Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Add MOTD or something for the title bar

            string[] arguments = Environment.GetCommandLineArgs();

            if (arguments.Length > 0)
            {
                int argument_count = 0;
                foreach (string arg in arguments)
                {
                    switch (argument_count)
                    {
                        case 0:
                            break;
                        case 1:
                            if (arg == "-play")
                            {
                                if (arguments.Length >= 2)
                                {
                                    string JSON = GetAPIData();

                                    if (JSON == "")
                                    {
                                        MessageBox.Show("Could not get data from KCN.", "Argument Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }

                                    StreamResponse response = JsonConvert.DeserializeObject<StreamResponse>(JSON);

                                    //string input = "https://certainurl.com/example.html|This is some title text|M";

                                    string BNUPipeFormat = string.Empty;
                                    Icon icon = null;
                                    Bitmap bitmap = null;

                                    foreach (Stream stream in response.streams)
                                    {
                                        if (stream.name.ToUpper() == arguments[2].ToUpper())
                                        {
                                            BNUPipeFormat = $"{stream.url}|{stream.title}|{stream.rating}";
                                            try
                                            {
                                                using (WebClient webClient = new WebClient())
                                                {
                                                    string imageUrl = $"https://kodicable.net/images/channel_logos/{stream.name.ToLower()}.png";
                                                    byte[] data = webClient.DownloadData(imageUrl);
                                                    if (data != null && data.Length > 0)
                                                    {
                                                        using (MemoryStream memoryStream = new MemoryStream(data))
                                                        {
                                                            bitmap = new Bitmap(memoryStream);
                                                            icon = Icon.FromHandle(bitmap.GetHicon());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        icon = null;
                                                    }
                                                }
                                            }
                                            catch (Exception)
                                            {
                                                icon = null;
                                            }
                                            break;
                                        }
                                    }

                                    if (string.IsNullOrEmpty(BNUPipeFormat))
                                    {
                                        MessageBox.Show("Channel not found.", "Argument Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }

                                    new LivePlayer { Tag = BNUPipeFormat, Icon = icon, logo = bitmap }.ShowDialog();

                                    return;
                                }
                                else
                                {
                                    MessageBox.Show("The provided arguments are invalid.", "Argument Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            if (arg == "-reset")
                            {
                                Properties.Settings.Default.Reset();
                                MessageBox.Show("Application reset successfully.", "Argument Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            MessageBox.Show("The arguments are invalid.", "Argument Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }
                    argument_count++;
                }
            }

            if (Properties.Settings.Default.FirstPowerOn)
            {
                Properties.Settings.Default.FirstPowerOn = false;
                Properties.Settings.Default.Save();
                new FusionPopup().ShowDialog();
            }

            if (!OpenStreamUI()) Environment.Exit(0);

            timer.Tick += (sender, args) => {
                if (!PlayerOpen) if (!OpenStreamUI()) Application.Exit();
            };
            timer.Interval = 300000;
            timer.Start();

            Application.Run();
        }

        private static void FontLoader()
        {
            //string tempFontPathA = Path.GetTempPath() + "Montserrat_Regular.ttf";
            //string tempFontPathB = Path.GetTempPath() + "Montserrat_SemiBold.ttf";

            //try
            //{
            //    FileStream fileA = new FileStream(tempFontPathA, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            //    byte[] fontBytesA = Properties.Resources.Montserrat_Regular;
            //    fileA.Write(fontBytesA, 0, fontBytesA.Length);
            //    fileA.Flush();
            //    fileA.Close();

            //    FileStream fileB = new FileStream(tempFontPathA, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            //    byte[] fontBytesB = Properties.Resources.Montserrat_SemiBold;
            //    fileB.Write(fontBytesB, 0, fontBytesB.Length);
            //    fileB.Flush();
            //    fileB.Close();

            //    int result = AddFontResourceA(tempFontPathA);

            //    Console.WriteLine(result != 0 ? "Font added to system font table." : "Failed to add font to system font table.");


            //    //File.SetAttributes(tempFontPath, FileAttributes.Normal);
            //    //File.Delete(tempFontPath);

            //    if (result != 0) SendMessage(0xFFFF, 0x001D, IntPtr.Zero, IntPtr.Zero);
            //}
            //catch (Exception)
            //{
            //    return;
            //}
        }

        static StreamList strList;

        /// <summary>
        /// Opens the stream UI. If already open, refreshes the window.
        /// </summary>
        /// <returns></returns>
        public static bool OpenStreamUI()
        {
            try
            {
                string JSON = GetAPIData();

                if (JSON == "")
                {
                    MessageBox.Show("Could not get data from KCN.", "KODI Cable Network", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                StreamResponse response = JsonConvert.DeserializeObject<StreamResponse>(JSON);

                int panelY = 5;
                bool RedirectStream = false;

                //Console.Beep();
                //Console.Write("Type a *.m3u8 URL to redirect all channels (leave empty to disable): ");
                //string output = Console.ReadLine();
                //if (output != "")
                //{
                //    RedirectStream = true;
                //    output = $"{output}|External Live Stream|UNDEFINED";
                //    // https://adultswim-vodlive.cdn.turner.com/live/rick-and-morty/stream.m3u8|Rick and Morty [Adult Swim]|M
                //}

                Control[] control = null;

                if (!(strList is null))
                {
                    strList.Invoke(new MethodInvoker(() =>
                    {
                        strList.AutoScroll = false;
                        foreach (Control ctrl in strList.Controls)
                        {
                            if (ctrl.Name == "streamPanel")
                            {
                                Array.Resize(ref control, control.Length + 1);
                                control[control.Length - 1] = ctrl;
                            }
                        }
                    }));
                }
                else
                {
                    strList = new StreamList();
                    strList.Show();
                }

                //strList.Invoke(new MethodInvoker(() =>
                //{
                //    // Properties.Resources.KCN_mini
                //    PictureBox waitingPictureBox = new PictureBox
                //    {
                //        BackColor = Color.Black,
                //        Location = new Point(0, 0),
                //        Size = new Size(240, 135),
                //        SizeMode = PictureBoxSizeMode.Zoom,
                //        Image = Properties.Resources.KCN_mini
                //    };
                //}));

                bool DarkMode = Properties.Settings.Default.DarkMode;

                foreach (Stream stream in response.streams)
                {
                    Console.WriteLine($"Name: {stream.name}");
                    Console.WriteLine($"URL: {stream.url}");
                    Console.WriteLine($"Icon URL: https://kodicable.net/images/channel_logos/{stream.name.ToLower()}.png");
                    Console.WriteLine($"Live: {stream.live}");
                    Console.WriteLine($"Rating: {stream.rating.ToUpper()}");
                    Console.WriteLine($"Title: {stream.title}");
                    Console.WriteLine($"Thumbnail: {stream.thumbnail}");
                    Console.WriteLine($"Description: {stream.description}");
                    Console.WriteLine();

                    //stream.live = "Yes";

                    strList.Invoke(new MethodInvoker(() =>
                    {
                        if (DarkMode)
                        {
                            strList.BackColor = Color.FromArgb(255, 0, 0, 0);
                        }
                        else
                        {
                            strList.BackColor = Color.FromArgb(255, 240, 240, 240);
                        }

                        Font MontserratSemiBoldTitle = null;
                        // 24
                        FontFamily MontSemiBold = FontFamily.Families.FirstOrDefault(f => f.Name == "Montserrat SemiBold");
                        if (MontSemiBold != null) MontserratSemiBoldTitle = new Font(MontSemiBold, 24, FontStyle.Regular, GraphicsUnit.Point);
                        else MontserratSemiBoldTitle = new Font("Segoe UI", 24, FontStyle.Regular, GraphicsUnit.Point);

                        Font MontserratRegularDescription = null;
                        // 17
                        FontFamily MontRegular = FontFamily.Families.FirstOrDefault(f => f.Name == "Montserrat");
                        if (MontRegular != null) MontserratRegularDescription = new Font(MontRegular, 17, FontStyle.Regular, GraphicsUnit.Point);
                        else MontserratRegularDescription = new Font("Segoe UI", 17, FontStyle.Regular, GraphicsUnit.Point);

                        if (stream.title.Contains("|"))
                        {
                            stream.title = stream.title.Replace("|", "⚫");
                        }

                        if (stream.rating.ToUpper().Contains("UNDEFINED"))
                        {
                            stream.rating = "?";
                        }

                        string BNUPipeFormat = "https://localhost/index.m3u8|Unknown|UNDEFINED";

                        if (RedirectStream)
                        {
                            stream.live = "Yes";
                            //BNUPipeFormat = output;
                        }
                        else BNUPipeFormat = $"{stream.url}|{stream.title}|{stream.rating}";

                        Icon icon = null;
                        Bitmap bitmap = null;

                        try
                        {
                            using (WebClient webClient = new WebClient())
                            {
                                string imageUrl = $"https://kodicable.net/images/channel_logos/{stream.name.ToLower()}.png";

                                // Download the image
                                byte[] data = webClient.DownloadData(imageUrl);

                                // Check if the data is not empty
                                if (data != null && data.Length > 0)
                                {
                                    using (MemoryStream memoryStream = new MemoryStream(data))
                                    {
                                        // Create a Bitmap from the image
                                        bitmap = new Bitmap(memoryStream);

                                        // Convert the Bitmap to an Icon
                                        icon = Icon.FromHandle(bitmap.GetHicon());
                                    }
                                }
                                else
                                {
                                    icon = null;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            icon = null;
                        }

                        // Create a new Panel for each stream
                        Panel streamPanel = new Panel
                        {
                            Width = 947, // Adjust the width as needed
                            Height = 135, // Adjust the height as needed
                            Location = new Point(11, panelY), // Set the Y-position
                            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                            AutoSize = false,
                            BorderStyle = BorderStyle.FixedSingle,
                            Tag = "MainPanel"
                        };
                        streamPanel.MouseClick += (sender, e) =>
                        {
                            switch (e.Button)
                            {
                                case MouseButtons.Left:
                                    strList.Invoke((MethodInvoker)(() => strList.PlayContent(BNUPipeFormat, icon, bitmap)));
                                    break;
                                case MouseButtons.Right:
                                    break;
                                default:
                                    break;
                            }
                        };
                        if (DarkMode)
                        {
                            streamPanel.BackColor = Color.FromArgb(60, 60, 60);
                            streamPanel.ForeColor = Color.White;
                        }
                        else
                        {
                            streamPanel.BackColor = Color.FromArgb(240, 240, 240);
                            streamPanel.ForeColor = Color.Black;
                        }

                        // Create a PictureBox for the stream thumbnail
                        PictureBox thumbnailPictureBox = new PictureBox
                        {
                            BackColor = Color.Black,
                            Location = new Point(0, 0),
                            Size = new Size(240, 135),
                            SizeMode = PictureBoxSizeMode.Zoom
                        };

                        if (stream.live == "Yes")
                        {
                            if (stream.rating.ToUpper() != "M") thumbnailPictureBox.ImageLocation = stream.thumbnail;
                            else thumbnailPictureBox.ImageLocation = "https://kodicable.net/images/mature-content.png";
                        }
                        else
                        {
                            if (DarkMode)
                            {
                                thumbnailPictureBox.Image = Properties.Resources.Unavailable;
                            }
                            else
                            {
                                thumbnailPictureBox.Image = Properties.Resources.UnavailableL;
                            }
                        }
                        if (DarkMode)
                        {
                            thumbnailPictureBox.InitialImage = Properties.Resources.Unavailable;
                            thumbnailPictureBox.ErrorImage = Properties.Resources.Unavailable;
                        }
                        else
                        {
                            thumbnailPictureBox.InitialImage = Properties.Resources.UnavailableL;
                            thumbnailPictureBox.ErrorImage = Properties.Resources.UnavailableL;
                        }
                        thumbnailPictureBox.MouseClick += (sender, e) =>
                        {
                            switch (e.Button)
                            {
                                case MouseButtons.Left:
                                    strList.Invoke((MethodInvoker)(() => strList.PlayContent(BNUPipeFormat, icon, bitmap)));
                                    break;
                                case MouseButtons.Right:
                                    break;
                                default:
                                    break;
                            }
                        };

                        // Create a new sub Panel for each stream
                        Panel subPanel = new Panel
                        {
                            Width = 64, // Adjust the width as needed
                            Height = 32, // Adjust the height as needed
                            Location = new Point(0, 0), // Set the Y-position
                            AutoSize = false,
                            BorderStyle = BorderStyle.None
                        };
                        if (DarkMode)
                        {
                            subPanel.BackColor = Color.FromArgb(60, 60, 60);
                            subPanel.ForeColor = Color.White;
                        }
                        else
                        {
                            subPanel.BackColor = Color.FromArgb(220, 220, 220);
                            subPanel.ForeColor = Color.Black;
                        }

                        // Create a PictureBox for the stream icon
                        PictureBox iconPictureBox = new PictureBox
                        {
                            BackColor = Color.Black,
                            Location = new Point(0, 0),
                            Size = new Size(32, 32),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            BorderStyle = BorderStyle.None,
                            ImageLocation = $"https://kodicable.net/images/channel_logos/{stream.name.ToLower()}.png",
                            InitialImage = Properties.Resources.KCN_mini,
                            ErrorImage = null
                        };
                        iconPictureBox.MouseClick += (sender, e) =>
                        {
                            switch (e.Button)
                            {
                                case MouseButtons.Left:
                                    MessageBox.Show($"KCN Callsign: {stream.name.ToUpper()}", "KODI Cable Network", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;
                                case MouseButtons.Right:
                                    break;
                                default:
                                    break;
                            }
                        };

                        // Create a PictureBox for the stream rating
                        PictureBox ratingPictureBox = new PictureBox
                        {
                            BackColor = Color.Black,
                            Location = new Point(32, 0),
                            Size = new Size(32, 32),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            BorderStyle = BorderStyle.None,
                            InitialImage = Properties.Resources.KCN_mini,
                            ErrorImage = null
                        };
                        switch (stream.rating.ToUpper())
                        {
                            case "E":
                                ratingPictureBox.ImageLocation = $"https://kodicable.net/images/ratings/everyone.png";
                                break;
                            case "P":
                                ratingPictureBox.ImageLocation = $"https://kodicable.net/images/ratings/parental_guidance.png";
                                break;
                            case "S":
                                ratingPictureBox.ImageLocation = $"https://kodicable.net/images/ratings/suggestive.png";
                                break;
                            case "M":
                                ratingPictureBox.ImageLocation = $"https://kodicable.net/images/ratings/mature.png";
                                break;
                            default:
                                ratingPictureBox.ImageLocation = $"https://kodicable.net/images/ratings/pending.png";
                                break;
                        }
                        ratingPictureBox.MouseClick += (sender, e) =>
                        {
                            switch (e.Button)
                            {
                                case MouseButtons.Left:
                                    switch (stream.rating.ToUpper())
                                    {
                                        case "E":
                                            MessageBox.Show($"BNU Rating: Everyone (0+)", "KODI Cable Network", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            break;
                                        case "P":
                                            MessageBox.Show($"BNU Rating: Parental Guidance (7+)", "KODI Cable Network", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            break;
                                        case "S":
                                            MessageBox.Show($"BNU Rating: Suggestive (13+)", "KODI Cable Network", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            break;
                                        case "M":
                                            MessageBox.Show($"BNU Rating: Mature (18+)", "KODI Cable Network", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            break;
                                        default:
                                            MessageBox.Show($"BNU Rating: Pending Rating (?+)", "KODI Cable Network", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            break;
                                    }
                                    break;
                                case MouseButtons.Right:
                                    break;
                                default:
                                    break;
                            }
                        };

                        // Create a Label for the stream title
                        Label titleLabel = new Label();
                        if (stream.live == "Yes")
                        {
                            // ⏣
                            //titleLabel.Text = Regex.Replace(stream.title, @"\p{Cs} ", "");
                            titleLabel.Text = stream.title;
                            if (DarkMode)
                            {
                                titleLabel.ForeColor = Color.White;
                            }
                            else
                            {
                                titleLabel.ForeColor = Color.Black;
                            }
                        }
                        else
                        {
                            titleLabel.Text = "Channel is off the air";
                            if (DarkMode)
                            {
                                titleLabel.ForeColor = Color.LightGray;
                            }
                            else
                            {
                                streamPanel.ForeColor = Color.Gray;
                            }
                            titleLabel.ForeColor = Color.Gray;
                        }
                        titleLabel.Font = MontserratSemiBoldTitle;
                        titleLabel.Location = new Point(240, 0);
                        titleLabel.Size = new Size(660, 50);
                        titleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                        titleLabel.AutoSize = true;
                        titleLabel.MouseClick += (sender, e) =>
                        {
                            switch (e.Button)
                            {
                                case MouseButtons.Left:
                                    strList.Invoke((MethodInvoker)(() => strList.PlayContent(BNUPipeFormat, icon, bitmap)));
                                    break;
                                case MouseButtons.Right:
                                    break;
                                default:
                                    break;
                            }
                        };

                        // Create a Label for the stream description
                        Label descriptionLabel = new Label();
                        if (stream.live == "Yes")
                        {
                            if (string.IsNullOrEmpty(stream.description))
                            {
                                descriptionLabel.Text = "The channel has not provided a description.";
                                if (DarkMode)
                                {
                                    descriptionLabel.ForeColor = Color.LightGray;
                                }
                                else
                                {
                                    descriptionLabel.ForeColor = Color.Gray;
                                }
                                descriptionLabel.ForeColor = Color.Gray;
                            }
                            else
                            {
                                descriptionLabel.Text = stream.description;
                                if (DarkMode)
                                {
                                    descriptionLabel.ForeColor = Color.White;
                                }
                                else
                                {
                                    descriptionLabel.ForeColor = Color.Black;
                                }
                            }
                        }
                        else
                        {
                            descriptionLabel.Text = "No description available while channel is offline.";
                            if (DarkMode)
                            {
                                descriptionLabel.ForeColor = Color.LightGray;
                            }
                            else
                            {
                                descriptionLabel.ForeColor = Color.Gray;
                            }
                        }
                        descriptionLabel.Font = MontserratRegularDescription;
                        descriptionLabel.Location = new Point(242, 40);
                        descriptionLabel.Size = new Size(670, 90);
                        descriptionLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                        descriptionLabel.AutoSize = false;
                        descriptionLabel.MouseClick += (sender, e) =>
                        {
                            switch (e.Button)
                            {
                                case MouseButtons.Left:
                                    strList.Invoke((MethodInvoker)(() => strList.PlayContent(BNUPipeFormat, icon, bitmap)));
                                    break;
                                case MouseButtons.Right:
                                    break;
                                default:
                                    break;
                            }
                        };

                        // Add controls to the streamPanel
                        subPanel.Controls.Add(iconPictureBox);
                        subPanel.Controls.Add(ratingPictureBox);
                        streamPanel.Controls.Add(subPanel);
                        streamPanel.Controls.Add(thumbnailPictureBox);
                        streamPanel.Controls.Add(titleLabel);
                        streamPanel.Controls.Add(descriptionLabel);

                        // Add the streamPanel to the StreamList form
                        strList.Controls.Add(streamPanel);

                        // Increase the Y-position for the next panel
                        panelY += 140; // You can adjust the value to control the vertical spacing

                        //if (control.Length > 0)
                        //{
                        //    Control firstPanel = control[0];
                        //    strList.Controls.Remove(firstPanel);
                        //    firstPanel.Dispose();

                        //    for (int i = 0; i < control.Length - 1; i++)
                        //    {
                        //        control[i] = control[i + 1];
                        //    }

                        //    Array.Resize(ref control, control.Length - 1);
                        //}
                    }));
                    Application.DoEvents();
                }

                strList.Invoke(new MethodInvoker(() =>
                {
                    strList.AutoScroll = true;
                    strList.IsLoading(false);
                }));

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deserializing JSON: " + ex.Message);
                return false;
            }
        }

        static private string GetAPIData()
        {
            string apiURL = "https://streams.kodicable.net/api/streams";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(apiURL).Result;
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(responseBody);
                    return responseBody;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        static public Bitmap ConvertToGrayscaleFromURL(string imageURL)
        {
            try
            {
                // Download the image from the internet
                WebClient webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(imageURL);

                // Create a MemoryStream from the downloaded image bytes
                using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                {
                    using (Bitmap original = new Bitmap(memoryStream))
                    {
                        // Create a new Bitmap with the same dimensions as the original
                        using (Bitmap grayscale = new Bitmap(original.Width, original.Height))
                        {
                            using (Graphics g = Graphics.FromImage(grayscale))
                            {
                                ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                                {
                                new float[] { 0.299f, 0.299f, 0.299f, 0, 0 },
                                new float[] { 0.587f, 0.587f, 0.587f, 0, 0 },
                                new float[] { 0.114f, 0.114f, 0.114f, 0, 0 },
                                new float[] { 0, 0, 0, 1, 0 },
                                new float[] { 0, 0, 0, 0, 1 }
                                });

                                using (ImageAttributes attributes = new ImageAttributes())
                                {
                                    attributes.SetColorMatrix(colorMatrix);

                                    // Draw the original image onto the new Bitmap using the Grayscale ColorMatrix
                                    g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                                        0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                                }
                            }

                            return grayscale;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return Properties.Resources.Unavailable;
            }
        }
    }
}
