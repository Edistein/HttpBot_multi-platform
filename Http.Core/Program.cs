using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            Console.WriteLine("== Welcome to VictimSimulator v1.1 ==");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        load:
            Console.WriteLine("Input the preset file name:  ");
            string name = Console.ReadLine().ToString();
            if (name == "")
            {
                name = "Default";
            }
            string path = ".\\"+name+".json";
            Console.WriteLine("Reading setting from "+name+".json...");
            #region 配置读取
            string json = "";
            try
            {
                StreamReader srReadFile = new StreamReader(path);
                while (!srReadFile.EndOfStream)
                 {
                    json += srReadFile.ReadLine();
                 }
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
                goto load;
            }
            Console.WriteLine("File content: "+json);
            preset p0;
            p0 = JsonConvert.DeserializeObject<preset>(json);
            try
            {
                
            }
            catch 
            {
                Console.WriteLine("File Error. ");
                goto load;
            }
            Console.WriteLine("Successfully loaded.\n---------------------");
            Console.WriteLine("Current Setting    \n---------------------");
            Console.WriteLine("Charset= " + p0.encode + "\nURL：" +p0. url + "\nUserV= " +p0. user + "\nPasswordV= " + p0.password + "\nAdditionalV= " + p0.addition + "\nRepeat= " + p0.repeat +  "\nThreads= " + p0.threads + "\nSleepMode= " + p0.sleep + "\n*\nEmailMode= " + p0.email + "\nStrongPwMode= " + p0.strong + "\n*\nNoreply= " + p0.noreply + "\n");
            int repeat = Convert.ToInt32(p0.repeat);
            bool threads = false;bool noreply = false;bool strong = false; bool email = false;bool sleep = false;
            if (p0.threads == "true")
            {
                threads = true;
            }
            if (p0.noreply == "true")
            {
                noreply = true;
            }
            if (p0.strong == "true")
            {
                strong = true;
            }
            if (p0.email == "true")
            {
                email = true;
            }
            if (p0.sleep == "true")
            {
                sleep = true;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
#endregion
            #region 干活
            void DoEvents(int process)
            {
                Random ro = new Random(GetRandomSeed());
                string u, p;
                if (email)
                {
                    u = GetUs(ro, 1);
                }
                else
                {
                    u = GetUs(ro, 0);
                }

                if (strong) { p = GetPw(ro, 1); } else { p = GetPw(ro, 0); }
                u = Uri.EscapeDataString(u);
                p = Uri.EscapeDataString(p);
                string ip = Convert.ToString(ro.Next(1, 255)) + "." + Convert.ToString(ro.Next(1, 255)) + "." + Convert.ToString(ro.Next(1, 255)) + "." + Convert.ToString(ro.Next(1, 255));
                string post = p0.user + "=" + u + "&" + p0.password + "=" + p + p0.addition;
                string ua = uaPool[ro.Next(0, 11)];
                try
                {
                    http pack = new http(p0, post, ip, ua);


                    if (noreply==false)
                    {
                        Console.WriteLine("ID:" + process + "\n" + u + "\n" + p + "\n" + post + "\n--------------------------------");
                        string re = pack.HttpPost(p0.encode, noreply);
                        Console.WriteLine(re + "\n--------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("ID:" + process + "\n" + u + "\n " + p + "\n " + post + "\n--------------------------------");
                        pack.PostOnly(p0.encode, noreply);
                    }
                    int t = ro.Next(90, 7000);
                    if (sleep)
                    {
                        for (int j = 0; j <= t; j++)
                        {

                            Console.WriteLine("ID:" + process + "  Sleeping...   " + j + "/" + t + " s");
                            Thread.Sleep(1000);
                            ClearLine();
                        }
                    }

                }
                catch (SystemException e)
                {
                    Console.WriteLine("ID:" + process + "   -->   " + e.Message);
                }
            }
            #endregion
            if (threads)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Parallel.For(1, repeat, i =>
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
                Console.WriteLine("Duration: " + sw.ElapsedMilliseconds + " ms");
            }

            Console.WriteLine("Done. ");
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
            string u = "";

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
            if (cases == 1)  //Strong passwords
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
        string Url, postDataStr, ip, ua, accept, ct, refer;
        #region 构造函数
        public http(preset p, string postDataStr, string ip, string ua)
        {
            this.Url = p.url;
            this.postDataStr = postDataStr;
            this.ip = ip;
            this.ua = ua;
            this.accept = p.accept;
            this.ct = p.ct;
            this.refer =p. refer;
        }
        #endregion
        public string HttpPost(string encode, bool noreply)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.CookieContainer = new CookieContainer();
            request.Method = "POST";
            request.KeepAlive = true;
            if (noreply == true)
            {
                request.KeepAlive = false;
            }
            request.ContentLength = postDataStr.Length;
            request.Accept = this.accept;
            request.UserAgent = this.ua;
            request.ContentType = this.ct;
            request.Referer = refer;
            request.Headers.Add("X_FORWARDED_FOR", this.ip);
            request.Headers.Add("CLIENT_IP", this.ip);
            request.Referer = this.refer;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding(encode));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(encode));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        public void PostOnly(string encode, bool noreply)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.KeepAlive = true;
            if (noreply == true)
            {
                request.KeepAlive = false;
            }
            request.ContentLength = postDataStr.Length;
            request.Accept = this.accept;
            request.UserAgent = this.ua;
            request.ContentType = this.ct;
            request.Referer = refer;
            request.Headers.Add("X_FORWARDED_FOR", this.ip);
            request.Headers.Add("CLIENT_IP", this.ip);
            request.Referer = this.refer;
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding(encode));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();
        }
    }
    public class preset
    {
        public string encode, url, user, password, accept, ct, refer, addition, repeat, email, strong, threads, noreply,sleep;
        public preset(string encode, string url, string user, string password, string accept, string ct, string refer, string ck, string addition, string repeat, string email, string threads, string noreply,string sleep, string strong)
        {
            this.encode = encode;
            this.url = url;
            this.user = user;
            this.password = password;
            this.accept = accept;
            this.ct = ct;
            this.refer = refer;
            this.addition = addition;
            this.repeat = repeat;
            this.email = email;
            this.threads = threads;
            this.noreply = noreply;
            this.sleep = sleep;
            this.strong = strong;
        }
    }
}
