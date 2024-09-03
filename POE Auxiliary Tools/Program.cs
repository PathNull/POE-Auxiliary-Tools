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
    }
}
