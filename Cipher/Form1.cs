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

            int i = 0;
            foreach (char c in s)
            {
                int a = (int)c;
                if (i % 2 == 0)
                {
                    a += (i + 10);
                }
                else
                {
                    a -= (i + 10);
                }
                while (a < 0) a += 65536;
                while (a > 65535) a -= 65536;
                s = s.Remove(i, 1);
                s = s.Insert(i, ((char)a).ToString());
                i++;
            }
            s = new string(s.ToCharArray().Reverse().ToArray());
            textBox2.Text = s;
            s = new string(s.ToCharArray().Reverse().ToArray());
            i = 0;
            foreach (char c in s)
            {
                int a = (int)c;
                if (i % 2 == 0)
                {
                    a -= (i + 10);
                }
                else
                {
                    a += (i + 10);
                }
                while (a < 0) a += 65536;
                while (a > 65535) a -= 65536;
                s = s.Remove(i, 1);
                s = s.Insert(i, ((char)a).ToString());
                i++;
            }

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
