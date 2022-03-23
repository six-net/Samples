using System.Collections.Generic;
using EZNEW.Development.Domain;
using EZNEWApp.Module.Sys;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 修改权限状态信息
    /// </summary>
    public class ModifyPermissionStatusParameter : IDomainParameter
    {
        /// <summary>
        /// 状态信息
        /// </summary>
        public Dictionary<long, PermissionStatus> StatusInfos { get; set; }
    }
}
