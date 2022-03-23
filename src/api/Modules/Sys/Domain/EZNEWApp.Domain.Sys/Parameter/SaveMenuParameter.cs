using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEWApp.Domain.Sys.Model;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 保存菜单信息
    /// </summary>
    public class SaveMenuParameter
    {
        /// <summary>
        /// 菜单
        /// </summary>
        public Menu Menu { get; set; }
    }
}