using System;
using System.Collections.Generic;
using System.Text;
using EZNEWApp.Domain.Sys.Model;
using EZNEW.Development.Domain;

namespace EZNEWApp.Domain.Sys.Parameter
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
