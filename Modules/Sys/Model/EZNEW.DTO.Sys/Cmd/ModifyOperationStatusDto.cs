using System.Collections.Generic;
using EZNEW.Module.Sys;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 修改授权操作状态信息
    /// </summary>
    public class ModifyOperationStatusDto
    {
        /// <summary>
        /// 状态信息
        /// </summary>
        public Dictionary<long, OperationStatus> StatusInfos { get; set; }
    }
}
