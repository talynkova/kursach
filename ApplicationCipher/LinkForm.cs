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
    public partial class LinkForm : Form
    {
        public string link;
        public LinkForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            link = textBox1.Text;
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
