using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Domain.Sys.Model;
using EZNEW.Develop.Domain;

namespace EZNEW.Domain.Sys.Parameter
{
    /// <summary>
    /// 修改用户授权
    /// </summary>
    public class ModifyUserPermissionParameter : IDomainParameter
    {
        /// <summary>
        /// 用户授权信息
        /// </summary>
        public IEnumerable<UserPermission> UserPermissions { get; set; }
    }
}
