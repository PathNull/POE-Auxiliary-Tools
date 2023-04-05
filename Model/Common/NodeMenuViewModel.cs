using System.Collections.Generic;

namespace Model.Common.ViewModels
{
    /// <summary>
    /// 左侧导航视图模型
    /// </summary>
    public class NodeMenuViewModel
    {
        public List<NodeMenu> Nodes { get; set; }

        public NodeMenuViewModel()
        {
            this.Nodes = new List<NodeMenu>();
        }
    }

    public class NodeMenu
    {
        public string Id { get; set; }
        public string 节点名称 { get; set; }

        public int 节点顺序 { get; set; }

        public bool isEnable { get; set; }

        public List<NodeMenu> Nodes { get; set; }

        public NodeMenu()
        {
            this.Nodes = new List<NodeMenu>();
        }
    }


}
