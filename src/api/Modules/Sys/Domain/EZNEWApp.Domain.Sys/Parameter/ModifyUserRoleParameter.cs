using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Development.Domain;
using EZNEWApp.Domain.Sys.Model;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 修改用户&角色绑定关系
    /// </summary>
    public class ModifyUserRoleParameter : IDomainParameter
    {
        /// <summary>
        /// 绑定信息
        /// </summary>
        public IEnumerable<UserRole> Bindings { get; set; }

        /// <summary>
        /// 解绑信息
        /// </summary>
        public IEnumerable<UserRole> Unbindings { get; set; }
    }
}
