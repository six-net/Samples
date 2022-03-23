using EZNEW.Model;
using EZNEWApp.AppServiceContract.Sys;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Console.Controllers.Sys
{
    /// <summary>
    /// 菜单授权管理
    /// </summary>
    [Route("menu-permission")]
    public class MenuPermissionController : ApiBaseController
    {
        IMenuPermissionAppService menuPermissionAppService;

        public MenuPermissionController(IMenuPermissionAppService menuPermissionAppService)
        {
            this.menuPermissionAppService = menuPermissionAppService;
        }

        #region 添加菜单授权

        /// <summary>
        /// 添加菜单授权
        /// </summary>
        /// <param name="menuPermissions">菜单授权信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public Result AddMenuPermission(IEnumerable<MenuPermission> menuPermissions)
        {
            return menuPermissionAppService.Modify(new ModifyMenuPermissionParameter()
            {
                Bindings = menuPermissions
            });
        }

        #endregion

        #region 删除菜单授权

        /// <summary>
        /// 删除菜单授权
        /// </summary>
        /// <param name="menuPermissions">菜单授权信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public Result DeleteMenuPermission(IEnumerable<MenuPermission> menuPermissions)
        {
            return menuPermissionAppService.Modify(new ModifyMenuPermissionParameter()
            {
                Unbindings = menuPermissions
            });
        }

        #endregion

        #region 根据菜单清空菜单授权

        /// <summary>
        /// 清空菜单的授权
        /// </summary>
        /// <param name="menuIds">菜单编号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("clear-by-menu")]
        public Result ClearByMenu(IEnumerable<long> menuIds)
        {
            return menuPermissionAppService.ClearByMenu(menuIds);
        }

        #endregion

        #region 根据权限清空操作授权

        /// <summary>
        /// 清空权限的授权菜单
        /// </summary>
        /// <param name="permissionIds">权限编号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("clear-by-permission")]
        public Result ClearByPermission(IEnumerable<long> permissionIds)
        {
            return menuPermissionAppService.ClearByPermission(permissionIds);
        }

        #endregion
    }
}
