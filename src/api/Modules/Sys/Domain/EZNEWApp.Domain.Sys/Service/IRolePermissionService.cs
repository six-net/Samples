using System;
using System.Collections.Generic;
using System.Text;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEW.Model;

namespace EZNEWApp.Domain.Sys.Service
{
    /// <summary>
    /// 角色授权服务
    /// </summary>
    public interface IRolePermissionService
    {
        #region 修改角色授权

        /// <summary>
        /// 修改角色授权
        /// </summary>
        /// <param name="roleAuthorizes">角色授权修改信息</param>
        /// <returns>返回执行结果</returns>
        Result Modify(ModifyRolePermissionParameter modifyRolePermission);

        #endregion

        #region 根据角色清除角色授权

        /// <summary>
        /// 根据角色清除角色授权
        /// </summary>
        /// <param name="roleIds">角色编号</param>
        /// <returns>返回执行结果</returns>
        Result ClearByRole(IEnumerable<long> roleIds);

        #endregion

        #region 根据权限清除角色授权

        /// <summary>
        /// 根据权限清除角色授权
        /// </summary>
        /// <param name="permissionIds">权限编号</param>
        /// <returns>返回执行结果</returns>
        Result ClearByPermission(IEnumerable<long> permissionIds);

        #endregion
    }
}
