using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace GetValueNode
{
    public class GetNode
    {
        #region Xử lý HtmlNode
        /// <summary>
        /// Trả về One Tag node
        /// </summary>
        /// <param name="html"></param>
        /// <param name="tag"></param>
        /// <param name="attt"></param>
        /// <param name="value"></param>
        /// <param name="Contain"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static HtmlNode GetOneTag(string html, string tag, string attt, string value, bool Contain = false, int index = 0)
        {
            HtmlNode node;
            node = GetListTag(html, tag, attt, value, Contain)[index];
            return node;
        }
        /// <summary>
        ///  Trả về List Node
        /// </summary>
        /// <param name="html"></param>
        /// <param name="tag"></param>
        /// <param name="attt"></param>
        /// <param name="value"></param>
        /// <param name="Contain"></param>
        /// <returns></returns>

       
        public static List<HtmlNode> GetListTag(string html, string tag, string attt, string value, bool Contain = false)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            List<HtmlNode> list = new List<HtmlNode>();
            if (Contain)
            {
                list = htmlDoc.DocumentNode.Descendants(tag).Where(t => t.GetAttributeValue(attt, "").Contains(value)).ToList();
            }
            else
            {
                list = htmlDoc.DocumentNode.Descendants(tag).Where(t => t.GetAttributeValue(attt, "").ToString() == value).ToList();
            }
            return list;

        }
       /// <summary>
        /// Truyền vào node thì trả về list Node, nếu remove_text = true thì remove #text   
       /// </summary>
       /// <param name="itemNode"></param>
       /// <param name="remove_text"></param>
       /// <returns></returns>
        public static List<HtmlNode> GetListNode(HtmlNode itemNode, bool remove_text = false)
        {
            List<HtmlNode> list = new List<HtmlNode>();
            list.Add(itemNode);
            var listNode = Remove_comment(itemNode, remove_text);
            foreach (var item in listNode)
            {
                list[0].ChildNodes.Add(item);
            }
            return list;
        }
        /// <summary>
        /// Remove comment
        /// </summary>
        /// <param name="itemNode"></param>
        /// <param name="remove_text"></param>
        /// <returns></returns>
        private static List<HtmlNode> Remove_comment(HtmlNode itemNode, bool remove_text = false)
        {
            var list = itemNode.ChildNodes.ToList();
            if (remove_text)
            {
                list = list.Where(t => t.Name != "#text").ToList();
            }
            list = list.Where(t => t.Name != "#comment").ToList();
            foreach (var item in list)
            {
                var listNode = Remove_comment(item,remove_text);
                list[list.IndexOf(item)].ChildNodes.Clear();
                foreach (var item1 in listNode)
                {
                    list[list.IndexOf(item)].ChildNodes.Add(item1);
                }
            }
            itemNode.ChildNodes.Clear();
            return list;
        }
        /// <summary>
        /// GetValue Html
        /// </summary>
        /// <param name="node"></param>
        /// <param name="countChidNode"></param>
        /// <param name="attibutes"></param>
        /// <param name="replace"></param>
        /// <param name="valueCatch"></param>
        /// <returns></returns>
  
        public static string GetValueHtml(HtmlNode node, int[] countChidNode, string attibutes, Dictionary<string, string> replace = null, string valueCatch = null)
        {
            string value = "";
            try
            {
                // for vao chirdNode
                foreach (var item in countChidNode)
                {
                    node = node.ChildNodes[item];
                }
                //End

                value = !string.IsNullOrEmpty(attibutes) ? node.Attributes[attibutes].Value.ToString() : node.InnerText.ToString();
                if (string.IsNullOrEmpty(value))
                {
                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(node.OuterHtml);
                    value = htmlDoc.DocumentNode.InnerText;
                }
                // ReplaceValue nếu có
                if (replace != null)
                {
                    foreach (var item in replace)
                    {
                        value = value.Replace(item.Key, item.Value);
                    }
                }
            }
            catch
            {
                value = valueCatch;
            }


            //end 
            return WebUtility.HtmlDecode(value.Replace("\t", "").Replace("\n", "").Trim());
        }
      /// <summary>
        ///  Get Listnode theo countChirdNode  
      /// </summary>
      /// <param name="node"></param>
      /// <param name="countChidNode"></param>
      /// <param name="remove_text"></param>
      /// <returns></returns>
        public static List<HtmlNode> GetListNodeToNode(HtmlNode node, int[] countChidNode, bool remove_text = false)
        {
            // for vao chirdNode
            foreach (var item in countChidNode)
            {
                node = node.ChildNodes[item];
            }
            //end 
            return GetListNode(node, remove_text);
        }
        #endregion
    }
}
