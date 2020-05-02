using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polybius
{
    public partial class Polybius : Form
    {
        bool mode = true;
        
        char[,] table = new char[5, 5] { { 'A', 'B', 'C', 'D', 'E' }, { 'F', 'G', 'H', 'I', 'K' }, { 'L', 'M', 'N', 'O', 'P' }, { 'Q', 'R', 'S', 'T', 'U' }, { 'V', 'W', 'X', 'Y', 'Z' } };
        Dictionary<char, Tuple<int, int>> table_dic = new Dictionary<char, Tuple<int, int>>();
        public Polybius()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    table_dic[table[i, j]] = new Tuple<int, int>(i, j);
                }
            }

            InitializeComponent();
        }
        public string Encrypt(string raw)
        {
            string encrypted = "";
            for (int i = 0; i < raw.Length; i++)
            {
                if (raw[i] <= 'Z' && raw[i] >= 'A' || raw[i]==' ')
                {
                    if (raw[i] == ' ')
                    {
                        encrypted += ' ';
                        
                    }
                    else if (raw[i] == 'J' || raw[i] == 'i')
                    {
                        encrypted += table_dic['I'].Item1 + 1;
                        encrypted += table_dic['I'].Item2 + 1;
                    }
                    else if (raw[i] != 'J')
                    {
                        encrypted += table_dic[raw[i]].Item1 + 1;
                        encrypted += table_dic[raw[i]].Item2 + 1;
                    }
                }
            }
            return encrypted;
        }
        public string Decrypt(string raw)
        {
            string Decrypt = "";

            for (int i = 0; i < raw.Length && raw.Length > 1; i++)
            {
                if (raw[i] == ' ')
                {
                    Decrypt += ' ';
                }
                else if (raw[i] >= '0' && raw[i] <= '9')
                {
                    if (raw.Length % 2 == 1)
                    {
                        raw = raw.Substring(0, raw.Length - 1);
                        Decrypt += table[Int32.Parse(raw[i].ToString()) - 1, Int32.Parse(raw[i + 1].ToString()) - 1];
                        i++;
                    }
                    else
                    {
                        //TODO: burada bir şeyler yaşanıyor onu bir çöz sana zahmet
                        Decrypt += table[Int32.Parse(raw[i].ToString()) - 1, Int32.Parse(raw[i + 1].ToString()) - 1];
                        i++;
                    }

                }
                
            }
            return Decrypt;
        }
        private void rawTextbox_Click(object sender, EventArgs e)
        {
            mode = true;
        }
        private void rawTextbox_TextChanged(object sender, EventArgs e)
        {
            if (mode)
                resultTextbox.Text = Encrypt(rawTextbox.Text.Replace("i", "ı").ToUpper());
            if (string.IsNullOrWhiteSpace(rawTextbox.Text))
            {
                resultTextbox.Text = String.Empty;
            }
        }
        private void resultTextbox_TextChanged(object sender, EventArgs e)
        {
            if (!mode)
                rawTextbox.Text = Decrypt(resultTextbox.Text.Replace("i", "I"));
            if (string.IsNullOrWhiteSpace(resultTextbox.Text))
            {
                rawTextbox.Text = String.Empty;
            }
        }
        private void resultTextbox_Click(object sender, EventArgs e)
        {
            mode = false;
        }
    }

}
