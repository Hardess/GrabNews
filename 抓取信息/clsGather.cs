using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace 抓取信息
{
    public class clsGather
    {
        /// <summary>
        /// 消息发送委托
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ep"></param>
        public delegate void DataArriveHandler(NewPage message, int rowindex);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event DataArriveHandler onDataArrived;

        public static string GetKeyValue(string Key, string MessageStr)
        {
            //获取指定Key后的=Value
            string pattern = Key + "[ ]*=[ ]*\"[^\"]+\"";
            string strReturn = GetRegexValue(pattern, MessageStr);
            if (!string.IsNullOrEmpty(strReturn))
            {
                //不返回Key只返回值，便于处理
                return strReturn.Substring(strReturn.IndexOf("=") + 1).Replace("\"", string.Empty);
            }
            else
            {
                pattern = Key + "[ ]*=[ ]*[^\"]+[ ]";
                strReturn = GetRegexValue(pattern, MessageStr);
                if (!string.IsNullOrEmpty(strReturn))
                {
                    return strReturn.Substring(strReturn.IndexOf("=") + 1).Replace(" ", string.Empty);
                }
                pattern = "<" + Key + ">.*</" + Key + ">";
                strReturn = GetRegexValue(pattern, MessageStr);
                if (!string.IsNullOrEmpty(strReturn))
                {
                    return strReturn.Replace("<" + Key + ">", string.Empty).Replace("</" + Key + ">", string.Empty).Replace("<" + Key.ToUpper() + ">", string.Empty).Replace("</" + Key.ToUpper() + ">", string.Empty);
                }
                else
                {
                    Key = string.Empty;
                }
            }
            return Key;
        }

        /// <summary>
        /// 正则表达式获取值
        /// </summary>
        /// <param name="patten">正则</param>
        /// <param name="MessageStr">字符串</param>
        /// <returns>结果</returns>
        public static string GetRegexValue(string pattern, string MessageStr)
        {
            Match match = Regex.Match(MessageStr, pattern);
            if (match.Success)
            {
                return match.Value;
            }
            else
            {
                return string.Empty;
            }
        }
        public static string GetRegexValueRightToLeft(string pattern, string MessageStr)
        {
            Match match = Regex.Match(MessageStr, pattern, RegexOptions.RightToLeft);
            if (match.Success)
            {
                return match.Value;
            }
            else
            {
                return string.Empty;
            }
        }
        public void StartThread(List<string> list)
        {
            Thread TOne = new Thread(StarThread);
            Thread TTwo = new Thread(StarThread);
            TOne.SetApartmentState(ApartmentState.STA);
            TOne.Start(list);
            TTwo.SetApartmentState(ApartmentState.STA);
            TTwo.Start(list);
        }
        int index = 0;
        public void StarThread(object obj)
        {
            index++;
            List<string> list = (List<string>)obj;
            lock (this)
            {
                if (index - 1 > list.Count)
                { return; }
                NewPage pag = new NewPage();
                WebBrowser web = new WebBrowser();
                web.Navigate("about:blank");
                //屏蔽掉JS报错，解决线程卡死。
                web.ScriptErrorsSuppressed = true;
                string htmlcode = GetHtmlSource(list[index - 2]);
                pag.URL = list[index - 2];
                try
                {
                    web.Document.Write(htmlcode);
                }
                catch (Exception)
                {
                    throw;
                }
                //执行分析操作 
                StringBuilder sb = new StringBuilder();
                //页面信息
                HtmlElementCollection elemColl2 = web.Document.GetElementsByTagName("meta");
                string pubDate = string.Empty;
                foreach (HtmlElement item in elemColl2)
                {
                    //取得关键字
                    if (item.Name.ToLower() == "keywords")
                    {

                        pag.KeyWord = GetRegexValue(@"content[ ]*=[ ]*[^>]*", item.OuterHtml).Replace("content", "").Replace("=", "").Replace("\"", "").Trim();
                    }
                    else if (item.Name.ToLower() == "description")
                    {
                        //取得简介
                        pag.Description = GetRegexValue(@"content[ ]*=[ ]*[^>]*", item.OuterHtml).Replace("content", "").Replace("=", "").Replace("\"", "").Trim();
                    }
                    else if (item.Name.ToLower() == "publishdate")
                    {
                        //取的发布时间
                        pubDate = GetRegexValue(@"content[ ]*=[ ]*[^>]*", item.OuterHtml).Replace("content", "").Replace("=", "").Replace("\"", "").Trim();
                    }
                }
                //取内容的地方，可根据需要配置网站
                #region 取内容 农民日报规则
                string Text = web.DocumentText;
                string title = web.Document.Title.Remove(web.Document.Title.Length - 5);
                pag.Title = title;
                web.Document.OpenNew(false);
                sb.Append(GetRegexValue(@"<!--标题-->[\s\S]*<!--分页-->", Text));
                string content = GetRegexRemove(@"class[ ]*=[ ]*[^>]*", sb.ToString(), "");
                string Content = GetRegexRemove(@"style[ ]*=[ ]*[^>]*", content, "");
                string End = GetRegexRemove(@"href[ ]*=[ ]*[^>]*", Content, "");
                string Con = GetRegexValue(@"<!--正文-->[\s\S]*<!--分页-->", End);
                pag.Content = Con;
                web.Document.Write(pag.Content);
                //网站来源 农民日报规则
                string Date = GetRegexValue(@"<p >[ ]*[ ]*[^<]*", End).Replace("<div >", "").Replace("</div>", "");
                pag.PublicDate = Date.Substring(4);
                string Author = GetRegexValue(@"</span>[ ]*[ ]*[^<]*", End).Replace("<div >", "").Replace("</div>", "");
                pag.Author = Author.Substring(10);
                string From = GetRegexValue(@"来源：[ ]*[ ]*[^<]*", End).Replace("<div >", "").Replace("</div>", "");
                pag.FormPage = From.Substring(3);
                #endregion
                HtmlElementCollection elemColl = web.Document.GetElementsByTagName("img");
                Tools ts = new Tools();
                foreach (HtmlElement item in elemColl)
                {
                    string src = ts.SearchImgList(item, @"D:\新闻\" + title, pag);
                    string img = src.Substring(0, src.LastIndexOf('/'));
                    string Img = src.Substring(src.LastIndexOf('/'), src.Length - src.LastIndexOf('/'));
                    pag.Content = pag.Content.Replace(src, "/FileImage/GrabNews/News/" + DateTime.Now.ToString("yyyyMMdd") + Img);
                }
                if (index - 1 > list.Count)
                { return; }
                if (!string.IsNullOrEmpty(pag.KeyWord) && !string.IsNullOrEmpty(pag.Description))
                {
                    onDataArrived(pag, index - 1);
                }
                StarThread(obj);
            }
        }
        /// <summary>
        /// 正则表达式替换指定内容
        /// </summary>
        /// <param name="pattern">规则</param>
        /// <param name="MessageStr">消息字符串</param>
        /// <param name="replaceStr">替换字符串</param>
        /// <returns>返回结果</returns>
        public static string GetRegexRemove(string pattern, string MessageStr, string replaceStr)
        {
            if (MessageStr == "")
            {
                return "";
            }
            return Regex.Replace(MessageStr, pattern, "", RegexOptions.IgnoreCase);
        }
        private string GetHtmlSource(string url)
        {
            string text = "";
            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                wc.Encoding = Encoding.UTF8;
                text = wc.DownloadString(url);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return text;
        }
    }
    public class NewPage
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title = string.Empty;
        /// <summary>
        /// 来源
        /// </summary>
        public string FormPage = string.Empty;
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord = string.Empty;
        /// <summary>
        /// 发布时间
        /// </summary>
        public string PublicDate = string.Empty;
        /// <summary>
        /// 介绍
        /// </summary>
        public string Description = string.Empty;
        /// <summary>
        /// 内容
        /// </summary>
        public string Content = string.Empty;
        /// <summary>
        /// 网址
        /// </summary>
        public string URL = string.Empty;
        /// <summary>
        /// 作者
        /// </summary>
        public string Author = string.Empty;
        /// <summary>
        /// 图片
        /// </summary>
        public string Image = string.Empty;
    }
}
