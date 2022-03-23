using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 检查授权操作分组名称
    /// </summary>
    public class ExistOperationGroupNameParameter
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排除验证的分组编号
        /// </summary>
        public long ExcludeId { get; set; }
    }
}
