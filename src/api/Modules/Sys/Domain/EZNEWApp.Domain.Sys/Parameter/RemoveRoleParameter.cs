using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 定义删除角色参数信息
    /// </summary>
    public class RemoveRoleParameter
    {
        /// <summary>
        /// 要删除的角色编号
        /// </summary>
        public IEnumerable<long> Ids { get; set; }
    }
}
