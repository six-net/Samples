using EZNEW.Develop.Domain;
using EZNEW.Module.Sys;
using System.Collections.Generic;

namespace EZNEW.Domain.Sys.Parameter
{
    /// <summary>
    /// 修改授权操作状态信息
    /// </summary>
    public class ModifyOperationStatus : IDomainParameter
    {
        /// <summary>
        /// 状态信息
        /// </summary>
        public Dictionary<long, OperationStatus> StatusInfos { get; set; }
    }
}
