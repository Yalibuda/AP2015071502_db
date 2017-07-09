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
    public partial class FrmOption : Form
    {
        public FrmOption()
        {
            InitializeComponent();
            textServer.Focus();
        }

        private void FrmOption_Load(object sender, EventArgs e)
        {
            textServer.Text = Properties.Settings.Default.Server;
            textPort.Text = Properties.Settings.Default.Port;
            mtextAccount.Text = Properties.Settings.Default.UID;
            mtextPsw.Text = Properties.Settings.Default.PSW;
        }

        private void btDefault_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            textServer.Text = Properties.Settings.Default.Server;
            textPort.Text = Properties.Settings.Default.Port;
            mtextAccount.Text = Properties.Settings.Default.UID;
            mtextPsw.Text = Properties.Settings.Default.PSW;        
        }
        private void btOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Server = textServer.Text;
            Properties.Settings.Default.Port = textPort.Text;
            Properties.Settings.Default.UID = mtextAccount.Text;
            Properties.Settings.Default.PSW = mtextPsw.Text; 
            Properties.Settings.Default.Save();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

    }
}
