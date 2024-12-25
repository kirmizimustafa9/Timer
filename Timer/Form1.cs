using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Timer
{
    public partial class Form1 : Form
    {
        private int seconds = 0;
        private int minutes = 0;
        private int hours = 0;
        string strTime = "";
        string startFolder = AppDomain.CurrentDomain.BaseDirectory;
        List<string> lines = new List<string>();
        StreamReader sr;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
            //getting old session logs from logs.txt when form initialized.
            if (File.Exists($"{startFolder}/logs.txt"))
            {
                using (sr = new StreamReader($"{startFolder}/logs.txt"))
                {
                    if (sr.ReadLine() != null)
                    {
                        foreach (string line in File.ReadLines($"{startFolder}/logs.txt"))
                        {
                            lines.Add(line);
                            listBox1.Items.Add(line);
                        }
                    }
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds++;
            if (seconds == 60)
            {
                minutes++;
                seconds = 0;
            }
            if (minutes == 60)
            {
                hours++;
                minutes = 0;
            }
            lbl_hour.Text = hours.ToString();
            lbl_minute.Text = minutes.ToString();
            lbl_second.Text = seconds.ToString();
            strTime = $"{hours.ToString()}h {minutes.ToString()}m {seconds.ToString()}s";
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            btn_save.Enabled = false;
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            btn_save.Enabled = true;
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            hours = 0;
            minutes = 0;
            seconds = 0;
            btn_save.Enabled = true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string sessionName = textBox1.Text != string.Empty ? textBox1.Text : "unnamed session";
            listBox1.Items.Add($"{sessionName} {strTime} {DateTime.Now.ToString("dd.MM.yyyy")}");

            //writing data to log file
            using (StreamWriter writer = new StreamWriter($"{startFolder}/logs.txt", append: true))
            {
                writer.WriteLine($"{sessionName} {strTime} {DateTime.Now.ToString("dd.MM.yyyy")}");
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            //erasing log data start to end
            using (StreamWriter writer = new StreamWriter($"{startFolder}/logs.txt"))
            {
                listBox1.Items.Clear();
            }
        }
    }

}