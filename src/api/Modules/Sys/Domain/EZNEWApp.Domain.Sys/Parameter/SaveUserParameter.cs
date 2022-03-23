using System;
using System.Collections.Generic;
using System.Text;
using EZNEWApp.Domain.Sys.Model;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 定义保存用户参数信息
    /// </summary>
    public class SaveUserParameter
    {
        /// <summary>
        /// 保存用户
        /// </summary>
        public User User { get; set; }
    }
}
