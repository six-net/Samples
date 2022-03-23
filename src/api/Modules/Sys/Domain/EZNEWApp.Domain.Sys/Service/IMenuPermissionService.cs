using EZNEW.Model;
using EZNEWApp.Domain.Sys.Parameter;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.Domain.Sys.Service
{
    /// <summary>
    /// 菜单权限服务
    /// </summary>
    public interface IMenuPermissionService
    {
        #region 清除菜单的所有权限

        /// <summary>
        /// 清除菜单的所有权限
        /// </summary>
        /// <param name="menuIds">菜单系统编号</param>
        /// <returns>执行结果</returns>
        Result ClearByMenu(IEnumerable<long> menuIds);

        #endregion

        #region 清除权限的所有菜单

        /// <summary>
        /// 清除权限的所有菜单
        /// </summary>
        /// <param name="permissionIds">权限系统编号</param>
        /// <returns>执行结果</returns>
        Result ClearByPermission(IEnumerable<long> permissionIds);

        #endregion

        #region 修改菜单&权限

        /// <summary>
        /// 修改菜单&权限
        /// </summary>
        /// <param name="modifyMenuPermissionParameter">菜单&权限修改参数</param>
        /// <returns>返回操作结果</returns>
        Result Modify(ModifyMenuPermissionParameter modifyMenuPermissionParameter);

        #endregion
    }
}
