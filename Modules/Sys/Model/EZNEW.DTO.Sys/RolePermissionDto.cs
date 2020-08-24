using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.DTO.Sys
{
    /// <summary>
    /// 角色授权信息
    /// </summary>
    public class RolePermissionDto
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 权限编号
        /// </summary>
        public long PermissionId { get; set; }
    }
}
