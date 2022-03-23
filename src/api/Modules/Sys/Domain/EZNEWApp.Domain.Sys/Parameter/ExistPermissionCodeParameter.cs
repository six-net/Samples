using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 验证权限编码是否存在
    /// </summary>
    public class ExistPermissionCodeParameter
    {
        /// <summary>
        /// 验证权限编码时需要排除的权限Id
        /// </summary>
        public long ExcludeId { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        public string Code { get; set; }
    }
}
