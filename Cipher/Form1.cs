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

            string s1 = "";
            foreach (char c in s)
            {
                string s2 = ((int)c).ToString();
                while (s2.Length < 5) s2 = '0' + s2;
                s1 += s2;
            }
            s = s1;

            textBox2.Text = s;

            s1 = "";
            for (int j = 0; j < s.Length - 4; j += 5)
            {
                int a = Convert.ToInt32(s[j].ToString() + s[j + 1].ToString() + s[j + 2].ToString() + s[j + 3].ToString() + s[j + 4].ToString());
                s1 += ((char)a).ToString();
            }
            s = s1;

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
