using DevExpress.XtraCharts.Designer.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPrinting.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using TextEdit = DevExpress.XtraEditors.TextEdit;

namespace POE_Auxiliary_Tools
{
    public partial class Frm_装备类工具 : Form
    {
        string clipboardText;
        public Frm_装备类工具()
        {
            InitializeComponent();
        }
        public void TriggerLoadEvent()
        {
            this.OnLoad(EventArgs.Empty);
        }

        const int WM_CLIPBOARDUPDATE = 0x031D;

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            AddClipboardFormatListener(this.Handle);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            RemoveClipboardFormatListener(this.Handle);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_CLIPBOARDUPDATE)
            {
                // 剪贴板的内容已更改，获取最新的文本内容
                if (Clipboard.ContainsText())
                {
                    clipboardText = Clipboard.GetText();
                    listBoxControl_zbsx.Items.Clear();
                    var list = clipboardText.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    foreach (var item in list)
                    {
                        listBoxControl_zbsx.Items.Add(item);
                    }

                }
            }
            base.WndProc(ref m);
        }

        private void listBoxControl_zbsx_DoubleClick(object sender, EventArgs e)
        {
            //ListBoxControl listBox = sender as ListBoxControl;
            //if (listBox != null)
            //{
            //    int index = listBox.SelectedIndex;
            //    if (index != -1)
            //    {
            //        string selectedItem = listBox.SelectedItem.ToString();
            //        var i = tableLayoutPanel.RowCount;
            //        // 针对每个 TextEdit 控件创建一个新的行，并设置行高
            //        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            //        var textEdit = new TextEdit() { Text = selectedItem, Width = 200 };
            //        var checkBox = new CheckBox();
            //        // 添加 TextEdit 控件，并将其放置在正确的行中
            //        tableLayoutPanel.Controls.Add(textEdit, 0, i);
            //        tableLayoutPanel.Controls.Add(checkBox, 0, i);
            //        tableLayoutPanel.SetRow(textEdit, i);
            //        tableLayoutPanel.SetRow(checkBox, i);
            //        tableLayoutPanel.SetColumn(checkBox, 0);
            //        tableLayoutPanel.SetColumn(textEdit, 1);
            //    }
            //}
        }
    }
}
