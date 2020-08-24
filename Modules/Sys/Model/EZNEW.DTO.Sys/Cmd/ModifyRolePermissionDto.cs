using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 修改角色&权限绑定信息
    /// </summary>
    public class ModifyRolePermissionDto
    {
        /// <summary>
        /// 绑定信息
        /// </summary>
        public IEnumerable<RolePermissionDto> Bindings { get; set; }

        /// <summary>
        /// 解绑信息
        /// </summary>
        public IEnumerable<RolePermissionDto> Unbindings { get; set; }
    }
}
