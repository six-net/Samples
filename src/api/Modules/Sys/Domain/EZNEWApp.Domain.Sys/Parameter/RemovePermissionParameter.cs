using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 删除权限信息
    /// </summary>
    public class RemovePermissionParameter
    {
        /// <summary>
        /// 要删除的权限编号
        /// </summary>
        public IEnumerable<long> Ids { get; set; }
    }
}
