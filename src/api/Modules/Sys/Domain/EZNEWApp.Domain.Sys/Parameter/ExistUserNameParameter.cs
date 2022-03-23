using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 验证用户名参数
    /// </summary>
    public class ExistUserNameParameter
    {
        /// <summary>
        /// 要排除的用户编号
        /// </summary>
        public long? ExcludeId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
    }
}
