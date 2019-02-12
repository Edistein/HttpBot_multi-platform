using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Http.Core
{
    class Program
    {
        #region uaPool
        static string[] uaPool = {"",
            "Mozilla/5.0 (Linux; Android 8.0.0; LON-AL00 Build/HUAWEILON-AL00; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/70.0.3538.80 Mobile Safari/537.36 V1_AND_SQ_7.7.6_898_GM_D PA QQ/7.7.6.3680 NetType/WIFI WebP/0.4.1 Pixel/1440",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36",
            "Mozilla/5.0(compatible;MSIE9.0;WindowsNT6.1;Trident/5.0",
            "Mozilla/5.0(WindowsNT6.1;rv:2.0.1)Gecko/20100101Firefox/4.0.1",
            "Mozilla/4.0(compatible;MSIE7.0;WindowsNT5.1;TheWorld",
            "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_8; en-us) AppleWebKit/534.50 (KHTML, like Gecko) Version/5.1",
            "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; SE 2.X MetaSr 1.0; SE 2.X MetaSr 1.0; .NET CLR 2.0.50727; SE 2.X MetaSr 1.0)",
            "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; 360SE)",
            "Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_3_3 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko)",
            "Mozilla/5.0 (Linux; U; Android 2.3.7; en-us; Nexus One Build/FRF91) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0",
            "MQQBrowser/26 Mozilla/5.0 (Linux; U; Android 2.3.7; zh-cn; MB200 Build/GRJ22; CyanogenMod-7) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1" };
        #endregion
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            load:
            Console.WriteLine("输入预设名：(直接回车使用默认值)");
            string section = Console.ReadLine().ToString();
            if (section == "")
            {
                section = "Default";
            }
            bool px = false; bool threads = false; bool sleep = false; bool reply = true; bool strong = false; bool email = false; 
            string pxStatus = "关闭", threadsStatus = "关闭", sleepStatus = "关闭", strongStatus = "关闭", emailStatus = "关闭", replyStatus = "关闭";
            string path = ".\\console.ini";
            Console.WriteLine("尝试读取"+section+"的内容");
            #region 配置读取
            fuckxml lxm = new fuckxml();
            if (lxm.ReadIt(path, section, "proxy") == "true")
            {
                px = true;
                pxStatus = "开启";
            }
            else if ((lxm.ReadIt(path, section, "proxy") == "false"))
            {
                px = false;
                pxStatus = "关闭";
            }
            if (lxm.ReadIt(path, section, "threads") == "true")
            {
                threads = true;
                threadsStatus = " 开启";
            }
            else if ((lxm.ReadIt(path, section, "threads") == "false"))
            {
                threads = false;
                threadsStatus = "关闭";
            }
            if (lxm.ReadIt(path, section, "sleep") == "true")
            {
                sleep = true;
                sleepStatus = "开启";
            }
            else if ((lxm.ReadIt(path, section, "sleep") == "false"))
            {
                sleep = false;
                sleepStatus = "关闭";
            }
            if (lxm.ReadIt(path, section, "reply") == "true")
            {
                reply = true;
                replyStatus = "关闭";
            }
            else if ((lxm.ReadIt(path, section, "reply") == "false"))
            {
                reply = false;
                replyStatus = "打开";
            }
            if (lxm.ReadIt(path, section, "strong") == "true")
            {
                strong = true;
                strongStatus = "打开";
            }
            else if ((lxm.ReadIt(path, section, "strong") == "false"))
            {
                strong = false;
                strongStatus = "关闭";
            }
            if (lxm.ReadIt(path, section, "email") == "true")
            {
                email = true;
                emailStatus = "打开";
            }
            else if ((lxm.ReadIt(path, section, "email") == "false"))
            {
                email = false;
                emailStatus = "关闭";
            }
            string url = lxm.ReadIt(path, section, "url");
            string uname = lxm.ReadIt(path, section, "user");
            string pname = lxm.ReadIt(path, section, "password");
            string accept = lxm.ReadIt(path, section, "accept");
            string ct = lxm.ReadIt(path, section, "ct");
            string refer = lxm.ReadIt(path, section, "refer");
            string ck = null;
            string add = lxm.ReadIt(path, section, "addition");
            string num = lxm.ReadIt(path, section, "repeat");
            string encode = lxm.ReadIt(path, section, "encode");


            Console.WriteLine("当前配置    \n``````````````````````````````````");
            Console.WriteLine("编码集："+encode+"\nURL：" + url + "\n用户变量：" + uname + "\n密码变量：" + pname + "\n额外参数：" + add + "\n重复次数：" + num + "\n\n代理模式：" + pxStatus + "\n并发模式：" + threadsStatus + "\n睡眠模式：" + sleepStatus + "\n\n邮箱模式：" + emailStatus + "\n强密码模式：" + strongStatus + "\n\n渣男模式：" + replyStatus+"\n");
            if (url == "Error"|encode=="Error"| uname == "Error" | pname == "Error" | num == "Error")
            {
                Console.WriteLine("\n-------------------------------------------------\n未能成功加载指定内容，检查配置文件和预设名\n=================================================\n");
                goto load;
            }
            int repeat = Convert.ToInt32(num);
            WebProxy proxy = new WebProxy("127.0.0.1", 8118);
            Console.WriteLine("按任意键开始...");
            Console.ReadKey();
            #endregion
            #region 干活
            void DoEvents(int process)
            {
                Random ro = new Random();
                string u, p;
                if (email)
                {
                    u = GetUs(ro,1);
                }
                else
                {
                    u = GetUs(ro, 0);
                }

                if (strong) { p = GetPw(ro, 1); } else {p = GetPw(ro,0); }
                /*u = Uri.EscapeDataString(u);
                p = Uri.EscapeDataString(p);*/
                string ip = Convert.ToString(ro.Next(1, 255)) + "." + Convert.ToString(ro.Next(1, 255)) + "." + Convert.ToString(ro.Next(1, 255)) + "." + Convert.ToString(ro.Next(1, 255));
                string post = uname + "=" + u + "&" + pname + "=" + p + add;
                post = Uri.EscapeDataString(post);
                string ua = uaPool[ro.Next(0, 11)];
                try
                {
                    http pack = new http(url, post, ip, ua, accept, ct, refer, ck, proxy, px);
                    
                    
                    if (reply)
                    {
                        Console.WriteLine("请求ID:" + process + "\n" + u + "\n" + p + "\n" + post + "\n--------------------------------");
                        string re = pack.HttpPost(encode);
                        Console.WriteLine(re + "\n--------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("请求ID:" + process + "\n" + u + "\n " + p + "\n " + post + "\n--------------------------------");
                        pack.PostOnly(encode);
                    }
                    int t = ro.Next(90, 7000);
                    if (sleep)
                    {
                        for (int j = 0; j <= t; j++)
                        {
                            
                            Console.WriteLine("请求ID:"+process+"  已挂起" + j + "/" + t + "秒");
                            Thread.Sleep(1000);
                            ClearLine();
                        }
                    }

                }
                catch (SystemException e)
                {
                    Console.WriteLine("请求ID:" + process +"   -->   "+ e.Message);
                }
            }
            #endregion
            if (threads)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Parallel.For(0, repeat, i =>
                {
                    DoEvents(i);
                });
                sw.Stop();
                Console.WriteLine("Duration: " + sw.ElapsedMilliseconds + " ms");
            }
            else
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                for (int i = 1; i <= repeat; i++)
                {
                    DoEvents(i);
                }
                Console.WriteLine("Duration: " + sw.ElapsedMilliseconds+" ms");
            }

            Console.WriteLine("完成");
            Console.ReadKey();

        }
        static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
        static public string GetUs(Random ro, int cases)
        {
            string u="";
            
            if (cases == 0)
            {
                u = Convert.ToString(ro.Next(100000000, 999999999));
            }
            else if (cases == 1)      //邮箱模式
            {
                string[] str1 = { "b", "p", "m", "f", "d", "t", "n", "l", "g", "k", "h", "j", "y", "x", "z", "c", "s", "s", "c", "c", "c", "n", "l", "l", "l", "d", "h", "h" };
                string[] str2 = { "1314", "123", "abc", "abcabc", "123123", "666", "654", "111", "000", "aaa", "1234", "abcabc", "cao", "6rzh6" };
                string[] str3 = { "00", "01", "02", "03", "04", "05", "06", "07" };
                string[] str5 = { "@hotmail.com", "@163.com", "@outlook.com", "@126.com", "@yeah.com", "@sina.com", "sohu.com", "@163.com", "@163.com", "@126.com", "@126.com", "@sina.com" };
                string[] str6 = { "zhao", "qian", "sun", "li", "zhou", "wu", "zheng", "wang", "guan", "deng", "song", "wei", "hua", "liu", "lu", "yin", "liang", "jiang", "ruan", "hou", "lan", "zhang", "qin", "huang", "jin", "ling", "yun", "zi", "chan", "zi", "ming", "chen", "buo", "zhi", "hao", "zhao", "qing", "yu", "tong", "meng", "jun", "yang", "heng" };
                //43
                int mode = ro.Next(0, 7);
                if (mode == 0 | mode == 1 | mode == 2)
                {
                    u = ro.Next(100000000, 999999999) + "@qq.com";
                }
                if (mode == 3)
                {
                    u = str1[ro.Next(0, 27)] + str1[ro.Next(0, 27)] + str1[ro.Next(0, 27)] + ro.Next(0, 9999).ToString() + str5[ro.Next(0, 11)];
                }
                if (mode == 4)
                {
                    u = ro.Next(0, 9999).ToString() + str1[ro.Next(0, 27)] + str1[ro.Next(0, 27)] + str1[ro.Next(0, 27)] + str5[ro.Next(0, 11)];
                }
                if (mode == 5)
                {
                    u = str1[ro.Next(0, 27)] + str1[ro.Next(0, 27)] + str6[ro.Next(0, 42)] + ro.Next(0, 9999).ToString() + str5[ro.Next(0, 11)];
                }
                if (mode == 6)
                {
                    u = ro.Next(0, 9999).ToString() + str1[ro.Next(0, 27)] + str1[ro.Next(0, 27)] + str6[ro.Next(0, 42)] + str5[ro.Next(0, 11)];
                }
                return u;
            }
            return u;
        }
        static public string GetPw(Random ro, int cases)
        {
            int mode = ro.Next(0, 23);
            if (cases==1)  //Strong passwords
            {
                mode = ro.Next(16, 23);
            }
            string p = "33550336";
            string[] str1 = { "b", "p", "m", "f", "d", "t", "n", "l", "g", "k", "h", "j", "y", "x", "z", "c", "s", "s", "c", "c", "c", "n", "l", "l", "l", "d", "h", "h" };
            string[] str2 = { "1314", "123", "abc", "abcabc", "123123", "666", "654", "111", "000", "aaa", "1234", "abcabc", "cao", "6rzh6" };
            string[] str3 = { "00", "01", "02", "03", "04", "05", "06", "07" };
            string[] str4 = { "B", "P", "M", "F", "D", "T", "N", "L", "G", "K", "H", "J", "Q", "X", "L", "L", "L", "L", "H", "H", "H", "Z", "Z", "Z", "C", "C", "C", "S" };
            if (mode == 0)
            {
                int p1 = ro.Next(10000, 999999);
                p = Convert.ToString(p1) + str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)];
            }
            if (mode == 1)
            {
                int p1 = ro.Next(130, 152);
                int p2 = ro.Next(69999999, 899999999);
                p = Convert.ToString(p1) + Convert.ToString(p2) + str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)];
            }
            if (mode == 2)
            {
                int p1 = ro.Next(1, 13);
                int p2 = ro.Next(1, 31);
                p = "20" + str3[ro.Next(0, 7)] + Convert.ToString(p1) + Convert.ToString(p2) + str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)];
            }
            if (mode == 3)
            {
                int p1 = ro.Next(1, 13);
                int p2 = ro.Next(1, 31);
                p = "19" + Convert.ToString(ro.Next(71, 100)) + Convert.ToString(p1) + Convert.ToString(p2) + str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)];
            }
            if (mode == 4)
            {
                int p1 = ro.Next(10000, 999999);
                p = str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)] + Convert.ToString(p1);
            }
            if (mode == 5)
            {
                int p1 = ro.Next(130, 152);
                int p2 = ro.Next(69999999, 899999999);
                p = str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)] + Convert.ToString(p1) + Convert.ToString(p2);
            }
            if (mode == 6)
            {
                int p1 = ro.Next(1, 13);
                int p2 = ro.Next(1, 31);
                p = str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)] + "20" + str3[ro.Next(0, 7)] + Convert.ToString(p1) + Convert.ToString(p2);
            }
            if (mode == 7)
            {
                int p1 = ro.Next(1, 13);
                int p2 = ro.Next(1, 31);
                p = str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)] + "19" + Convert.ToString(ro.Next(71, 100)) + Convert.ToString(p1) + Convert.ToString(p2);
            }
            if (mode == 8)
            {
                int p1 = ro.Next(10000, 999999);
                p = Convert.ToString(p1) + str4[ro.Next(0, 16)] + str4[ro.Next(0, 16)] + str4[ro.Next(0, 16)];
            }
            if (mode == 9)
            {
                int p1 = ro.Next(130, 152);
                int p2 = ro.Next(699, 899);
                int p3 = ro.Next(39999, 89999);
                p = Convert.ToString(p1) + Convert.ToString(p2) + Convert.ToString(p3) + str4[ro.Next(0, 27)] + str4[ro.Next(0, 27)] + str4[ro.Next(0, 27)];
            }
            if (mode == 10)
            {
                int p1 = ro.Next(1, 13);
                int p2 = ro.Next(1, 31);
                p = "20" + str3[ro.Next(0, 7)] + Convert.ToString(p1) + Convert.ToString(p2) + str4[ro.Next(0, 27)] + str4[ro.Next(0, 27)] + str4[ro.Next(0, 27)];
            }
            if (mode == 11)
            {
                int p1 = ro.Next(130, 152);
                int p2 = ro.Next(69999999, 899999999);
                p = str4[ro.Next(0, 27)] + str4[ro.Next(0, 27)] + str4[ro.Next(0, 27)] + Convert.ToString(p1) + Convert.ToString(p2);
            }
            if (mode == 12)
            {
                int p1 = ro.Next(1, 13);
                int p2 = ro.Next(1, 31);
                p = str4[ro.Next(0, 27)] + str4[ro.Next(0, 27)] + str4[ro.Next(0, 27)] + "20" + str3[ro.Next(0, 7)] + Convert.ToString(p1) + Convert.ToString(p2);
            }
            if (mode == 13)
            {
                int p1 = ro.Next(1, 13);
                int p2 = ro.Next(1, 31);
                p = str4[ro.Next(0, 27)] + str4[ro.Next(0, 27)] + str4[ro.Next(0, 27)] + "19" + Convert.ToString(ro.Next(71, 100)) + Convert.ToString(p1) + Convert.ToString(p2);
            }
            if (mode == 14)
            {
                p = str4[ro.Next(0, 27)] + str4[ro.Next(0, 27)] + str4[ro.Next(0, 27)] + str2[ro.Next(13)] + str2[ro.Next(13)] + str4[ro.Next(0, 27)] + str4[ro.Next(0, 27)] + str4[ro.Next(0, 27)];
            }
            if (mode == 15)
            {
                p = str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)] + str2[ro.Next(13)] + str2[ro.Next(13)] + str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)];
            }
            if (mode == 16)
            {
                int p1 = ro.Next(130, 152);
                int p2 = ro.Next(69999999, 899999999);
                p = Convert.ToString(p1) + Convert.ToString(p2) + str4[ro.Next(0, 16)] + str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)];
            }
            if (mode == 17)
            {
                int p1 = ro.Next(1, 13);
                int p2 = ro.Next(1, 31);
                p = "20" + str3[ro.Next(0, 7)] + Convert.ToString(p1) + Convert.ToString(p2) + str4[ro.Next(0, 16)] + str1[ro.Next(0, 16)] + str1[ro.Next(0, 16)];
            }
            if (mode == 18)
            {
                int p1 = ro.Next(1, 13);
                int p2 = ro.Next(1, 31);
                p = str4[ro.Next(0, 27)] + str1[ro.Next(0, 27)] + str1[ro.Next(0, 27)] + "20" + str3[ro.Next(0, 7)] + Convert.ToString(p1) + Convert.ToString(p2);
            }
            if (mode == 19)
            {
                int p1 = ro.Next(1, 13);
                int p2 = ro.Next(1, 31);
                p = str4[ro.Next(0, 27)] + str1[ro.Next(0, 27)] + str1[ro.Next(0, 27)] + "19" + Convert.ToString(ro.Next(71, 100)) + Convert.ToString(p1) + Convert.ToString(p2);
            }
            if (mode == 20)
            {
                p = str4[ro.Next(0, 27)] + str1[ro.Next(0, 27)] + str1[ro.Next(0, 27)] + str2[ro.Next(13)] + str2[ro.Next(13)] + str4[ro.Next(0, 27)] + str1[ro.Next(0, 27)] + str1[ro.Next(0, 27)];
            }
            if (mode == 21)
            {
                int p1 = ro.Next(1, 13);
                int p2 = ro.Next(1, 31);
                p = str4[ro.Next(0, 27)] + str1[ro.Next(0, 27)] + str1[ro.Next(0, 27)] + "19" + Convert.ToString(ro.Next(71, 100)) + Convert.ToString(p1) + Convert.ToString(p2);
            }
            if (mode == 22)
            {
                int p1 = ro.Next(1, 13);
                int p2 = ro.Next(1, 31);
                p = str4[ro.Next(0, 27)] + str1[ro.Next(0, 27)] + str1[ro.Next(0, 27)] + "20" + str3[ro.Next(0, 7)] + Convert.ToString(p1) + Convert.ToString(p2);
            }
            if (mode == 23)
            {
                p = ro.Next(110100, 660000).ToString() + "20" + str3[ro.Next(0, 7)] + ro.Next(1, 13).ToString().PadLeft(2, '0') + ro.Next(1, 31).ToString().PadLeft(2, '0') + ro.Next(1000, 9999).ToString();
            }
            if (mode == 24)
            {
                p = ro.Next(110100, 660000).ToString() + "19" + ro.Next(71, 100).ToString() + ro.Next(1, 13).ToString().PadLeft(2, '0') + ro.Next(1, 31).ToString().PadLeft(2, '0') + ro.Next(1000, 9999).ToString();
            }
            return p;
        }
    }
    public class http
    {
        string Url, postDataStr, ip, ua, accept, ct, refer, ck;
        WebProxy proxy;
        bool px;
        #region 构造函数
        public http(string Url, string postDataStr, string ip, string ua, string accept, string ct, string refer, string ck,  WebProxy proxy, bool px)
        {
            this.Url = Url;
            this.postDataStr = postDataStr;
            this.ip = ip;
            this.ua = ua;
            this.accept = accept;
            this.ct = ct;
            this.refer = refer;
            this.ck = ck;
            this.proxy = proxy;
            this.px = px;
        }
        #endregion
        public string HttpPost(string encode)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
           /* Cookie cookie = new Cookie();
            cookie.Name = this.ckName;
            cookie.Value = this.ck;
            cookie.Domain = this.Url.Substring(4);
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(cookie);*/
            request.Method = "POST";
            request.KeepAlive = true;
            request.ContentLength = postDataStr.Length;
            request.Accept = this.accept;
            request.UserAgent = this.ua;
            request.ContentType = this.ct;
            request.Referer = refer;
            request.Headers.Add("X_FORWARDED_FOR", this.ip);
            request.Headers.Add("CLIENT_IP", this.ip);
            if (this.px)
            {
                request.Proxy = proxy;
            }
            request.Referer = this.refer;
            //request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding(encode));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        public void PostOnly(string encode)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            /*Cookie cookie = new Cookie();
            cookie.Name = this.ckName;
            cookie.Value = this.ck;
            cookie.Domain = this.Url.Substring(4);
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(cookie);*/
            request.Method = "POST";
            request.KeepAlive = true;
            request.ContentLength = postDataStr.Length;
            request.Accept = this.accept;
            request.UserAgent = this.ua;
            request.ContentType = this.ct;
            request.Referer = refer;
            request.Headers.Add("X_FORWARDED_FOR", this.ip);
            request.Headers.Add("CLIENT_IP", this.ip);
            if (this.px)
            {
                request.Proxy = proxy;
            }
            request.Referer = this.refer;
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            Stream myRequestStream = request.GetRequestStream();
           StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding(encode));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();
        }
    }
    public class fuckxml
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key,
                                                             string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key,
                                                           string def, StringBuilder retVal,
                                                           int size, string filePath);

        public string ReadIt(string path, string section, string key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "Error", temp, 255, path);
            int jj = temp.Length;
            return temp.ToString();
        }
    }
}
