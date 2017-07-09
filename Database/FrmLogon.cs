using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace LCY_Database
{
    public enum UserType
    {
        ADMIN = 0, USER = 1, GUEST = 2, NONE = 3
    }

    public partial class FrmLogon : Form
    {
        public FrmLogon()
        {
            InitializeComponent();

        }
        private UserType _userType = UserType.GUEST;
        private string _userename = "guest";
        private string _connString = LCY_DBTools.DBTool.GetConnString();

        private void rbtRegular_CheckedChanged(object sender, EventArgs e)
        {
            RefreshStatus();
        }
        private void rbtGuest_CheckedChanged(object sender, EventArgs e)
        {
            RefreshStatus();
        }
        private void RefreshStatus()
        {
            if (rbtRegular.Checked)
            {
                textACC.Enabled = true;
                textPSW.Enabled = true;
            }
            else
            {
                textACC.Enabled = false;
                textPSW.Enabled = false;
            }
        }
        private void FrmLogon_Load(object sender, EventArgs e)
        {
            rbtRegular.Select();
            this.ActiveControl = textACC;

        }
        private void btLogon_Click(object sender, EventArgs e)
        {
            //
            // 防呆
            //
            if (string.IsNullOrWhiteSpace(textACC.Text) || string.IsNullOrWhiteSpace(textPSW.Text))
            {
                MessageBox.Show(null, "帳號/密碼不可為空", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (rbtGuest.Checked)
            {
                _userType = UserType.GUEST;
                _userename = "guest";
            }
            else
            {
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                    {
                        conn.Open();
                        NpgsqlCommand cmnd = new NpgsqlCommand("select emp_ename, role from employee where acc=:acc and pwd=:pwd", conn);
                        cmnd.Parameters.Add("acc", NpgsqlTypes.NpgsqlDbType.Char);
                        cmnd.Parameters.Add("pwd", NpgsqlTypes.NpgsqlDbType.Char);
                        cmnd.Parameters["acc"].SourceColumn = "acc";
                        cmnd.Parameters["pwd"].SourceColumn = "pwd";
                        cmnd.Parameters["acc"].Value = textACC.Text;
                        cmnd.Parameters["pwd"].Value = textPSW.Text;
                        NpgsqlDataReader reader = cmnd.ExecuteReader();
                        if (!reader.HasRows)
                        {
                            MessageBox.Show(null, "帳號/密碼不正確!\r\n 請洽管理者。", "",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                _userename = reader["emp_ename"].ToString();
                                _userType = (UserType)Enum.Parse(typeof(UserType), reader["role"].ToString(), true);
                            }
                            MessageBox.Show(null, "登入成功", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

        }
        public UserType GetUserType()
        {
            return _userType;
        }
        public string GetUserName()
        {
            return _userename;
        }
        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        private void textACC_KeyDown(object sender, KeyEventArgs e)
        {
            KeyEvent(e);
        }
        private void textPSW_KeyDown(object sender, KeyEventArgs e)
        {
            KeyEvent(e);
        }
        private void KeyEvent(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btLogon_Click(btLogon, new EventArgs());
            }
        }

    }
}
