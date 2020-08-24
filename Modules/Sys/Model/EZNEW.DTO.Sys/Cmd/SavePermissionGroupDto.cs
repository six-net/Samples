using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 权限分组编辑信息
    /// </summary>
    public class SavePermissionGroupDto
    {
        /// <summary>
        /// 分组信息
        /// </summary>
        public PermissionGroupDto PermissionGroup { get; set; }
    }
}
