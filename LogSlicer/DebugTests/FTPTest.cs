using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace LogSlicer
{
    public partial class FTPTest : Form
    {
        public string file;
        public FTPTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "C:";
            dialog.Title = "Select file";

            switch (dialog.ShowDialog())
            {
                case (DialogResult.OK):
                    {
                        file = dialog.FileName;
                        break;
                    }
                case (DialogResult.Cancel):
                    {
                        Environment.Exit(1);
                        break;
                    }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Packer.SendToSupport(textBox1.Text, textBox2.Text, textBox3.Text, file));
        }
    }
}
