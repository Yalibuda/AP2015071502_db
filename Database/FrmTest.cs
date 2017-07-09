using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LCY_Database
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
            
        }
        string[] prods = { "PCMA", "TPO", "TPV", "SBC-C" };
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox tbx = (TextBox)sender;
            var x = tbx.Left;
            var y = tbx.Top + tbx.Height;
            var width = tbx.Width + 20;
            const int height = 40;

            ListBox listBox = new ListBox();            

            listBox1.SetBounds(x, y, width, height);
            listBox1.KeyDown += listBox_SelectedIndexChanged;

            List<string> localList = prods.Where(z => z.StartsWith(tbx.Text)).ToList();
            if (localList.Any() && !string.IsNullOrEmpty(tbx.Text))
            {
                listBox1.DataSource = localList;
                listBox1.Show();
                listBox1.Focus();

            }
        }
        void listBox_SelectedIndexChanged(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (decimal)Keys.Enter)
            {
                textBox2.Text = ((ListBox)sender).SelectedItem.ToString();
                listBox1.Hide();
            }
        }
    }
}
