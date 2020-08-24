using System.Collections.Generic;
using EZNEW.Module.Sys;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 修改权限状态信息
    /// </summary>
    public class ModifyPermissionStatusDto
    {
        /// <summary>
        /// 权限状态信息
        /// </summary>
        public Dictionary<long, PermissionStatus> StatusInfos { get; set; }
    }
}
