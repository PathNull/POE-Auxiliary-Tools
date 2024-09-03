using Core.SQLite;
using EntRail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POE_Auxiliary_Tools
{
    internal static class Program
    {
        public static 用户信息Mode baseInfo = new 用户信息Mode();
        public static List<UserAgent> userAgentList = new List<UserAgent>();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //限制使用时间
            var date = DateTime.Now;
            if(date< Convert.ToDateTime("2024/12/30"))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainFrom());
            }
        }
        private static void Init()
        {
            Program.userAgentList.Add(new UserAgent() { agent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36 Edg/119.0.0.0" });
            Program.userAgentList.Add(new UserAgent() { agent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36" });
            Program.userAgentList.Add(new UserAgent() { agent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:89.0) Gecko/20100101 Firefox/89.0" });
            Program.userAgentList.Add(new UserAgent() { agent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.0 Safari/605.1.15" });
            Program.userAgentList.Add(new UserAgent() { agent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36 Edg/91.0.864.37" });
            Program.userAgentList.Add(new UserAgent() { agent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko" });
        }
    }
}
