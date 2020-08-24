using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 修改权限授权的操作
    /// </summary>
    public class ModifyPermissionOperationDto
    {
        /// <summary>
        /// 绑定信息
        /// </summary>
        public IEnumerable<PermissionOperationDto> Bindings { get; set; }

        /// <summary>
        /// 解绑信息
        /// </summary>
        public IEnumerable<PermissionOperationDto> Unbindings { get; set; }
    }
}
