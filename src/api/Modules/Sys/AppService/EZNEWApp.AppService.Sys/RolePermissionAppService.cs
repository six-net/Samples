using System;
using System.Collections.Generic;
using System.Text;
using EZNEWApp.AppServiceContract.Sys;
using EZNEW.DependencyInjection;
using EZNEW.Development.UnitOfWork;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Service;
using EZNEW.Model;
using EZNEWApp.BusinessContract.Sys;

namespace EZNEWApp.AppService.Sys
{
    /// <summary>
    /// 角色授权业务逻辑
    /// </summary>
    public class RolePermissionAppService : IRolePermissionAppService
    {
        IRolePermissionBusiness rolePermissionBusiness;

        public RolePermissionAppService(IRolePermissionBusiness rolePermissionBusiness)
        {
            this.rolePermissionBusiness = rolePermissionBusiness;
        }

        #region 根据角色清空角色授权信息

        /// <summary>
        /// 根据角色清空角色授权信息
        /// </summary>
        /// <param name="roleIds">角色系统编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearByRole(IEnumerable<long> roleIds)
        {
            return rolePermissionBusiness.ClearByRole(roleIds);
        }

        #endregion

        #region 根据权限清空角色授权信息

        /// <summary>
        /// 根据权限清空角色授权信息
        /// </summary>
        /// <param name="permissionIds">权限系统编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearByPermission(IEnumerable<long> permissionIds)
        {
            return rolePermissionBusiness.ClearByPermission(permissionIds);
        }

        #endregion

        #region 修改角色授权

        /// <summary>
        /// 修改角色授权
        /// </summary>
        /// <param name="modifyRolePermissionParameter">角色授权修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result Modify(ModifyRolePermissionParameter modifyRolePermissionParameter)
        {
            return rolePermissionBusiness.Modify(modifyRolePermissionParameter);
        }

        #endregion
    }
}
