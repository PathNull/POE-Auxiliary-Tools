using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Core
{
    public class ListViewModel
    {
        public List<ListViewHeader> Headers { get; set; }



        public ListViewModel()
        {
            this.Headers = new List<ListViewHeader>();
        }
    }
    /// <summary>
    /// 表头
    /// </summary>
    public class ListViewHeader
    {
        /// <summary>
        /// 字段名称（数据库中）
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 表头名称
        /// </summary>
        public string HeaderName { get; set; }
        /// <summary>
        /// 对应的控件Name
        /// </summary>
        public string ControlName { get; set; }
        /// <summary>
        /// 输入为空提示
        /// </summary>
        public string EmptyTips { get; set; }
        /// <summary>
        /// 输入长度限制
        /// </summary>
        public int MaxLength { get; set; }
        /// <summary>
        /// 是否允许修改
        /// </summary>
        public bool IsAllowUpdates = true;
        /// <summary>
        /// 显示的字段名（绑定到控件上的内容）
        /// </summary>
        public string DisplayFieldName { get; set; }
        /// <summary>
        /// 允许输入的字符类型
        /// </summary>
        public LimitTypeEnum LimitType { get; set; }
        /// <summary>
        /// 固定长度（此字段不为空，则需要等于该长度）
        /// </summary>
        public int FixedLength { get; set; }
        /// <summary>
        /// 是否验证是电子邮件地址
        /// </summary>
        public bool IsEmail = false;
        /// <summary>
        /// 是否是电话号码
        /// </summary>
        public bool IsTelephoneNumber = false;
        /// <summary>
        /// 默认值
        /// </summary>
        public string Default = "";
        //最后赋值，处理comboBox联级绑定问题
        public bool LastAssignment = false;
        /// <summary>
        /// 是否隐藏该列
        /// </summary>
        public bool IsHide = false;
        /// <summary>
        /// 是否需要为空
        /// </summary>
        public bool IsEmpty = false;
        /// <summary>
        /// 验证是否是身份证
        /// </summary>
        public bool IsDCard = false;
        /// <summary>
        /// 是否赋值给控件
        /// </summary>
        public bool IsAssignment = true;
        /// <summary>
        /// 绑定值的顺序
        /// </summary>
        public int ComboBoxOrder = 0;

        public Color FontColor = Color.Black;
        /// <summary>
        /// 为0时是否显示数字0,为否时显示空白
        /// </summary>
        public bool IsShowZero = false;
        /// <summary>
        /// 是否将True 或 False 显示为 是 或 否
        /// </summary>
        public bool IsShowBoolCN = false;

    }

    public enum LimitTypeEnum
    {
        无,
        字母,
        数字,
        中文,
        纯数字
    }
}
