using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCompiler
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public static string inp = "";
        private void Form3_Load(object sender, EventArgs e)
        {
            
        }
        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (string linecode in richTextBox1.Text.Split('\n'))
            {
                if (linecode != "")
                {
                    //inp = linecode;
                    Form1.input = linecode;
                }
                else
                {
                    this.Dispose();
                }
            }
        }
    }
}
