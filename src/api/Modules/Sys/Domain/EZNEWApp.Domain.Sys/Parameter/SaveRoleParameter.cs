using EZNEWApp.Domain.Sys.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 保存角色参数
    /// </summary>
    public class SaveRoleParameter
    {
        /// <summary>
        /// 角色信息
        /// </summary>
        public Role Role { get; set; }
    }
}
