using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 修改用户授权信息
    /// </summary>
    public class ModifyUserPermissionDto
    {
        /// <summary>
        /// 用户授权信息
        /// </summary>
        public IEnumerable<UserPermissionDto> UserPermissions { get; set; }
    }
}
