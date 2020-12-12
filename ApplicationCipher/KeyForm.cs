using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationCipher
{
    public partial class KeyForm : Form
    {
        //public string key;
        
        public KeyForm()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != '.' && l != '.')
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBox1.Text) && !textBox1.Text.Contains("."))
            {
                MainWindow.key = textBox1.Text;
                Close();
               // MessageBox.Show(MainWindow.key);
            }
            else MessageBox.Show("Please, enter the key");
        }

     
    }
}
