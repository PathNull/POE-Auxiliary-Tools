using Model.ConfigModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Core
{
    public static class XMLHandler
    {
        public static void CreateXMLDocument(string name)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
            XmlDeclaration xmlDeclar;
            xmlDeclar = xmlDoc.CreateXmlDeclaration("1.0", "gb2312", null);
            xmlDoc.AppendChild(xmlDeclar);

            //加入Employees根元素
            XmlElement xmlElement = xmlDoc.CreateElement("", "Goods", "");
            xmlDoc.AppendChild(xmlElement);

            //添加节点
            //XmlNode root = xmlDoc.SelectSingleNode("Employees");
            xmlDoc.Save($"../../{name}.xml");//保存的路径
        }

        public static void CreateNode(string fileName,string Content)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load($"../../{fileName}.xml");//找到xml文件
            XmlNode root = xmlDoc.SelectSingleNode("Goods");//查找Employees节点
            XmlElement xe1 = xmlDoc.CreateElement("Node");//添加Node2节点
            xe1.SetAttribute("Item", "物品");
            XmlElement xeSub1 = xmlDoc.CreateElement("Name");//定义子节点
            xeSub1.InnerText = Content;
            xe1.AppendChild(xeSub1);//添加节点到Node2
            root.AppendChild(xe1);//添加节点到Employees
            xmlDoc.Save($"../../{fileName}.xml");
        }
        public static void ModifyNode()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("D:\\book.xml");
            XmlNodeList nodeList = xmlDocument.SelectSingleNode("Employees").ChildNodes;//获取Employees节点的所有子节点

            foreach (XmlNode xn in nodeList)//遍历
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.GetAttribute("Name") == "薪薪代码")
                {
                    xe.SetAttribute("Name", "薪薪");//更改节点的属性

                    XmlNodeList xnl = xe.ChildNodes;//获取xe的所有子节点
                    foreach (XmlNode xn1 in xnl)
                    {
                        XmlElement xe2 = (XmlElement)xn1;//将节点xn1的属性转换为XmlElement
                        if (xe2.Name == "title")//找到节点名字为title的节点
                        {
                            xe2.InnerText = "今天天气不好";
                        }

                        if (xe2.Name == "price")
                        {
                            XmlNodeList xnl2 = xe2.ChildNodes;
                            foreach (XmlNode xn2 in xnl2)
                            {
                                if (xn2.Name == "weight")
                                {
                                    xn2.InnerText = "88";
                                }
                            }
                        }
                    }
                }
            }

            xmlDocument.Save("D:\\book2.xml");
        }
        public static void DeleteNode()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("D:\\book1.xml");
            XmlNodeList xnl = xmlDocument.SelectSingleNode("Employees").ChildNodes;

            foreach (XmlNode xn in xnl)
            {
                if (xn.Name == "Node")
                {
                    XmlElement xe = (XmlElement)xn;//将xn的属性转换为XmlElement
                    xe.RemoveAttribute("ID");//移除xe的ID属性
                    XmlNodeList xnl2 = xe.ChildNodes;
                    for (int i = 0; i < xnl2.Count; i++)
                    {
                        XmlElement xe2 = (XmlElement)xnl2.Item(i);
                        if (xe2.Name == "title")
                        {
                            xe.RemoveChild(xe2);//删除节点title
                        }
                    }
                }
            }
            xmlDocument.Save("D:\\book3.xml");
        }

        public static List<string> Read(string name)
        {
            List<string> list = new List<string>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load($"../../{name}.xml");
            // 得到根节点bookstore
            XmlNode xn = xmlDocument.SelectSingleNode("Goods");


            // 得到根节点的所有子节点
            XmlNodeList xnl = xn.ChildNodes;

            foreach (XmlNode xn1 in xnl)
            {
                GoodsModel model = new GoodsModel();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Type和ISBN两个属性的属性值
                model.Type = xe.GetAttribute("Item").ToString();
                // 得到Book节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                model.Name = xnl0.Item(0).InnerText;

                list.Add(model.Name);
            }
            return list;
        }

    }
}
