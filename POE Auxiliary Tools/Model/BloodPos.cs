using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Path_of_Exile_Tool.Model
{
    /// <summary>
    /// 血量对应的坐标点
    /// </summary>
    public class BloodPos
    {
        /// <summary>
        /// 百分比
        /// </summary>
        public string percentage { get; set; }

        public string  PosX { get; set; }

        public string PosY { get; set; }

        /// <summary>
        /// 有血时颜色
        /// </summary>
        public string Color { get; set; }
    }
}
