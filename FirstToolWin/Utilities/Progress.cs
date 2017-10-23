using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FirstToolWin.Utilities
{
    public partial class Progress : Form
    {
        public Progress()
        {
            InitializeComponent();
        }

        internal void _UpdateConsole(object obj, IMEventArgs args)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() =>
                {
                    this.rtb_Progress.AppendText(DateTime.Now.ToString("yy/MM/dd HH:mm:ss") + " - " + args.Message + Environment.NewLine);
                    this.rtb_Progress.SelectionStart = this.rtb_Progress.Text.Length;
                    this.rtb_Progress.ScrollToCaret();
                }));
            }
            else
            {
                try
                {
                    this.rtb_Progress.AppendText(DateTime.Now.ToString("yy/MM/dd HH:mm:ss") + " - " + args.Message + Environment.NewLine);
                    this.rtb_Progress.SelectionStart = this.rtb_Progress.Text.Length;
                    this.rtb_Progress.ScrollToCaret();

                }
                catch (Exception)
                {

                }
            }
        }

        void MM_Progress_Load(object sender, EventArgs e)
        {

        }
        void Progress_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!System.IO.Directory.Exists("C:\\TEMP\\"))
            //    System.IO.Directory.CreateDirectory("C:\\TEMP\\");

            //System.IO.File.WriteAllText("C:\\TEMP\\log_FirstToolWin.txt", this.rtb_Progress.Text);
        }


    }
}
