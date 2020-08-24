using System.Collections.Generic;
using EZNEW.Develop.Domain;
using EZNEW.Module.Sys;

namespace EZNEW.Domain.Sys.Parameter
{
    /// <summary>
    /// 修改权限状态信息
    /// </summary>
    public class ModifyPermissionStatus : IDomainParameter
    {
        /// <summary>
        /// 状态信息
        /// </summary>
        public Dictionary<long, PermissionStatus> StatusInfos { get; set; }
    }
}
