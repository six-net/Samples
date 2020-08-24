using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.Domain.Sys.Model
{
    /// <summary>
    /// 用户&角色绑定信息
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public long RoleId { get; set; }
    }
}
