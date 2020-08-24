using System.Collections.Generic;
using EZNEW.Module.Sys;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 修改用户状态
    /// </summary>
    public class ModifyUserStatusDto
    {
        /// <summary>
        /// 用户状态信息
        /// </summary>
        public Dictionary<long, UserStatus> StatusInfos { get; set; }
    }
}
