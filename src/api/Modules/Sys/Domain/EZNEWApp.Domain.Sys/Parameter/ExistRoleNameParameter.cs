using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 验证角色名是否存在
    /// </summary>
    public class ExistRoleNameParameter
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 验证角色名称时需要排除的角色编号
        /// </summary>
        public long ExcludeId { get; set; }
    }
}
