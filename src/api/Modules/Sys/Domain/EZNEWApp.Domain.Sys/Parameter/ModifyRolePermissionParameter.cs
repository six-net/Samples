using System;
using System.Collections.Generic;
using EZNEW.Development.Domain;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter.Filter;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 修改角色权限绑定
    /// </summary>
    public class ModifyRolePermissionParameter : IDomainParameter
    {
        /// <summary>
        /// 绑定信息
        /// </summary>
        public IEnumerable<RolePermission> Bindings { get; set; }

        /// <summary>
        /// 解绑信息
        /// </summary>
        public IEnumerable<RolePermission> Unbindings { get; set; }
    }
}
