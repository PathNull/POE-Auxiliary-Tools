using Core.Common;
using POE_Auxiliary_Tools.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POE_Auxiliary_Tools.查询类工具
{
    public partial class Fm_价格缓存 : Form
    {
        private string _name;
        public Fm_价格缓存(string name)
        {
            _name = name;
            InitializeComponent();
        }

        private void Fm_价格缓存_Load(object sender, EventArgs e)
        {
            StringBuilder sbr = new StringBuilder();
            sbr.Clear();
            sbr.Append($@"SELECT 
	                        查询时间,物品名称,价格,通货类型,物品类型,数量,
                          CASE
                            WHEN (strftime('%s', 'now') - strftime('%s', 上架时间)) < 60 THEN '几秒前'
                            WHEN (strftime('%s', 'now') - strftime('%s', 上架时间)) < 3600 THEN CAST((strftime('%s', 'now') - strftime('%s', 上架时间)) / 60 AS INTEGER) || '分钟前'
                            ELSE CAST((strftime('%s', 'now') - strftime('%s', 上架时间)) / 3600 AS INTEGER) || '小时前'
                          END AS 上架时间,CAST((数量*价格) AS DOUBLE) as 总价
                        FROM (
                          SELECT *,strftime('%Y-%m-%d %H:%M:%S', 上架时间) AS 上架时间 FROM 查询缓存 WHERE 物品名称='{_name}'
                        ) t;");
            var cmdText = sbr.ToString();
            DataTable _dt = MainFrom.database.ExecuteDataTable(cmdText);
            var list = DataHandler.TableToListModel<查询缓存>(_dt);
            
            gridControl1.DataSource = list.OrderBy(x=>Convert.ToDouble(x.价格));
        }
    }
}
