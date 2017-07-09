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
    public partial class FrmSelectProd : Form
    {
        public FrmSelectProd(string[] prods)
        {
            _prods = prods;
            InitializeComponent();
            btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            
#if DEBUG
            //treeViewProd.Nodes.Add("Products");

            //foreach (string item in new string[] { "PCMA", "TPV", "TPO", "SCB-C" })
            //{
            //    treeViewProd.Nodes[0].Nodes.Add(item);
            //}
           
#endif

            
        }

        public string[] _prods;
        private string _connString = LCY_DBTools.DBTool.GetConnString();

        private void LoadItems(string[] prods)
        {
            if (prods == null || prods.Length == 0)
            {
                //treeViewProd.SelectedNode = treeViewProd.Nodes[0];
                //treeViewProd.SelectedNode = treeViewProd.Nodes[0];
                //treeViewProd.SelectedNode.Checked = true;
                //foreach (TreeNode node in treeViewProd.Nodes[0].Nodes)
                //{
                //    node.Checked = true;
                //}
                return;
            }
            else
            {
                foreach (string item in prods)
                {
                    TreeNode[] treeNodes = treeViewProd.Nodes[0].Nodes.Cast<TreeNode>().Where(r => r.Text == item).ToArray();
                    if (treeNodes != null && treeNodes.Length > 0)
                    {
                        foreach (TreeNode node in treeNodes)
                        {
                            node.Checked = true;
                        }
                    }
                }
            }


        }

        private void treeViewSheet_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)//一定要加這段不然會無窮迴圈
            {
                if (e.Node.Checked)
                {
                    SetChildNodeCheckState(e.Node, true);
                }
                else
                {
                    SetChildNodeCheckState(e.Node, false);
                    if (e.Node.Parent != null)
                    {
                        SetParentCheckState(e.Node, false);
                    }
                }
            }
        }

        private void SetParentCheckState(TreeNode treeNode, bool state)
        {
            TreeNode parentNode = treeNode.Parent;
            parentNode.Checked = state;
            if (treeNode.Parent.Parent != null)
            {
                SetParentCheckState(parentNode, state);
            }
        }

        private void SetChildNodeCheckState(TreeNode treeNode, bool state)
        {
            TreeNodeCollection nodes = treeNode.Nodes;
            if (nodes.Count > 0)
            {
                foreach (TreeNode node in nodes)
                {
                    node.Checked = state;
                    SetChildNodeCheckState(node, state);
                }
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            List<string> prods = new List<string>();
            TreeNode[] treeNodes = treeViewProd.Nodes[0].Nodes.Cast<TreeNode>().Where(r => r.Checked == true).ToArray();
            if (treeNodes != null && treeNodes.Length > 0)
            {
                foreach (TreeNode node in treeNodes)
                {
                    prods.Add(node.Text);
                }
            }
            _prods = prods.ToArray();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void FrmSelectProd_Load(object sender, EventArgs e)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter("select distinct type from item where type is not null order by type", conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        string[] prods = dt.Rows.Cast<DataRow>().Select(x => x["type"].ToString()).ToArray();
                        //
                        // 在 TressView 中加入查詢結果
                        //
                        treeViewProd.Nodes.Add("Products");

                        foreach (string item in prods)
                        {
                            treeViewProd.Nodes[0].Nodes.Add(item);
                        }
                        treeViewProd.ExpandAll();
                    }
                }
                LoadItems(_prods);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

    }
}
