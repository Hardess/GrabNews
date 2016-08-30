using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text;
using System.IO.Compression;

namespace 抓取信息
{
    public partial class Form1 : Form
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
        /// 处理结果列
        /// </summary>
        private int indexRtn = 4;
        /// <summary>
        /// 关键字列
        /// </summary>
        private int indexKey = 5;
        /// <summary>
        /// 概要列
        /// </summary>
        private int indexDes = 6;
        private int indexAuthor = 7;
        private int indexImage = 10;
        public Form1()
        {
            InitializeComponent();
            this.webBrowser1.ScriptErrorsSuppressed = true;
        }
        static List<string> list = new List<string>();
        clsGather cls = new clsGather();
        private void btnBegin_Click(object sender, EventArgs e)
        {
            N = 0;
            string content = "";
            string Url = "";

            string Text = comboBox1.SelectedItem.ToString();
            if (Text == "农民日报")
            {
                Url = "http://ppny.cnguonong.com/news/?cid=50";

            }
            webBrowser1.Url = new Uri(Url);
            this.webBrowser1.Navigate(Url);
            this.webBrowser1.ScriptErrorsSuppressed = true;
            content = webBrowser1.DocumentText;
            //WebBrowser取源代码中的标签
            if (webBrowser1.Document == null)
            { return; }
            HtmlElementCollection hec = this.webBrowser1.Document.GetElementsByTagName("a");
            foreach (HtmlElement elem in hec)
            {
                string contentStr = elem.GetAttribute("href");
                //排除条件非http开始的
                //elem.InnerText为空的
                if (contentStr.IndexOf("http://") < 0 || string.IsNullOrEmpty(elem.InnerText))
                {
                    //非URL
                    continue;
                }
                string url = "";
                #region 农民日报规则
                if (Text == "农民日报")
                {
                    url = clsGather.GetRegexValue(@"http://ppny.cnguonong.com/newshtml[^\s]*", contentStr);
                }
                #endregion
                if (!string.IsNullOrEmpty(url))
                {
                    list.Add(url);
                }
            }
            dgvNew.Rows.Add(list.Count);
            cls.StartThread(list);
            cls.onDataArrived += new clsGather.DataArriveHandler(onDataArrived);
        }
        /// <summary>
        /// 展示数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ep"></param>
        private void onDataArrived(NewPage s, int rowindex)
        {
            try
            {
                int index = rowindex - 1;
                dgvNew.Rows[index].Cells[indexTitle].Value = s.Title;
                if (string.IsNullOrEmpty(s.URL))
                {
                    dgvNew.Rows[index].Cells[indexRtn].Value = "空数据";
                }
                else
                {
                    dgvNew.Rows[index].Cells[indexRtn].Value = "采集完成";
                }
                dgvNew.Rows[index].Cells[indexContent].Value = s.Content;
                dgvNew.Rows[index].Cells[indexKey].Value = s.KeyWord;
                dgvNew.Rows[index].Cells[indexDes].Value = s.Description;
                dgvNew.Rows[index].Cells[indexUrl].Value = s.URL;
                dgvNew.Rows[index].Cells[indexDate].Value = s.PublicDate;
                dgvNew.Rows[index].Cells[indexFrom].Value = s.FormPage;
                dgvNew.Rows[index].Cells[indexAuthor].Value = s.Author;
            }
            catch (Exception)
            {
                MessageBox.Show("请等待列表加载完成，在进行操作！");
            }
            
        }
        static int N = 0;
        static DataSet ds;
        private void btnSave_Click(object sender, EventArgs e)
        {
            N = 1;
            SaveFileDialog sfd = new SaveFileDialog();
            ds = new DataSet();
            ds.Tables.Add("News");
            //给表中增加列
            for (int i = 0; i < dgvNew.ColumnCount; i++)
            {
                ds.Tables["News"].Columns.Add();
            }
            //给表中增加选中行数据
            int k = 0;
            for (int i = 0; i < dgvNew.Rows.Count; i++)
            {
                if (dgvNew.Rows[i].Cells[0].Value != null)
                {
                    if (dgvNew.Rows[i].Cells[0].Value.ToString() == "1")
                    {
                        ds.Tables["News"].Rows.Add();
                        for (int j = 0; j < dgvNew.ColumnCount; j++)
                        {
                            ds.Tables["News"].Rows[k][j] = dgvNew.Rows[i].Cells[j].Value;
                        }
                        k++;
                    }
                }
            }
            if (ds.Tables["News"].Rows.Count <= 0)
            {
                MessageBox.Show("至少勾选一条新闻！");
            }
            else
            {
                sfd.Filter = ".xml|*.xml";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ds.WriteXml(sfd.FileName, XmlWriteMode.WriteSchema);
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            N = 0;
            dgvNew.Rows.Clear();
        }

        private void dgvNew_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmNewEdit frmn = new frmNewEdit(ref dgvNew);
            frmn.ShowDialog();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            if (N == 0)
            {
                MessageBox.Show("上传之前必须先保存！");
                return;
            }
            GrabNews.GrabNewsSoapClient gsc = new GrabNews.GrabNewsSoapClient();
            string result = gsc.News(ds.Tables["News"]);
            if (result == "1")
            {
                MessageBox.Show("上传成功！");
            }
            else
            {
                MessageBox.Show("上传失败！");
            }
            N = 0;
        }
    }
}
