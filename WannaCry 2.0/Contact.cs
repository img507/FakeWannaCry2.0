using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WannaCry_2._0
{
    public partial class Contact : Form
    {
        public Contact()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("bruh", "bruh");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cannot connect to the server... Try agin later.");
        }
    }
}
