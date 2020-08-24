using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 验证权限名称
    /// </summary>
    public class ExistPermissionNameDto
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 验证权限名称时需要排除的权限编号
        /// </summary>
        public long ExcludeId { get; set; }
    }
}
