using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.DTO.Sys
{
    /// <summary>
    /// 用户角色信息
    /// </summary>
    public class UserRoleDto
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
