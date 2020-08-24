using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EZNEW.ViewModel.Common
{
    /// <summary>
    /// 树节点对象
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// 文本
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// 值
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// 展开
        /// </summary>
        [JsonProperty(PropertyName = "open")]
        public bool Open
        {
            get;
            set;
        }

        /// <summary>
        /// 子节点
        /// </summary>
        [JsonProperty(PropertyName = "children")]
        public List<TreeNode> Children
        {
            get;
            set;
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        [JsonProperty(PropertyName = "checked")]
        public bool Checked
        {
            get;
            set;
        }

        /// <summary>
        /// 图标
        /// </summary>
        [JsonProperty(PropertyName = "icon")]
        public string Icon
        {
            get; set;
        }

        /// <summary>
        /// 是否为父级
        /// </summary>
        [JsonProperty(PropertyName = "isParent")]
        public bool IsParent
        {
            get; set;
        }

        /// <summary>
        /// 是否加载数据
        /// </summary>
        [JsonProperty(PropertyName = "loadData")]
        public bool LoadData
        {
            get; set;
        }
    }
}
