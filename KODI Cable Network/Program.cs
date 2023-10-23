using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        [STAThread]
        static void Main()
        {
            //new LivePlayer { Tag = "https://live.kodicable.net/hlscfsp/cfsp/index.m3u8" }.ShowDialog();
            //return;

            Application.EnableVisualStyles();

            new FusionPopup().ShowDialog();

            if (!Directory.Exists("libvlc"))
            {
                if (MessageBox.Show("libvlc is not downloaded.\nDownload it now?", "Required Reference Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
                {
                    return;
                }

            }

            StreamList strList = new StreamList();
            strList.Show();

            Thread.Sleep(500);

            string jsonResponse = GetAPIData();

            if (jsonResponse == "")
            {
                MessageBox.Show("Could not get data from KCN.", "KODI Cable Network", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                StreamResponse response = JsonConvert.DeserializeObject<StreamResponse>(jsonResponse);

                int panelY = 5;
                bool RedirectStream = false;

                Console.Beep();
                Console.Write("Type a *.m3u8 URL to redirect all channels (leave empty to disable): ");
                string output = Console.ReadLine();
                if (output != "")
                {
                    RedirectStream = true;
                    output = $"{output}|External Live Stream|UNDEFINED";
                    // https://adultswim-vodlive.cdn.turner.com/live/rick-and-morty/stream.m3u8|Rick and Morty [Adult Swim]|M
                }

                foreach (var stream in response.streams)
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

                    strList.Invoke(new MethodInvoker(() =>
                    {
                        if (stream.title.Contains("|"))
                        {
                            stream.title = stream.title.Replace("|", "⚫");
                        }

                        if (stream.rating.ToUpper().Contains("UNDEFINED"))
                        {
                            stream.rating = "?";
                        }

                        string BNUPipeFormat = "https://localhost/index.m3u8";

                        if (RedirectStream)
                        {
                            stream.live = "Yes";
                            BNUPipeFormat = output;
                        } else BNUPipeFormat = $"{stream.url}|{stream.title}|{stream.rating}";

                        // Create a new Panel for each stream
                        Panel streamPanel = new Panel
                        {
                            Width = 911, // Adjust the width as needed
                            Height = 135, // Adjust the height as needed
                            BackColor = Color.FromArgb(60, 60, 60), // Set the background color
                            ForeColor = Color.White,
                            Location = new Point(11, panelY), // Set the Y-position
                            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                            AutoSize = false
                        };
                        streamPanel.MouseClick += (sender, e) =>
                        {
                            switch (e.Button)
                            {
                                case MouseButtons.Left:
                                    new LivePlayer { Tag = BNUPipeFormat }.ShowDialog();
                                    break;
                                case MouseButtons.Right:
                                    break;
                                default:
                                    break;
                            }
                        };

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
                            thumbnailPictureBox.Image = Properties.Resources.Unavailable;
                        }

                        thumbnailPictureBox.InitialImage = Properties.Resources.Unavailable;
                        thumbnailPictureBox.ErrorImage = Properties.Resources.Unavailable;
                        thumbnailPictureBox.MouseClick += (sender, e) =>
                        {
                            switch (e.Button)
                            {
                                case MouseButtons.Left:
                                    new LivePlayer { Tag = BNUPipeFormat }.ShowDialog();
                                    break;
                                case MouseButtons.Right:
                                    break;
                                default:
                                    break;
                            }
                        };

                        // Create a PictureBox for the stream icon
                        PictureBox iconPictureBox = new PictureBox
                        {
                            BackColor = Color.Black,
                            Location = new Point(0, 0),
                            Size = new Size(32, 32),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            BorderStyle = BorderStyle.FixedSingle,
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
                            BorderStyle = BorderStyle.FixedSingle
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
                        ratingPictureBox.InitialImage = Properties.Resources.KCN_mini;
                        ratingPictureBox.ErrorImage = null;
                        ratingPictureBox.MouseClick += (sender, e) =>
                        {
                            switch (e.Button)
                            {
                                case MouseButtons.Left:
                                    MessageBox.Show($"BNU Rating: {stream.rating.ToUpper()}", "KODI Cable Network", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            titleLabel.Text = stream.title;
                            titleLabel.ForeColor = Color.White;
                        }
                        else
                        {
                            titleLabel.Text = "Channel is off the air";
                            titleLabel.ForeColor = Color.Gray;
                        }
                        titleLabel.Font = new Font("Segoe UI Symbol", 24, FontStyle.Bold, GraphicsUnit.Point, 0);
                        titleLabel.Location = new Point(245, 0);
                        titleLabel.Size = new Size(660, 50);
                        titleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                        titleLabel.AutoSize = true;
                        titleLabel.MouseClick += (sender, e) =>
                        {
                            switch (e.Button)
                            {
                                case MouseButtons.Left:
                                    new LivePlayer { Tag = BNUPipeFormat }.ShowDialog();
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
                            if (stream.description == "" || stream.description is null)
                            {
                                descriptionLabel.Text = "The channel has not provided a description.";
                                descriptionLabel.ForeColor = Color.Gray;
                            }
                            else descriptionLabel.Text = stream.description;
                            descriptionLabel.ForeColor = Color.White;
                        }
                        else
                        {
                            descriptionLabel.Text = "No description available while channel is offline.";
                            descriptionLabel.ForeColor = Color.Gray;
                        }
                        descriptionLabel.Font = new Font("Segoe UI Symbol", 17, FontStyle.Regular, GraphicsUnit.Point, 0);
                        descriptionLabel.Location = new Point(245, 45);
                        descriptionLabel.Size = new Size(668, 85);
                        descriptionLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                        descriptionLabel.AutoSize = false;
                        descriptionLabel.MouseClick += (sender, e) =>
                        {
                            switch (e.Button)
                            {
                                case MouseButtons.Left:
                                    new LivePlayer { Tag = BNUPipeFormat }.ShowDialog();
                                    break;
                                case MouseButtons.Right:
                                    break;
                                default:
                                    break;
                            }
                        };

                        // Create a Label for the stream rating
                        Label ratingLabel = new Label
                        {
                            Text = $"Rated: {stream.rating}",
                            Font = new Font("Segoe UI Semibold", 9, FontStyle.Italic, GraphicsUnit.Point, 0),
                            Location = new Point(265, 105),
                            Size = new Size(160, 25),
                            //ratingLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                            TextAlign = ContentAlignment.BottomLeft
                        };

                        // Add controls to the streamPanel
                        //streamPanel.Controls.Add(ratingLabel);
                        streamPanel.Controls.Add(iconPictureBox);
                        streamPanel.Controls.Add(ratingPictureBox);
                        streamPanel.Controls.Add(thumbnailPictureBox);
                        streamPanel.Controls.Add(titleLabel);
                        streamPanel.Controls.Add(descriptionLabel);

                        // Add the streamPanel to the StreamList form
                        strList.Controls.Add(streamPanel);

                        // Increase the Y-position for the next panel
                        panelY += 140; // You can adjust the value to control the vertical spacing
                    }));
                }

                strList.Invoke(new MethodInvoker(() =>
                {
                    strList.AutoScroll = true;
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deserializing JSON: " + ex.Message);
                return;
            }

            Application.Run();
        }

        static private string GetAPIData()
        {
            string apiUrl = "https://streams.kodicable.net/api/streams";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;
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
