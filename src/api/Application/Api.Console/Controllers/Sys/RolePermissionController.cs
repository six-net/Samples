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
    /// 角色授权管理
    /// </summary>
    [Route("role-permission")]
    public class RolePermissionController : ApiBaseController
    {
        IRolePermissionAppService rolePermissionAppService;

        public RolePermissionController(IRolePermissionAppService rolePermissionAppService)
        {
            this.rolePermissionAppService = rolePermissionAppService;
        }

        #region 添加角色授权

        /// <summary>
        /// 添加角色授权
        /// </summary>
        /// <param name="rolePermissions">角色授权信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public Result AddRolePermission(IEnumerable<RolePermission> rolePermissions)
        {
            return rolePermissionAppService.Modify(new ModifyRolePermissionParameter()
            {
                Bindings = rolePermissions
            });
        }

        #endregion

        #region 删除角色授权

        /// <summary>
        /// 删除角色授权
        /// </summary>
        /// <param name="rolePermissions">角色授权信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public Result DeleteRolePermission(IEnumerable<RolePermission> rolePermissions)
        {
            return rolePermissionAppService.Modify(new ModifyRolePermissionParameter()
            {
                Unbindings = rolePermissions
            });
        }

        #endregion

        #region 根据角色清空角色授权

        /// <summary>
        /// 清空角色所有授权
        /// </summary>
        /// <param name="roleIds">角色编号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("clear-by-role")]
        public Result ClearByRole(IEnumerable<long> roleIds)
        {
            return rolePermissionAppService.ClearByRole(roleIds);
        }

        #endregion

        #region 根据权限清空角色授权

        /// <summary>
        /// 清空权限授权的角色
        /// </summary>
        /// <param name="permissionIds">权限编号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("clear-by-permission")]
        public Result ClearByPermission(IEnumerable<long> permissionIds)
        {
            return rolePermissionAppService.ClearByPermission(permissionIds);
        }

        #endregion
    }
}
