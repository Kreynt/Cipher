using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cipher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//Названия переменных, объектов и методов сделаны такими специально, обычно я их нормально называю :-) 
        {
            string s = textBox1.Text;
            for (int j = 0; j < s.Length - 2; j += 2)
            {
                char ch = s[j];
                s = s.Remove(j, 1);
                s = s.Insert(j + 1, ch.ToString());
            }

            textBox2.Text = s;

            for (int j = 0; j < s.Length - 2; j += 2)
            {
                char ch = s[j];
                s = s.Remove(j, 1);
                s = s.Insert(j + 1, ch.ToString());
            }

            textBox3.Text = s;
        }
    }
}
