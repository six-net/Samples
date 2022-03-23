using System;
using System.Collections.Generic;
using System.Text;
using EZNEWApp.Domain.Sys.Model;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 修改菜单权限参数
    /// </summary>
    public class ModifyMenuPermissionParameter
    {
        /// <summary>
        /// 绑定信息
        /// </summary>
        public IEnumerable<MenuPermission> Bindings { get; set; }

        /// <summary>
        /// 解绑信息
        /// </summary>
        public IEnumerable<MenuPermission> Unbindings { get; set; }
    }
}
