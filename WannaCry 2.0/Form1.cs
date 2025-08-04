using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using yt_DesignUI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WannaCry_2._0
{
    public partial class Form1 : Form
    {
        private readonly int firstTimer = 259200;
        private readonly int secondTimer = 604800;
        private ProgressBarVertical progressBarVertical1;
        private ProgressBarVertical progressBarVertical2;
        private Timer timer1;
        private Timer timer2;
        private readonly Font customFont;

        public Form1()
        {
            InitializeComponent();

            Text = "Wana Decrypt0r 2.0";
            customFont = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);

            Shown += Form1_Shown;

            richTextBox1.ReadOnly = true;

            DateTime dt = DateTime.Now;
            label9.Text = dt.AddDays(3).ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            label10.Text = dt.AddDays(7).ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            InitializeProgressBar1();
            InitializeProgressBar2();

            InitializeTimer1();
            InitializeTimer2();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            using (Graphics graphics = CreateGraphics())
            {
                string formTitle = Text.Trim();
                SizeF titleSize = graphics.MeasureString(formTitle, Font);
                float shiftRight = 37;
                float startingPoint = (ClientSize.Width - titleSize.Width) / 2 + shiftRight;
                float widthOfSpace = graphics.MeasureString(" ", Font).Width;
                string padding = "";
                float paddingWidth = 0;

                while (paddingWidth + widthOfSpace < startingPoint)
                {
                    padding += " ";
                    paddingWidth += widthOfSpace;
                }

                Text = padding + formTitle;
            }
        }

        private void InitializeProgressBar1()
        {
            progressBarVertical1 = new ProgressBarVertical();
            progressBarVertical1.Parent = groupBox1;
            progressBarVertical1.Location = new Point(210, 40);
            progressBarVertical1.Size = new Size(20, 90);
            progressBarVertical1.TotalTime = TimeSpan.FromSeconds(firstTimer);
            progressBarVertical1.StartColorTop = Color.Lime;
            progressBarVertical1.StartColorBottom = Color.Red;
            progressBarVertical1.EndColorTop = Color.Red;
            progressBarVertical1.EndColorBottom = Color.OrangeRed;
            progressBarVertical1.RemainingTime = progressBarVertical1.TotalTime;
            groupBox1.Controls.Add(progressBarVertical1);
        }

        private void InitializeProgressBar2()
        {
            progressBarVertical2 = new ProgressBarVertical();
            progressBarVertical2.Parent = groupBox2;
            progressBarVertical2.Location = new Point(210, 40);
            progressBarVertical2.Size = new Size(20, 90);
            progressBarVertical2.TotalTime = TimeSpan.FromSeconds(secondTimer);
            progressBarVertical2.StartColorTop = Color.Lime;
            progressBarVertical2.StartColorBottom = Color.Red;
            progressBarVertical2.EndColorTop = Color.Red;
            progressBarVertical2.EndColorBottom = Color.OrangeRed;
            progressBarVertical2.RemainingTime = progressBarVertical2.TotalTime;
            groupBox2.Controls.Add(progressBarVertical2);
        }

        private void InitializeTimer1()
        {
            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += Timer_Tick1;
            timer1.Start();
        }

        private void InitializeTimer2()
        {
            timer2 = new Timer();
            timer2.Interval = 1000;
            timer2.Tick += Timer_Tick2;
            timer2.Start();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.bitcoin.com/about-us/");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.investopedia.com/articles/investing/082914/basics-buying-and-investing-bitcoin.asp");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox1_SelectedIndexChanged(comboBox1, EventArgs.Empty);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please pay first! one file deleted.", "WannnaDecrypt0r");
            MessageBox.Show("No file has been deleted! this is fake wanna cry!", "FAKE WANNA CRY");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cannot connect to the server... Try agin later.");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Contact contact = new Contact();
            contact.Show();
        }

        private void Timer_Tick1(object sender, EventArgs e)
        {
            progressBarVertical1.RemainingTime = progressBarVertical1.RemainingTime.Subtract(TimeSpan.FromMilliseconds(timer1.Interval));

            TimeSpan time = progressBarVertical1.RemainingTime;
            label7.Text = time.ToString(@"dd\:hh\:mm\:ss");

            if (progressBarVertical1.RemainingTime <= TimeSpan.Zero)
            {
                timer1.Stop();
                progressBarVertical1.RemainingTime = TimeSpan.Zero;
                progressBarVertical1.Value = 0;
            }

            double progress = (progressBarVertical1.RemainingTime.TotalSeconds > 0) ? (progressBarVertical1.RemainingTime.TotalSeconds / progressBarVertical1.TotalTime.TotalSeconds) : 0;
            progressBarVertical1.Value = (int)(progress * 100);
            progressBarVertical1.Invalidate();
        }

        private void Timer_Tick2(object sender, EventArgs e)
        {
            progressBarVertical2.RemainingTime = progressBarVertical2.RemainingTime.Subtract(TimeSpan.FromMilliseconds(timer2.Interval));

            TimeSpan time = progressBarVertical2.RemainingTime;
            label8.Text = time.ToString(@"dd\:hh\:mm\:ss");

            if (progressBarVertical2.RemainingTime <= TimeSpan.Zero)
            {
                timer2.Stop();
                progressBarVertical2.RemainingTime = TimeSpan.Zero;
                progressBarVertical2.Value = 0;
            }

            double progress = (progressBarVertical2.RemainingTime.TotalSeconds > 0) ? (progressBarVertical2.RemainingTime.TotalSeconds / progressBarVertical2.TotalTime.TotalSeconds) : 0;
            progressBarVertical2.Value = (int)(progress * 100);
            progressBarVertical2.Invalidate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.SelectionStart = 0;
            comboBox1.SelectionLength = 0;

            if (comboBox1.SelectedItem != null)
            {
                string selectedItemName = comboBox1.SelectedItem.ToString();
                string resourceName = "WannaCry_2._0.msg." + selectedItemName + ".rtf";

                try
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();

                    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                    {
                        if (stream != null)
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                string fileContent = reader.ReadToEnd();

                                richTextBox1.Rtf = fileContent;
                            }
                        }
                        else
                        {
                            richTextBox1.Text = "Ресурс не найден: " + resourceName;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке ресурса: " + ex.Message);
                }
            }
        }
    }
}
