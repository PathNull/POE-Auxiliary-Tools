using AutoHotkey.Interop;
using Core.SQLite;
using DevExpress.XtraEditors;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace POE_Auxiliary_Tools 
{
    public partial class Frm_自动按键 : BaseForm
    {
        private AutoHotkeyEngine ahk;
        private MouseHook mouseHook;
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);


        public void TriggerLoadEvent()
        {
            this.OnLoad(EventArgs.Empty);
        }
        public static class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("gdi32.dll")]
            public static extern int BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight,
                IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        }


        public Frm_自动按键()
        {
            InitializeComponent();
            ahk = new AutoHotkeyEngine();
            mouseHook = new MouseHook();
            mouseHook.MouseClick += MouseHook_MouseClick;

#if DEBUG
            var path_kf = GetApplicationPath();
            var loadPath_zdcy = path_kf + "AHK Script\\火炬自动吃药.ahk";
            ahk.LoadFile(loadPath_zdcy);
#else
             var path_sc = Environment.CurrentDirectory;
                var loadPath_zdcy = path_sc + "\\Script\\火炬自动吃药.ahk";
                ahk.LoadFile(loadPath_zdcy);
#endif
        }
        private static string GetApplicationPath()
        {
            string path = Application.StartupPath;
            string folderName = String.Empty;
            while (folderName.ToLower() != "bin")
            {
                path = path.Substring(0, path.LastIndexOf("\\"));
                folderName = path.Substring(path.LastIndexOf("\\") + 1);
            }
            return path.Substring(0, path.LastIndexOf("\\") + 1);
        }
        private void Frm_自动按键_Load(object sender, EventArgs e)
        {
          
        }
        private void MouseHook_MouseClick(object sender, MouseEventArgs e)
        {
            // 获取鼠标点击位置
            Point mousePosition = e.Location;

            // 获取屏幕上鼠标位置的颜色
            Color pixelColor = GetPixelColor(mousePosition);


            // 将颜色转换为 0x 格式的十六进制值
            string hexColor = "0x" + ColorTranslator.ToWin32(pixelColor).ToString("X");

            // 将颜色值赋给TextBox
            txt_color.Text = hexColor;
            //坐标
            txt_zb.Text = $"X:{mousePosition.X} Y:{mousePosition.Y}";

            mouseHook.Stop();
        }
        //获取基准点
        private void btn_getPot_Click(object sender, EventArgs e)
        {
            // 模糊匹配的进程名称
            string processName = "Torchlight";

            Process[] processes = Process.GetProcesses();

            // 使用模糊匹配算法找到匹配的进程名称
            Process matchedProcess = processes.FirstOrDefault(p => Regex.IsMatch(p.ProcessName, processName, RegexOptions.IgnoreCase));

            if (matchedProcess != null)
            {
                // 获取匹配进程的主窗口句柄
                IntPtr mainWindowHandle = matchedProcess.MainWindowHandle;

                // 将窗口置顶
                SetForegroundWindow(mainWindowHandle);
            }
            else
            {
                MessageBox.Show("未找到匹配的进程");
            }
            mouseHook.Start();
        }
        private Color GetPixelColor(Point position)
        {
            Bitmap screenPixel = new Bitmap(1, 1);
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = NativeMethods.BitBlt(hDC, 0, 0, 1, 1, hSrcDC, position.X, position.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }

            return screenPixel.GetPixel(0, 0);
        }
        //启用或禁用自动喝药
        private void ce_zdhy_CheckedChanged(object sender, EventArgs e)
        {
            if (txt_color.Text=="")
            {
                MessageBox.Show("请先获取颜色点！");
                return;
            }
            var isStartUp = ce_zdhy.Checked;
            var frequency = cb_jcpl.SelectedItem.ToString();
            var jgTime = cb_jg.SelectedItem.ToString();  //吃血后暂停检测的时间
            var pot = txt_zb.Text.Split(' ');
            var x = pot[0].Substring(2, pot[0].Length - 2);
            var y = pot[1].Substring(2, pot[1].Length - 2);
            ahk.SetVar("posX", x);
            ahk.SetVar("posY", y);
            ahk.SetVar("color", txt_color.Text);
            ahk.SetVar("frequency", frequency);
            ahk.SetVar("jgTime", jgTime);
            ahk.ExecFunction("WAutoBloodBottle_HJ", isStartUp.ToString(), frequency);
        }
    }
    public static class NativeMethods
    {
        [DllImport("gdi32.dll")]
        public static extern int BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight,
            IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
    }

    public class MouseHook
    {
        private IntPtr hookId = IntPtr.Zero;
        public event EventHandler<MouseEventArgs> MouseClick;

        public void Start()
        {
            HookCallback mouseDelegate = MouseHookCallback;
            hookId = SetHook(mouseDelegate);
        }

        public void Stop()
        {
            if (hookId != IntPtr.Zero)
            {
                UnhookWindowsHookEx(hookId);
                hookId = IntPtr.Zero;
            }
        }

        private IntPtr SetHook(HookCallback proc)
        {
            using (ProcessModule module = Process.GetCurrentProcess().MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(module.ModuleName), 0);
            }
        }

        private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_LBUTTONDOWN)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                MouseEventArgs args = new MouseEventArgs(MouseButtons.Left, 1, hookStruct.pt.x, hookStruct.pt.y, 0);
                OnMouseClick(args);
            }

            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        protected virtual void OnMouseClick(MouseEventArgs e)
        {
            MouseClick?.Invoke(this, e);
        }

        private delegate IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam);

        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public int mouseData;
            public int flags;
            public int time;
            public IntPtr dwExtraInfo;
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookCallback lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
