using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Model;
using EZNEWApp.AppServiceContract.Sys;
using EZNEWApp.BusinessContract.Sys;
using EZNEWApp.Domain.Sys.Parameter;

namespace EZNEWApp.AppService.Sys
{
    public class MenuPermissionAppService : IMenuPermissionAppService
    {
        IMenuPermissionBusiness menuPermissionBusiness;

        public MenuPermissionAppService(IMenuPermissionBusiness menuPermissionBusiness)
        {
            this.menuPermissionBusiness = menuPermissionBusiness;
        }

        #region 清除菜单的所有权限

        /// <summary>
        /// 清除菜单的所有权限
        /// </summary>
        /// <param name="menuIds">菜单系统编号</param>
        /// <returns>执行结果</returns>
        public Result ClearByMenu(IEnumerable<long> menuIds)
        {
            return menuPermissionBusiness.ClearByMenu(menuIds);
        }

        #endregion

        #region 清除权限的所有菜单

        /// <summary>
        /// 清除权限的所有菜单
        /// </summary>
        /// <param name="permissionIds">权限系统编号</param>
        /// <returns>执行结果</returns>
        public Result ClearByPermission(IEnumerable<long> permissionIds)
        {
            return menuPermissionBusiness.ClearByPermission(permissionIds);
        }

        #endregion

        #region 修改菜单&权限

        /// <summary>
        /// 修改菜单&权限
        /// </summary>
        /// <param name="modifyMenuPermissionParameter">菜单&权限修改参数</param>
        /// <returns>返回操作结果</returns>
        public Result Modify(ModifyMenuPermissionParameter modifyMenuPermissionParameter)
        {
            return menuPermissionBusiness.Modify(modifyMenuPermissionParameter);
        }

        #endregion
    }
}
