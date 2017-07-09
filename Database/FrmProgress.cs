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
    public partial class FrmProgress : Form
    {
        public FrmProgress()
        {
            InitializeComponent();
        }
        public void SetStyle(ProgressBarStyle style)
        {
            progressBar.Style = style;
        }
        public int ProgressValue
        {
            set
            {
                this.progressBar.Value = value;
                this.labProgress.Text = String.Format("進度: {0}%", value);               
            }
        }

        public void autoReport_progressChange(object sender, ProgressChangedEventArgs e)
        {
            int progressPercentage = (int)e.ProgressPercentage;
            this.progressBar.Value = progressPercentage;
            if (this.progressBar.Value == 0) this.Show();
            this.labProgress.Text = String.Format("進度: {0}%", progressPercentage);
            this.labStatus.Text = e.UserState.ToString();
            Application.DoEvents();
           
        }
    }
}
