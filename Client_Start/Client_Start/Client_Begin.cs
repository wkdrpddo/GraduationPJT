using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Client_Start
{
    public partial class Client_Begin : Form
    {
        ProcessStartInfo pStart = new ProcessStartInfo();
        Process p = new Process();
        String s = "";

        public Client_Begin()
        {
            InitializeComponent();
        }

        private void Broadcasting_Click(object sender, EventArgs e)
        {
            Button b = Broadcasting;
            accessProcess(b);
        }

        private void Watching_Click(object sender, EventArgs e)
        {
            Button b = Watching;
            accessProcess(b);
        }

        private void accessProcess(Button b)
        {
            s = Environment.CurrentDirectory;
            s = Path.GetFullPath(Path.Combine(s, @"..\..\..\..\"));

            pStart.RedirectStandardOutput = true;
            pStart.UseShellExecute = false;
            if (b.Text == "송출하기")
            {
                pStart.FileName = s + "\\B_super\\B_super\\bin\\Debug\\B_super.exe";
            }
            else
            {
                pStart.FileName = s + "\\Viewer\\Viewer\\bin\\Debug\\Viewer.exe";
            }


            p.StartInfo = pStart;
            p.EnableRaisingEvents = true;
            p.Start();

            this.Close();
        }

        private void Exit_B_Click(object sender, EventArgs e)
        {
            p.Close();
            p.Dispose();
            this.Close();
        }
    }
}
