using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Core.Popup;

namespace POE_Auxiliary_Tools
{
    public partial class POESESSID输入 : BaseForm
    {
        DialogResult dialogResult;
        StringBuilder sbr = new StringBuilder();
        public POESESSID输入()
        {
            InitializeComponent();
        }

        private void simpleButton_bc_Click(object sender, EventArgs e)
        {

            //检查token是否正确
            if (textEdit_token.Text.Length!=32)
            {
                dialogResult = Popup.Tips(this, "POESESSID不正确!", "提示信息", PopUpType.Info);
                return;
            }
            //保存POESESSID
            sbr.Clear();
            sbr.Append("INSERT INTO 用户属性 (POESESSID) VALUES ");
            sbr.Append($"('{textEdit_token.Text}')");
            var cmdText = sbr.ToString();
            MainFrom.database.ExecuteNonQuery(cmdText);
            this.Close();
        }
    }
}
