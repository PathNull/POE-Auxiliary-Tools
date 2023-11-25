using Core.Common;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POE_Auxiliary_Tools.基础数据
{
    public partial class 用户信息 : BaseForm
    {
        StringBuilder sbr = new StringBuilder();
        public 用户信息()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_name.Text=="")
            {
                MessageBox.Show("论坛昵称不能为空！", "提示信息");
                return;
            }
            if (txt_posid.Text == "")
            {
                MessageBox.Show("POESESSID不能为空！", "提示信息");
                return;
            }

            sbr.Clear();
            sbr.Append($"SELECT * FROM 用户属性");
            var t = sbr.ToString();
            DataTable dt = MainFrom.database.ExecuteDataTable(t);
            var list = DataHandler.TableToListModel<用户信息Mode>(dt);
            sbr.Clear();
            if (list.Count==0)
            {
               
                sbr.Append("INSERT INTO 用户属性 (POESESSID,论坛名称,赛季) VALUES ");
                sbr.Append($"('{txt_posid.Text}','{txt_name.Text}','{txt_sj.Text}')");
             
            }
            else
            {
                sbr.Append($"UPDATE 用户属性 SET POESESSID = '{txt_posid.Text}' ,论坛名称 ='{txt_name.Text}',赛季='{txt_sj.Text}' ");
            }
            var cmdText = sbr.ToString();
            MainFrom.database.ExecuteNonQuery(cmdText);
            MessageBox.Show("保存成功！","提示信息");
            this.Close();


        }

        private void Frm_用户信息_Load(object sender, EventArgs e)
        {
            sbr.Clear();
            sbr.Append($"SELECT * FROM 用户属性");
            var t = sbr.ToString();
            DataTable dt = MainFrom.database.ExecuteDataTable(t);
            var list = DataHandler.TableToListModel<用户信息Mode>(dt);
            if (list.Count>0)
            {
                txt_posid.Text = list[0].POESESSID;
                txt_name.Text = list[0].论坛名称;
                txt_sj.Text = list[0].赛季;
            }
        }
    }
}
