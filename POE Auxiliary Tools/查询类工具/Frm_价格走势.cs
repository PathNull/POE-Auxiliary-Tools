using Core.Common;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Native;
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
    public partial class Frm_价格走势 : BaseForm
    {
        StringBuilder sbr = new StringBuilder();
        private string _name;
        public Frm_价格走势(string name)
        {
            InitializeComponent();
            _name = name;
        }

        private void Frm_价格走势_Load(object sender, EventArgs e)
        {
            comboBoxEdit_sjd.SelectedIndex = 0;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        public List<查询记录> GetData(string text)
        {
            DateTime date = DateTime.Now;
            DateTime start= DateTime.Now;
            switch (text)
            {
                case "近一周":
                    start = date.AddDays(-7);
                    break;
                case "近一个月":
                    start = date.AddDays(-30);
                    break;
                default:
                    break;
            }
            sbr.Clear();
            sbr.Append($@"SELECT * FROM 查询记录 WHERE 物品名称='{_name}' 
                        AND  查询时间 > '{start.ToString("yyyy-MM-dd HH:mm:ss")}' ORDER BY 查询时间");
            DataTable dt = MainFrom.database.ExecuteDataTable(sbr.ToString());
            var list = DataHandler.TableToListModel<查询记录>(dt);
            return list;
        }
        //时间段变化
        private void comboBoxEdit_sjd_SelectedIndexChanged(object sender, EventArgs e)
        {
            chartControl1.Series.Clear();
            var text = comboBoxEdit_sjd.Text;
            //创建一个折线图Series对象
            Series series = new Series(_name, ViewType.Line);

            //获取数据
            var list = GetData(text);
            var index = 1;
            foreach (var item in list)
            {
                var data = Convert.ToDateTime(item.查询时间);
                var time = data.ToString("yyyy/MM/dd HH:mm");
                var price = Math.Round(Convert.ToDouble(item.价格), 2);
                //向Series对象添加数据点
                series.Points.Add(new SeriesPoint(index, price));
                index++;
            }

            //将Series对象添加到ChartControl中
            chartControl1.Series.Add(series);

            // 获取 X 轴对象
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = (XYDiagram)this.chartControl1.Diagram;
            xyDiagram1.AxisX.DateTimeScaleOptions.ScaleMode = ScaleMode.Continuous;//x轴是扫描轴，时间类型
            //xyDiagram1.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Minute;//测量单位是秒这样才能显示到秒
            //xyDiagram1.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Minute;
            //设置ChartControl的X轴和Y轴
            ((XYDiagram)chartControl1.Diagram).AxisX.NumericScaleOptions.AutoGrid = false;
            ((XYDiagram)chartControl1.Diagram).AxisX.NumericScaleOptions.GridSpacing = 1;
            ((XYDiagram)chartControl1.Diagram).AxisY.NumericScaleOptions.AutoGrid = false;
            ((XYDiagram)chartControl1.Diagram).AxisY.NumericScaleOptions.GridSpacing = 10;
        }
    }
}
