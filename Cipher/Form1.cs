﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

namespace Cipher//Названия переменных, объектов и методов сделаны такими специально, обычно я их нормально называю :-) 
{
    internal partial class Form1 : Form
    {
        internal Form1()
        {
            InitializeComponent();
        }

        private void B_Gen_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    string s = textBox1.Text;
                    if (CB_spec_char.Checked) s += "\r\n/_=+/'" + textBox5.Text;
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
                    string h = s.GetHashCode().ToString();
                    textBox6.Text = "";
                    foreach (char c in h)
                    {
                        string s2 = ((int)c).ToString();
                        textBox6.Text += s2;
                    }
                    string s1 = "";
                    foreach (char c in s)
                    {
                        string s2 = ((int)c).ToString();
                        while (s2.Length < 5) s2 = '0' + s2;
                        s1 += s2;
                    }
                    s = s1;
                    int[,] k = new int[100, 3];
                    Random x = new Random();
                    for (int j = 0; j < 100; j++)
                        for (int f = 0; f < 3; f++)
                            k[j, f] = x.Next(0, 10);
                    k[0, 0] = x.Next(101, 1000);
                    k[0, 1] = x.Next(11, 100);

                    int z = x.Next(91, 100);
                    for (int j = 0; j < 10; j++)
                    {
                        int d = x.Next(1, 5);
                        k[z, 0] = d;
                        if (d == 1)
                        {
                            int p = x.Next(1, 10);
                            k[z, 1] = p;
                            p *= k[0, 1];
                            while (s.Length < p)
                                p -= s.Length;
                            if (p == s.Length) p--;

                            StringBuilder sb = new StringBuilder(s);
                            for (int f = 0; f < p; f++)
                                sb.Remove(0, 1).Append(s[f]);
                            s = sb.ToString();
                        }
                        if (d == 2)
                        {
                            int p = x.Next(1, 10);
                            k[z, 1] = p;
                            p *= k[0, 1];
                            while (s.Length < p)
                                p -= s.Length;
                            if (p == s.Length) p--;

                            p = s.Length - p;
                            StringBuilder sb = new StringBuilder(s);
                            for (int f = 0; f < p; f++)
                                sb.Remove(0, 1).Append(s[f]);
                            s = sb.ToString();
                        }
                        if (d == 3)
                        {
                            int p = x.Next(2, 10);
                            k[z, 1] = p;
                            int f = 0;
                            string sk = "";
                            while (s[f] == '0')
                            {
                                sk += 0;
                                f++;
                            }
                            BigInteger b = BigInteger.Parse(s) * p;
                            s = sk + b.ToString();
                        }
                        if (d == 4)
                        {
                            int p = x.Next(1, 10);
                            k[z, 1] = p;
                            int l = 0;
                            while (p * 4 < s.Length - l)
                            {
                                string b1 = s.Substring(l, p);
                                string b2 = s.Substring(l + p * 2, p);
                                s = s.Remove(l, p);
                                s = s.Insert(l, b2);
                                s = s.Remove(l + p * 2, p);
                                s = s.Insert(l + p * 2, b1);
                                l += p;
                                b1 = s.Substring(l, p);
                                b2 = s.Substring(l + p * 2, p);
                                s = s.Remove(l, p);
                                s = s.Insert(l, b2);
                                s = s.Remove(l + p * 2, p);
                                s = s.Insert(l + p * 2, b1);
                                l += 3 * p;
                            }
                        }
                        int y = x.Next(1, 10);
                        k[z - y, 2] = y;
                        if (j != 9) z -= y;
                    }
                    k[0, 2] = z;
                    string sm = "";
                    int m = 0;
                    while (s.Length - m > 3)
                    {
                        string b = (k[0, 0] + Convert.ToInt32(s.Substring(m, 3))).ToString();
                        if (b.Length < 4) b = x.Next(2, 10).ToString() + b;
                        sm += (x.Next(100, 1000).ToString() + b);
                        m += 3;
                    }
                    if (m < s.Length) sm += s.Substring(m);
                    s = sm;


                    textBox2.Text = s;
                    textBox4.Text = "";
                    for (int j = 0; j < 3; j++)
                        for (int f = 0; f < 100; f++)
                            textBox4.Text += k[f, j].ToString();
                }
                catch
                {
                    MessageBox.Show("Не удалось зашифровать текст", "Неудача", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox8.Text != "") && (textBox7.Text != ""))
                {
                    int[,] k = new int[100, 3];
                    int n = 0;
                    for (int j = 0; j < 3; j++)
                        for (int f = 0; f < 100; f++)
                        {
                            if (f == 0)
                            {
                                if (j == 0)
                                {
                                    k[0, 0] = Convert.ToInt32(textBox8.Text.Substring(n, 3));
                                    n += 3;
                                }
                                if (j == 1)
                                {
                                    k[0, 1] = Convert.ToInt32(textBox8.Text.Substring(n, 2));
                                    n += 2;
                                }
                                if (j == 2)
                                {
                                    k[0, 2] = Convert.ToInt32(textBox8.Text.Substring(n, 2));
                                    n += 2;
                                }
                            }
                            else
                            {
                                k[f, j] = Convert.ToInt32(textBox8.Text.Substring(n, 1));
                                n += 1;
                            }
                        }
                    string s = textBox7.Text;
                    string sm = "";
                    int m = 0;
                    while (s.Length - m > 7)
                    {
                        string b = s.Substring(m, 7);
                        b = b.Remove(0, 3);
                        if (b[0] != '1') b = b.Remove(0, 1);
                        b = (Convert.ToInt32(b) - k[0, 0]).ToString();
                        while (b.Length < 3) b = '0' + b;
                        sm += b;
                        m += 7;
                    }
                    if (m < s.Length) sm += s.Substring(m);
                    s = sm;

                    int z = k[0, 2]; ;
                    for (int j = 0; j < 10; j++)
                    {
                        int d = k[z, 0];
                        int p = k[z, 1];
                        if (d == 2)
                        {
                            p *= k[0, 1];
                            while (s.Length < p)
                                p -= s.Length;
                            if (p == s.Length) p--;
                            StringBuilder sb = new StringBuilder(s);
                            for (int f = 0; f < p; f++)
                                sb.Remove(0, 1).Append(s[f]);
                            s = sb.ToString();
                        }
                        if (d == 1)
                        {
                            p *= k[0, 1];
                            while (s.Length < p)
                                p -= s.Length;
                            if (p == s.Length) p--;
                            p = s.Length - p;
                            StringBuilder sb = new StringBuilder(s);
                            for (int f = 0; f < p; f++)
                                sb.Remove(0, 1).Append(s[f]);
                            s = sb.ToString();
                        }
                        if (d == 3)
                        {
                            int f = 0;
                            string sk = "";
                            while (s[f] == '0')
                            {
                                sk += 0;
                                f++;
                            }
                            BigInteger b = BigInteger.Parse(s) / p;
                            s = sk + b.ToString();

                        }
                        if (d == 4)
                        {
                            int l = 0;
                            while (p * 4 < s.Length - l)
                            {
                                string b1 = s.Substring(l, p);
                                string b2 = s.Substring(l + p * 2, p);
                                s = s.Remove(l, p);
                                s = s.Insert(l, b2);
                                s = s.Remove(l + p * 2, p);
                                s = s.Insert(l + p * 2, b1);
                                l += p;
                                b1 = s.Substring(l, p);
                                b2 = s.Substring(l + p * 2, p);
                                s = s.Remove(l, p);
                                s = s.Insert(l, b2);
                                s = s.Remove(l + p * 2, p);
                                s = s.Insert(l + p * 2, b1);
                                l += 3 * p;
                            }
                        }
                        z += k[z, 2];
                    }
                    string s1 = "";
                    for (int j = 0; j < s.Length - 4; j += 5)
                    {
                        int a = Convert.ToInt32(s[j].ToString() + s[j + 1].ToString() + s[j + 2].ToString() + s[j + 3].ToString() + s[j + 4].ToString());
                        s1 += ((char)a).ToString();
                    }
                    s = s1;
                    string h = s.GetHashCode().ToString();
                    string sh = "";
                    foreach (char c in h)
                    {
                        string s2 = ((int)c).ToString();
                        sh += s2;
                    }
                    if (sh == textBox9.Text)
                    {
                        label12.Text = "Проверка пройдена. Достоверность\r\nтекста и подписи подтверждена.";
                        label12.ForeColor = Color.FromArgb(0, 190, 0);
                    }
                    else
                    {
                        label12.Text = "Проверка провалена!!!\r\nНас обманули! Достоверность нарушена!!!";
                        label12.ForeColor = Color.Red;
                    }
                    s = new string(s.ToCharArray().Reverse().ToArray());
                    int i = 0;
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
                    string t = "";
                    if (s.LastIndexOf("/_=+/'") != -1)
                    {
                        t = s.Substring(s.LastIndexOf("/_=+/'") + 6);
                        s = s.Remove(s.LastIndexOf("/_=+/'") - 2);
                    }
                    textBox3.Text = s;
                    if (t.IndexOf("Q7H@_") == 0)
                    {
                        t = t.Remove(0, 5);
                        int l = 0;
                        int p = 4;
                        while (p * 4 < t.Length - l)
                        {
                            string b1 = t.Substring(l, p);
                            string b2 = t.Substring(l + p * 2, p);
                            t = t.Remove(l, p);
                            t = t.Insert(l, b2);
                            t = t.Remove(l + p * 2, p);
                            t = t.Insert(l + p * 2, b1);
                            l += p;
                            b1 = t.Substring(l, p);
                            b2 = t.Substring(l + p * 2, p);
                            t = t.Remove(l, p);
                            t = t.Insert(l, b2);
                            t = t.Remove(l + p * 2, p);
                            t = t.Insert(l + p * 2, b1);
                            l += 3 * p;
                        }
                        p = 71;
                        while (t.Length < p)
                            p -= t.Length;
                        if (p == t.Length) p--;
                        StringBuilder sb = new StringBuilder(t);
                        for (int f = 0; f < p; f++)
                            sb.Remove(0, 1).Append(t[f]);
                        t = sb.ToString();
                        int f1 = 0;
                        p = 58;
                        string sk = "";
                        while (t[f1] == '0')
                        {
                            sk += 0;
                            f1++;
                        }
                        BigInteger b = BigInteger.Parse(t) / p;
                        t = sk + b.ToString();
                        p = 23;
                        while (t.Length < p)
                            p -= t.Length;
                        if (p == t.Length) p--;
                        p = t.Length - p;
                        StringBuilder sb1 = new StringBuilder(t);
                        for (int f = 0; f < p; f++)
                            sb1.Remove(0, 1).Append(t[f]);
                        t = sb1.ToString();
                        string t1 = "";
                        for (int j = 0; j < t.Length - 4; j += 5)
                        {
                            int a = Convert.ToInt32(t[j].ToString() + t[j + 1].ToString() + t[j + 2].ToString() + t[j + 3].ToString() + t[j + 4].ToString());
                            t1 += ((char)a).ToString();
                        }
                        t = t1;
                        t = new string(t.ToCharArray().Reverse().ToArray());
                        i = 0;
                        foreach (char c in t)
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
                            t = t.Remove(i, 1);
                            t = t.Insert(i, ((char)a).ToString());
                            i++;
                        }
                        for (int j = 0; j < t.Length - 2; j += 2)
                        {
                            char ch = t[j];
                            t = t.Remove(j, 1);
                            t = t.Insert(j + 1, ch.ToString());
                        }
                        label13.Text = "Отправитель подтвержден ЭЦП.";
                    }
                    else
                    {
                        label13.Text = "";
                    }
                    textBox10.Text = t;
                }
            }
            catch
            {
                MessageBox.Show("Не удалось расшифровать текст", "Неудача", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox4.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox6.Text);
        }

        private void B_N_Click(object sender, EventArgs e)
        {
            string filePath = SelectFile();
            if (filePath != "")
            {
                textBox1.Text = File.ReadAllText(filePath, Encoding.Default);
            }
        }

        private string SelectFile()
        {
            try
            {
                using (var ofd = new OpenFileDialog())
                {
                    if (ofd.ShowDialog() != DialogResult.OK)
                    {
                        return string.Empty;
                    }
                    return ofd.FileName;
                }
            }
            catch
            {
                return ("");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string filePath = SelectFile();
            if (filePath != "")
            {
                textBox7.Text = File.ReadAllText(filePath, Encoding.UTF7);
            }
            textBox8.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Все файлы (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF7);
                sw.Write(textBox2.Text);
                sw.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.Default);
                sw.Write(textBox3.Text);
                sw.Close();
            }
        }
    }
}
