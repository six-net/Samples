using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.Domain.Sys.Model
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public class RolePermission
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
