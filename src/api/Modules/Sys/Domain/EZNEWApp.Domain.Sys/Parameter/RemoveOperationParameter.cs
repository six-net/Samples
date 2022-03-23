using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 授权操作删除信息
    /// </summary>
    public class RemoveOperationParameter
    {
        /// <summary>
        /// 要删除的授权操作编号
        /// </summary>
        public IEnumerable<long> Ids { get; set; }
    }
}
