using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 删除权限分组信息
    /// </summary>
    public class RemovePermissionGroupParameter
    {
        /// <summary>
        /// 要删除的权限分组编号
        /// </summary>
        public IEnumerable<long> Ids { get; set; }
    }
}
