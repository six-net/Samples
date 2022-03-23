using EZNEW.Development.Domain;
using EZNEWApp.Module.Sys;
using System.Collections.Generic;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 修改授权操作状态信息
    /// </summary>
    public class ModifyOperationStatusParameter : IDomainParameter
    {
        /// <summary>
        /// 状态信息
        /// </summary>
        public Dictionary<long, OperationStatus> StatusInfos { get; set; }
    }
}
