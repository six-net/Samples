using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 删除操作分组信息
    /// </summary>
    public class RemoveOperationGroupPatameter
    {
        /// <summary>
        /// 要删除的操作分组编号
        /// </summary>
        public IEnumerable<long> Ids { get; set; }
    }
}
