using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace http2
{
    public partial class Form1 : Form
    {

        bool stop = false;
        bool rndua = false;
        public static void Delay(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }
        static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        public string GetPw(Random ro)
        {

            int mode = ro.Next(0, 16);
            if (checkBox4.Checked == true)  //Strong passwords
            {
                mode = ro.Next(16, 23);
            }
            if (checkBox7.Checked == true)  //ID numbers
            {
                mode = ro.Next(23, 25);
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

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            stop = false;
            textBox1.Enabled = false;
            textBox9.Enabled = false;
            textBox10.Enabled = false;
            button1.Enabled = false;
            bool px = false;
            Random ro = new Random(GetRandomSeed());
            string ip = "";
            string p = " 33550336";
            string url = "http://" + textBox7.Text;
            string ua = "";
            string accept = textBox5.Text;
            string ct = textBox6.Text;
            string refer = textBox8.Text;
            string ck = textBox12.Text;
            string uname = textBox9.Text;
            string pname = textBox10.Text;
            progressBar1.Value = 0;
            progressBar1.Maximum = Convert.ToInt32(textBox1.Text);
            progressBar1.Value = 0;
            for (int i = 1; i <= Convert.ToInt32(textBox1.Text); i++)
            {
                #region 模式传递
                if (checkBox1.Checked == true)      //劣质随机IP
                {
                    textBox3.Enabled = false;
                    ip = Convert.ToString(ro.Next(1, 255)) + "." + Convert.ToString(ro.Next(1, 255)) + "." + Convert.ToString(ro.Next(1, 255)) + "." + Convert.ToString(ro.Next(1, 255));
                    label10.Text = ip;
                }
                else
                {
                    textBox3.Enabled = true;
                    ip = textBox3.Text;
                }
                if (checkBox2.Checked == true)      //随机UA
                {
                    rndua = true;
                }
                else
                {
                    rndua = false;
                }
                if (checkBox8.Checked == true)//代理模式
                {
                    px = true;
                }
                else
                {
                    px = false;
                }
                #endregion
                if (stop == true)
                {
                    progressBar1.Value = 0;
                    label8.Text = "0%";
                    break;
                }
                p = GetPw(ro);
                
                string u = Convert.ToString(ro.Next(100000000, 999999999));
                #region 邮箱模式覆写
                if (checkBox5.Checked == true)      //邮箱模式
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
                        u = u + "@qq.com";
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
                }
                #endregion
                #region 姓名模式覆写
                if (checkBox6.Checked)                  //姓名模式
                {
                    string[] fn = { "王", "李", "张", "刘", "陈", "杨", "黄", "赵", "吴", "周", "徐", "孙", "马", "朱", "胡", "郭", "何", "高", "林", "郑", "谢", "罗", "梁", "宋", "唐", "许", "韩", "冯", "邓", "曹", "彭", "曾", "肖", "田", "董", "袁", "潘", "于", "蒋", "蔡", "余", "杜", "叶", "程", "苏", "魏", "吕", "丁", "任", "沈", " 姚", "卢", "姜", "崔", "钟", "谭", "陆", "汪", "范", "金", "石", "廖", "贾", "夏", "韦", "付", "方", "白", "邹", "孟", "熊", "秦", "邱", "江", "尹", "薛", "闫", "段", "雷", "侯", "龙", "史", "陶", "黎", "贺", "顾", " 毛", "郝", "龚", "邵", "万", "钱", "严", "覃", "武", "戴", "莫", "孔", "向", "汤" };
                    string[] bn = { "世", "舜", "丞", "主", "产", "仁", "仇", "仓", "仕", "仞", "任", "伋", "众", "伸", "佐", "佺", "侃", "侪", "促", "俟", "信", "俣", "修", "倝", "倡", "倧", "偿", "储", "僖", "僧", "僳", "儒", "俊", "伟", "列", "则", "刚", "创", "前", "剑", "助", "劭", "势", "勘", "参", "叔", "吏", "嗣", "士", "壮", "孺", "守", "宽", "宾", "宋", "宗", "宙", "宣", "实", "宰", "尊", "峙", "峻", "崇", "崈", "川", "州", "巡", "帅", "庚", "战", "才", "承", "拯", "操", "斋", "昌", "晁", "暠", "曹", "曾", "珺", "玮", "珹", "琒", "琛", "琩", "琮", "琸", "瑎", "玚", "璟", "璥", "瑜", "生", "畴", "矗", "矢", "石", "磊", "砂", "碫", "示", "社", "祖", "祚", "祥", "禅", "稹", "穆", "竣", "竦", "综", "缜", "绪", "舱", "舷", "船", "蚩", "襦", "轼", "辑", "轩", "子", "杰", "榜", "碧", "葆", "莱", "蒲", "天", "乐", "东", "钢", "铎", "铖", "铠", "铸", "铿", "锋", "镇", "键", "镰", "馗", "旭", "骏", "骢", "骥", "驹", "驾", "骄", "诚", "诤", "赐", "慕", "端", "征", "坚", "建", "弓", "强", "彦", "御", "悍", "擎", "攀", "旷", "昂", "晷", "健", "冀", "凯", "劻", "啸", "柴", "木", "林", "森", "朴", "骞", "寒", "函", "高", "魁", "魏", "鲛", "鲲", "鹰", "丕", "乒", "候", "冕", "勰", "备", "宪", "宾", "密", "封", "山", "峰", "弼", "彪", "彭", "旁", "日", "明", "昪", "昴", "胜", "汉", "涵", "汗", "浩", "涛", "淏", "清", "澜", "浦", "澉", "澎", "澔", "濮", "濯", "瀚", "瀛", "灏", "沧", "虚", "豪", "豹", "辅", "辈", "迈", "邶", "合", "部", "阔", "雄", "霆", "震", "韩", "俯", "颁", "颇", "频", "颔", "风", "飒", "飙", "飚", "马", "亮", "仑", "仝", "代", "儋", "利", "力", "劼", "勒", "卓", "哲", "喆", "展", "帝", "弛", "弢", "弩", "彰", "征", "律", "德", "志", "忠", "思", "振", "挺", "掣", "旲", "旻", "昊", "昮", "晋", "晟", "晸", "朕", "朗", "段", "殿", "泰", "滕", "炅", "炜", "煜", "煊", "炎", "选", "玄", "勇", "君", "稼", "黎", "利", "贤", "谊", "金", "鑫", "辉", "墨", "欧", "有", "友", "闻", "问", "涛", "昌", "进", "林", "有", "坚", "和", "彪", "博", "诚", "先", "敬", "震", "振", "壮", "会", "群", "豪", "心", "邦", "承", "乐", "绍", "功", "松", "善", "厚", "庆", "磊", "民", "友", "裕", "河", "哲", "江", "超", "浩", "亮", "政", "谦", "亨", "奇", "固", "之", "轮", "翰", "朗", "伯", "宏", "言", "若", "鸣", "朋", "斌", "梁", "栋", "维", "启", "克", "伦", "翔", "旭", "鹏", "泽", "晨", "辰", "士", "以", "建", "家", "致", "树", "炎", "德", "行", "时", "泰", "盛", "雄", "琛", "钧", "冠", "策", "腾", "伟", "刚", "勇", "毅", "俊", "峰", "强", "军", "平", "保", "东", "文", "辉", "力", "明", "永", "健", "世", "广", "志", "义", "兴", "良", "海", "山", "仁", "波", "宁", "贵", "福", "生", "龙", "元", "全", "国", "胜", "学", "祥", "才", "发", "成", "康", "星", "光", "天", "达", "安", "岩", "中", "茂", "武", "新", "利", "清", "飞", "彬", "富", "顺", "信", "子", "杰", "楠", "榕", "风", "航", "弘" };
                    string[] gn = { "嘉", "琼", "桂", "娣", "叶", "璧", "璐", "娅", "琦", "晶", "妍", "茜", "秋", "珊", "莎", "锦", "黛", "青", "倩", "婷", "姣", "婉", "娴", "瑾", "颖", "露", "瑶", "怡", "婵", "雁", "蓓", "纨", "仪", "荷", "丹", "蓉", "眉", "君", "琴", "蕊", "薇", "菁", "梦", "岚", "苑", "婕", "馨", "瑗", "琰", "韵", "融", "园", "艺", "咏", "卿", "聪", "澜", "纯", "毓", "悦", "昭", "冰", "爽", "琬", "茗", "羽", "希", "宁", "欣", "飘", "育", "滢", "馥", "筠", "柔", "竹", "霭", "凝", "晓", "欢", "霄", "枫", "芸", "菲", "寒", "伊", "亚", "宜", "可", "姬", "舒", "影", "荔", "枝", "思", "丽", "真", "环", "雪", "荣", "爱", "妹", "月", "莺", "媛", "艳", "瑞", "凡", "佳" };
                    int mode = ro.Next(0, 9);
                    if (mode == 0 | mode == 8)
                    {
                        u = fn[ro.Next(0, fn.Length - 1)] + bn[ro.Next(0, bn.Length - 1)];
                    }
                    if (mode == 1)
                    {
                        u = fn[ro.Next(0, fn.Length - 1)] + gn[ro.Next(0, gn.Length - 1)];
                    }
                    if (mode == 2 | mode == 3 | mode == 4 | mode == 5)
                    {
                        u = fn[ro.Next(0, fn.Length - 1)] + bn[ro.Next(0, bn.Length - 1)] + bn[ro.Next(0, bn.Length - 1)];
                    }
                    if (mode == 6 | mode == 7)
                    {
                        u = fn[ro.Next(0, fn.Length - 1)] + gn[ro.Next(0, gn.Length - 1)] + gn[ro.Next(0, gn.Length - 1)];
                    }
                }
                #endregion
                if (rndua == true)                         //随机UA
                {
                    string path = "C:\\Users\\" + System.Environment.UserName + "\\Desktop\\config.ini";
                    fuckxml lxm = new fuckxml();
                    ua = lxm.ReadIt(path, "UA-pool", ro.Next(1,12).ToString()); ;
                    textBox4.Text = ua;
                }
                else
                {
                    ua = textBox4.Text;
                }
                
                
                label4.Text = u;
                label5.Text = p;
                u = Uri.EscapeDataString(u);        //URI编码
                p = Uri.EscapeDataString(p);
                string post = uname + "=" + u + "&" + pname + "=" + p + textBox11.Text;
                textBox13.Text = post;
                WebProxy proxy = new WebProxy("127.0.0.1", 8118);
                try
                {
                    http pack = new http(url, post, ip, ua, accept, ct, refer, ck, proxy, px);
                   string re = pack.HttpPost();
                    Application.DoEvents();
                    textBox2.Text = "NO." + Convert.ToString(i) + ":   \r\n" + re;
                    Application.DoEvents();
                }
                catch (System.Exception e1)
                {
                    textBox2.Text = "NO." + Convert.ToString(i) + ":   " + e1.Message + Environment.NewLine + textBox2.Text;
                }
               
                progressBar1.Value++;
                label8.Text = Convert.ToString(Convert.ToSingle(i) / Convert.ToSingle(textBox1.Text) * 100 + "%");
                Application.DoEvents();
                #region 睡眠模式
                if (checkBox3.Checked == true)      //Sleep mode
                {
                    int s = ro.Next(5, 20);
                    progressBar2.Value = 0;
                    for (int l = 1; l <= s; l++)
                    {
                        if (stop == true)
                        {
                            progressBar2.Value = 0;
                            label20.Text = "## s";
                            break;
                        }
                        progressBar2.Maximum = s;
                        Delay(1000);
                        label20.Text = Convert.ToString(s - l) + " s";
                        progressBar2.Value++;
                    }
                }
                #endregion
                #region 恶心模式
                if (checkBox9.Checked == true)      //Suck mode
                {
                    int s = ro.Next(5000, 20000);
                    progressBar2.Value = 0;
                    for (int l = 1; l <= s; l++)
                    {
                        if (stop == true)
                        {
                            progressBar2.Value = 0;
                            label20.Text = "## s";
                            break;
                        }
                        progressBar2.Maximum = s;
                        Delay(1000);
                        label20.Text = Convert.ToString(s - l) + " s";
                        progressBar2.Value++;
                    }
                }
                #endregion
            }
            textBox1.Enabled = true;
            textBox9.Enabled = true;
            textBox10.Enabled = true;
            textBox7.Enabled = true;
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stop = true;
            textBox3.Enabled = true;
            textBox1.Enabled = true;
            textBox9.Enabled = true;
            textBox10.Enabled = true;
            textBox7.Enabled = true;
            button1.Enabled = true;
            progressBar1.Value = 0;
            progressBar2.Value = 0;
        }
        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.BackColor = Color.FromArgb(255, 255, 232, 232);
        }
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.WhiteSmoke;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = "C:\\Users\\" + System.Environment.UserName + "\\Desktop\\config.ini";
            fuckxml lxm = new fuckxml();
            textBox7.Text = lxm.ReadIt(path, textBox14.Text, "url");
            textBox9.Text = lxm.ReadIt(path, textBox14.Text, "user");
            textBox10.Text = lxm.ReadIt(path, textBox14.Text, "password");
            textBox5.Text = lxm.ReadIt(path, textBox14.Text, "accept");
            textBox8.Text = lxm.ReadIt(path, textBox14.Text, "refer");
            textBox11.Text = lxm.ReadIt(path, textBox14.Text, "addition");
        }
    }
    public class http
    {
        string Url, postDataStr, ip, ua, accept, ct, refer, ck;
        WebProxy proxy;
        bool px;
        #region 构造函数
        public http(string Url, string postDataStr, string ip, string ua, string accept, string ct, string refer, string ck, WebProxy proxy, bool px)
        {
            this.Url = Url;
            this.postDataStr = postDataStr;
            this.ip = ip;
            this.ua =ua ;
            this.accept = accept;
            this.ct = ct;
            this.refer = refer;
            this.ck = ck;
            this.proxy = proxy;
            this.px = px;
        }
        #endregion
        public string HttpPost()
        {
             HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            /*Cookie cookie = new Cookie();
            cookie.Name = "PHPSESSID";
            cookie.Value = "kvraft0qss8lqe2l1kck7u3ba6";
            cookie.Domain = "1.qqaba.cn";
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
            request.Referer =this. refer;
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
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
            int i = GetPrivateProfileString(section, key, "未能从" + path + "读取到指定信息，检查文件和section", temp, 255, path);
            int jj = temp.Length;
            return temp.ToString();
        }
    }
}