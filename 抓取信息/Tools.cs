using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace 抓取信息
{
   public class Tools
    {
        //#region http请求
        ///// <summary>
        ///// 获得post请求后响应的数据
        ///// </summary>
        ///// <param name="postUrl">请求地址</param>
        ///// <param name="referUrl">请求引用地址</param>
        /////          /// <param name="data">请求带的数据</param>
        ///// <returns>响应内容</returns>
        //public string PostLogin(string postUrl, string data)
        //{
        //    string result = "";
        //    try
        //    {
        //        //命名空间System.Net下的HttpWebRequest类
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(postUrl);
        //        //参照浏览器的请求报文 封装需要的参数 这里参照ie9
        //        //浏览器可接受的MIME类型
        //        request.Accept = "text/plain, */*; q=0.01";
        //        //浏览器类型，如果Servlet返回的内容与浏览器类型有关则该值非常有用
        //        request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET4.0C; .NET4.0E)";
        //        request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
        //        //请求方式
        //        request.Method = "POST";
        //        //是否保持常连接
        //        request.KeepAlive = false;
        //        request.Headers.Add("Accept-Encoding", "gzip, deflate");
        //        //表示请求消息正文的长度
        //        request.ContentLength = data.Length;
        //        Stream postStream = request.GetRequestStream();
        //        byte[] postData = Encoding.UTF8.GetBytes(data);
        //        //将传输的数据，请求正文写入请求流
        //        postStream.Write(postData, 0, postData.Length);
        //        postStream.Dispose();
        //        //响应
        //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //        //判断响应的信息是否为压缩信息 若为压缩信息解压后返回
        //        if (response.ContentEncoding == "gzip")
        //        {
        //            MemoryStream ms = new MemoryStream();
        //            GZipStream zip = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
        //            byte[] buffer = new byte[1024];
        //            int l = zip.Read(buffer, 0, buffer.Length);
        //            while (l > 0)
        //            {
        //                ms.Write(buffer, 0, l);
        //                l = zip.Read(buffer, 0, buffer.Length);
        //            }
        //            ms.Dispose();
        //            zip.Dispose();
        //            result = Encoding.UTF8.GetString(ms.ToArray());
        //        }
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //#endregion

        #region 保存图片到本地
        //直接存图片
        public string SearchImgList(HtmlElement elem, string savePath,NewPage pag)
       {
           string sImgUrl;
           //取得所有图片地址
           sImgUrl = elem.GetAttribute("src");
            try
            {
                //图片链接地址
                string rtn = string.Empty;
                
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
               
                if (sImgUrl != "")
                {
                    rtn += sImgUrl.Replace(sImgUrl.Substring(0, sImgUrl.LastIndexOf('/')), savePath);
                    //调用保存远程图片函数
                    SaveImageFromWeb(sImgUrl, savePath);
                }
                Stream stream = new FileStream(rtn, FileMode.Open);
                BinaryReader br = new BinaryReader(stream);
                byte[] data = br.ReadBytes((int)stream.Length);
                GrabNews.GrabNewsSoapClient gsc = new GrabNews.GrabNewsSoapClient();
                string fileName= "/News/" + DateTime.Now.ToString("yyyyMMdd") +rtn.Substring(rtn.LastIndexOf('/'),(rtn.Length-rtn.LastIndexOf('/')));
                pag.Image = fileName; 
                gsc.TOImage(data, fileName);
                stream.Close();
                return sImgUrl;
            }
            catch (Exception)
            {
                return sImgUrl;
            }
           
        }
        public int SaveImageFromWeb(string imgUrl, string path)
        {
            string imgName = imgUrl.ToString().Substring(imgUrl.ToString().LastIndexOf("/") + 1);
            path = path + "//" + imgName;
            string defaultType = ".jpg";
            string[] imgTypes = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            string imgType = imgUrl.ToString().Substring(imgUrl.ToString().LastIndexOf("."));
            foreach (string it in imgTypes)
            {
                if (imgType.ToLower().Equals(it))
                    break;
                if (it.Equals(".bmp"))
                    imgType = defaultType;
            }
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imgUrl);
                request.UserAgent = "Mozilla/6.0 (MSIE 6.0; Windows NT 5.1; Natas.Robot)";
                request.Timeout = 10000;
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                if (response.ContentType.ToLower().StartsWith("image/"))
                {
                    byte[] arrayByte = new byte[1024];
                    int imgLong = (int)response.ContentLength;
                    int l = 0;
                    FileStream fso = new FileStream(path, FileMode.Create);
                    while (l < imgLong)
                    {
                        int i = stream.Read(arrayByte, 0, 1024);
                        fso.Write(arrayByte, 0, i);
                        l += i;
                    }
                    fso.Close();
                    stream.Close();
                    response.Close();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (WebException)
            {
                return 0;
            }
            catch (UriFormatException)
            {
                return 0;
            }
        }
        #endregion

        #region DataTableToJson
        public string DataTableToJson(System.Data.DataTable dt)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]");
            return Json.ToString();
        }   
        #endregion
    }
}
