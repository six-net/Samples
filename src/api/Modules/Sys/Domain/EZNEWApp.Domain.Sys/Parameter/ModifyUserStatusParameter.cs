using System.Collections.Generic;
using EZNEW.Development.Domain;
using EZNEWApp.Module.Sys;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 修改用户状态信息
    /// </summary>
    public class ModifyUserStatusParameter : IDomainParameter
    {
        /// <summary>
        /// 用户状态信息
        /// 键：用户编号
        /// 值：用户状态
        /// </summary>
        public Dictionary<long, UserStatus> StatusInfos { get; set; }
    }
}
