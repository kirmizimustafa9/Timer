﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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

        public Form1()
        {
            InitializeComponent();
            
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
            strTime = $"{hours.ToString()} Hours \n{minutes.ToString()} Minutes \n{seconds.ToString()} Seconds ";
            lbl_timer.Text = strTime;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            btn_save.Enabled = false;
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
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
            File.WriteAllText($"{startFolder}/logs.txt",
                    $"{textBox1.Text} {lbl_timer.Text.ToString()} {DateTime.Now.ToString("dd.MM.yyyy")}"
                    );
            listBox1.Items.Add($"{textBox1.Text} {lbl_timer.Text.ToString()} {DateTime.Now.ToString("dd.MM.yyyy")}");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btn_save.Enabled = false;
        }
        
    }
}