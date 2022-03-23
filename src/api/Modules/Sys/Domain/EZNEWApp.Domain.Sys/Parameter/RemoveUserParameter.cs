using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 定义删除用户参数信息
    /// </summary>
    public class RemoveUserParameter
    {
        /// <summary>
        /// 要删除的用户编号
        /// </summary>
        public IEnumerable<long> Ids { get; set; }
    }
}
