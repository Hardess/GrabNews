using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 抓取信息
{
    public partial class frmNewEdit : Form
    {
        /// <summary>
        /// 地址列
        /// </summary>
        private int indexUrl = 2;
        /// <summary>
        /// 标题列
        /// </summary>
        private int indexTitle = 1;
        /// <summary>
        /// 内容列
        /// </summary>
        private int indexContent = 3;
        /// <summary>
        /// 来源列
        /// </summary>
        private int indexFrom = 8;
        /// <summary>
        /// 发布时间列
        /// </summary>
        private int indexDate = 9;
        /// <summary>
        /// 关键字列
        /// </summary>
        private int indexKey = 5;
        /// <summary>
        /// 概要列
        /// </summary>
        private int indexDes = 6;

        private int indexAuthor = 7;
        public DataGridView dgv;
        public frmNewEdit(ref DataGridView _dgv)
        {
            dgv = _dgv;
            InitializeComponent();
        }

        private void frmNewEdit_Load(object sender, EventArgs e)
        {
            showData();
        }

        private void showData()
        {
            try
            {
                txtTitle.Text = dgv.SelectedRows[0].Cells[indexTitle].Value.ToString();
                txtCon.DocumentText = dgv.SelectedRows[0].Cells[indexContent].Value.ToString();
                if (dgv.SelectedRows[0].Cells[indexKey].Value != null)
                {
                    txtKeys.Text = dgv.SelectedRows[0].Cells[indexKey].Value.ToString();
                }
                txtDes.Text = dgv.SelectedRows[0].Cells[indexDes].Value.ToString();
                linkNew.Text = dgv.SelectedRows[0].Cells[indexUrl].Value.ToString();
                txtFrom.Text = dgv.SelectedRows[0].Cells[indexFrom].Value.ToString();
                txtDate.Text = dgv.SelectedRows[0].Cells[indexDate].Value.ToString();
                txtAuthor.Text = dgv.SelectedRows[0].Cells[indexAuthor].Value.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows[0].Index > 0)
            {
                dgv.Rows[dgv.SelectedRows[0].Index - 1].Selected = true;
                showData();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows[0].Index < dgv.Rows.Count - 1)
            {
                dgv.Rows[dgv.SelectedRows[0].Index + 1].Selected = true;
                showData();
            }
        }

        private void frmNewEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.txtCon.Dispose();//外部控件异常特殊处理
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dgv.SelectedRows[0].Cells[indexTitle].Value = txtTitle.Text;
            dgv.SelectedRows[0].Cells[indexContent].Value = txtCon.BodyHtml;
            dgv.SelectedRows[0].Cells[indexKey].Value = txtKeys.Text;
            dgv.SelectedRows[0].Cells[indexDes].Value = txtDes.Text;
            dgv.SelectedRows[0].Cells[indexFrom].Value = txtFrom.Text;
            dgv.SelectedRows[0].Cells[indexDate].Value = txtDate.Text;
            dgv.SelectedRows[0].Cells[indexAuthor].Value = txtAuthor.Text;
        }
    }
}
