using EZNEWApp.Domain.Sys.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 保存权限分组信息
    /// </summary>
    public class SavePermissionGroupParameter
    {
        /// <summary>
        /// 分组信息
        /// </summary>
        public PermissionGroup PermissionGroup { get; set; }
    }
}
