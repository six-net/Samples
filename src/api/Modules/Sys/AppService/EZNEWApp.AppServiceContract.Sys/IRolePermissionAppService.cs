using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Parameter;

namespace EZNEWApp.AppServiceContract.Sys
{
    /// <summary>
    /// 角色授权业务逻辑
    /// </summary>
    public interface IRolePermissionAppService
    {
        #region 修改角色授权

        /// <summary>
        /// 修改角色授权
        /// </summary>
        /// <param name="modifyRolePermissionParameter">角色授权修改信息</param>
        /// <returns>返回执行结果</returns>
        Result Modify(ModifyRolePermissionParameter modifyRolePermissionParameter);

        #endregion

        #region 根据角色清空角色授权信息

        /// <summary>
        /// 根据角色清空角色授权信息
        /// </summary>
        /// <param name="roleIds">角色系统编号</param>
        /// <returns>返回执行结果</returns>
        Result ClearByRole(IEnumerable<long> roleIds);

        #endregion

        #region 根据权限清空角色授权信息

        /// <summary>
        /// 根据权限清空角色授权信息
        /// </summary>
        /// <param name="permissionIds">权限系统编号</param>
        /// <returns>返回执行结果</returns>
        Result ClearByPermission(IEnumerable<long> permissionIds);

        #endregion
    }
}
