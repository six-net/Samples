using System;
using System.Collections.Generic;
using EZNEW.Develop.Domain;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Parameter.Filter;

namespace EZNEW.Domain.Sys.Parameter
{
    /// <summary>
    /// 修改角色权限绑定
    /// </summary>
    public class ModifyRolePermission : IDomainParameter
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
