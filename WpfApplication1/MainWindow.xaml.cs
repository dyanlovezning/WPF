using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        List<LeafNode> roots = new List<LeafNode>();
        public MainWindow()
        {
            InitializeComponent();
            InitTree();
            //twLeaf.ItemsSource = roots;

        }

        private void twLeaf_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.OriginalSource as TreeViewItem;
            //item.IsExpanded = true;

            listv.Items.Add(item.ToString());
            listv.Items.Add(sender.ToString());

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("test.xml");

            XmlElement root = doc.DocumentElement;

            XmlNodeList nlist = root.GetElementsByTagName("模板0");

            foreach (XmlNode nd in nlist)
            {
                
                string str = ((XmlElement)nd).GetElementsByTagName("点0")[0].InnerText;
                listv.Items.Add(nd.Name);
            }
        }

        private void InitTree()
        {          

            for (int i = 0; i < 1; i++)
            {
                LeafNode child = new LeafNode() { Data = "模板" };
                child.Children = new List<LeafNode>();
                roots.Add(child);
                ////10 children
                for (int j = 0; j < 3; j++)
                {
                    LeafNode subChild = new LeafNode() { Data = "模板" + j.ToString() };
                    subChild.Children = new List<LeafNode>();
                    ////15 children
                    child.Children.Add(subChild);
                    for (int k = 0; k < 6; k++)
                    {
                        LeafNode gs = new LeafNode() { Data = "点" + k.ToString() };
                        gs.Children = new List<LeafNode>();
                        subChild.Children.Add(gs);
                        for (int ki = 0; ki < 2; ki++)
                        {
                            LeafNode gss = new LeafNode() { Data = "x" + ki.ToString() };
                            gs.Children.Add(gss);
                        }
                    }
                }
            }

            for (int i = 0; i < roots.Count; i++)
            {
                TreeViewItem item = new TreeViewItem();
                AddLeaf(item, roots[i]);  //call 

                twLeaf.Items.Add(item);
            }
        }

        private void AddLeaf(TreeViewItem twItem, LeafNode node)
        {
            if (node == null)
            {
                return;
            }

            twItem.Header = node.Data;

            if (node.Children != null && node.Children.Count > 0)
            {
                for (int i = 0; i < node.Children.Count; i++)
                {
                    TreeViewItem it = new TreeViewItem();
                    AddLeaf(it, node.Children[i]);
                    twItem.Items.Add(it);
                }
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(sender.ToString());
            TreeViewItem item = twLeaf.SelectedItem as TreeViewItem;
            if (item != null)
            {
                TreeViewItem pi = item.Parent as TreeViewItem;
                if (pi != null)
                {
                    pi.Items.Remove(item);
                }
                else
                {
                    twLeaf.Items.Remove(item);
                }
            }


        }

        List<LeafNode> nodeList = new List<LeafNode>();
        private void btnGetNodes_Click(object sender, RoutedEventArgs e)
        {
            nodeList.Clear();
            for (int i = 0; i < twLeaf.Items.Count; i++)
            {
                TreeViewItem rootItem = twLeaf.Items[i] as TreeViewItem;
                if (null != rootItem)
                {
                    GetTreeviewNodes(null, rootItem);
                }
            }

            foreach (LeafNode a in nodeList)
            {
                listv.Items.Add(a.Data);
                if (a.Children != null)
                    foreach (LeafNode b in a.Children)
                    {
                        listv.Items.Add(b.Data);
                        if (b.Children != null)
                            foreach (LeafNode c in b.Children)
                                listv.Items.Add(c.Data);
                    }
                            
            }
        }

        private void GetTreeviewNodes(LeafNode parentNode, TreeViewItem twItem)
        {
            LeafNode node = new LeafNode()
            {
                Data = twItem.Header.ToString(),
                Children = new List<LeafNode>()
            };

            if (parentNode != null)
            {
                parentNode.Children.Add(node);

                //listv.Items.Add(parentNode.Data);
            }
            else
            {
                nodeList.Add(node);
            }

            if (twItem.Items.Count > 0)
            {
                for (int i = 0; i < twItem.Items.Count; i++)
                {
                    TreeViewItem item = twItem.Items[i] as TreeViewItem;
                    if (null != item)
                    {
                        GetTreeviewNodes(node, item);

                        //listv.Items.Add(item.Header.ToString());
                    }
                }
            }
            else
            {
                return;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listv.Items.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Tavset t = new Tavset("XMLFile.xml");
            //t.LoadXml2TV(treeView1);
            //treeView1.TopNode.Expand();
            XmlDocument xml = new XmlDocument();
            XmlElement root;

            XmlDeclaration dec = xml.CreateXmlDeclaration("1.0", "utf-8", null);
            xml.AppendChild(dec);

            root = xml.CreateElement("模板库");
            xml.AppendChild(root);

            //foreach (ItemCollection a in twLeaf.Items)
            //{
            //    listv.Items.Add(a.ToString());
            //}

            for (int i = 0; i < twLeaf.Items.Count; i++)
            {
                TreeViewItem rootItem = twLeaf.Items[i] as TreeViewItem;
                //listv.Items.Add(rootItem.ToString());

                if (null != rootItem)
                {
                    GetTreeviewNodes(null, rootItem);
                }
            }

            XmlElement a1, a2, a3;
            //XmlText t1;

            //a1 = xml.CreateElement("子节点1");
            ////a1.SetAttribute("id", "1");
            //a2 = xml.CreateElement("name");
            //t1 = xml.CreateTextNode("text");
            //a2.AppendChild(t1);
            //a1.AppendChild(a2);
            //root.AppendChild(a1);

            foreach (LeafNode a in nodeList)
            {
                //a1 = xml.CreateElement( a.Data );
                //root.AppendChild(a1);

                listv.Items.Add(a.Data);
                if (a.Children != null)
                    foreach (LeafNode b in a.Children)
                    {
                        a2 = xml.CreateElement( b.Data );
                        root.AppendChild(a2);
                        listv.Items.Add(b.Data);
                        if (b.Children != null)
                            foreach (LeafNode c in b.Children)
                            {
                                a3 = xml.CreateElement(c.Data);
                                a2.AppendChild(a3);

                                listv.Items.Add(c.Data);
                            }
                    }

            }
           

            xml.Save("test.xml");

            //nodeList.Clear();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.listv.Columns.Add("列标题1", 120, HorizontalAlignment.Left); //一步添加

        }

        /// <summary>
        /// 存储不成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //SaveXml.savedata(nodeList, "test1.xml");
            //XmlDataDocument doc = new XmlDataDocument();
           

            //FileStream file = new FileStream("test.xml",FileMode.Open,FileAccess.Read);
            //doc.Load(file);

            //加载文件
            XmlDocument doc = new XmlDocument();
            doc.Load("test.xml");

            //Root 标签
            XmlNode xnode;
            xnode = doc.ChildNodes[1];

            TreeViewItem itm = new TreeViewItem();
            itm.Header = doc.DocumentElement.Name;

            //清空TreeView
            twLeaf.Items.Clear();
       
            //twLeaf.Items.Add(itm);
            
            //TreeViewItem itms = new TreeViewItem();
            //itms = twLeaf.
            
            add_nodes(xnode, itm);
            twLeaf.Items.Add(itm);
            //itms = twLeaf.Items
        }

        public void add_nodes(XmlNode x_node, TreeViewItem t_node)
        {
            XmlNode xnode;       
            XmlNodeList node_list;
            int i;

            if (x_node.HasChildNodes)
            {
                node_list = x_node.ChildNodes;
                for (i = 0; i <= node_list.Count-1; i++)
                {
                    xnode = x_node.ChildNodes[i];

                    TreeViewItem tnode = new TreeViewItem();
                    tnode.Header = xnode.Name;
                    t_node.Items.Add(tnode);

                    //tnode = t_node.Items[i];
                    add_nodes(xnode, tnode);

                    //TreeViewItem it = new TreeViewItem();
                    //AddLeaf(it, node.Children[i]);
                    //twItem.Items.Add(it);
                }
            }
        }
    }


    /// <summary>
    /// Tree 节点
    /// </summary>
    public class LeafNode
    {
        private string data;
        public string Data
        {
            get { return data; }
            set { data = value; }
        }

        private List<LeafNode> children;
        public List<LeafNode> Children
        {
            get { return children; }
            set { children = value; }
        }
    }

    public class SaveXml
    {
        public static void savedata(object obj, string filename)
        {
            XmlSerializer sr = new XmlSerializer(obj.GetType());
            TextWriter write = new StreamWriter(filename);
            sr.Serialize(write, obj);
            write.Close();
        }
    }
}
