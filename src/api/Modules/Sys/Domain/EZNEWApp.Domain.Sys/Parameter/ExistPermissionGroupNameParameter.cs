using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 验证权限分组名称是否存在
    /// </summary>
    public class ExistPermissionGroupNameParameter
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 验证分组名称时需要排除的分组编号
        /// </summary>
        public long ExcludeId { get; set; }
    }
}
