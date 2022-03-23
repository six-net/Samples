using System;
using System.Collections.Generic;
using System.Text;
using EZNEWApp.BusinessContract.Sys;
using EZNEW.DependencyInjection;
using EZNEW.Development.UnitOfWork;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Service;
using EZNEW.Model;

namespace EZNEWApp.Business.Sys
{
    /// <summary>
    /// 角色授权业务逻辑
    /// </summary>
    public class RolePermissionBusiness : IRolePermissionBusiness
    {
        readonly IRolePermissionService rolePermissionService = null;

        public RolePermissionBusiness(IRolePermissionService rolePermissionService)
        {
            this.rolePermissionService = rolePermissionService;
        }

        #region 修改角色授权

        /// <summary>
        /// 修改角色授权
        /// </summary>
        /// <param name="modifyRolePermissionParameter">角色授权修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result Modify(ModifyRolePermissionParameter modifyRolePermissionParameter)
        {
            if (modifyRolePermissionParameter is null)
            {
                throw new ArgumentNullException(nameof(modifyRolePermissionParameter));
            }

            using (var work = WorkManager.Create())
            {
                var modifyResult = rolePermissionService.Modify(modifyRolePermissionParameter);
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
        }

        #endregion

        #region 根据角色清空角色授权信息

        /// <summary>
        /// 根据角色清空角色授权信息
        /// </summary>
        /// <param name="roleIds">角色系统编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearByRole(IEnumerable<long> roleIds)
        {
            if (roleIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何角色信息");
            }
            using (var work = WorkManager.Create())
            {
                var result = rolePermissionService.ClearByRole(roleIds);
                if (!result.Success)
                {
                    return result;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? result : Result.FailedResult("清空失败");
            }
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
            if (permissionIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何权限信息");
            }
            using (var work = WorkManager.Create())
            {
                var result = rolePermissionService.ClearByPermission(permissionIds);
                if (!result.Success)
                {
                    return result;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? result : Result.FailedResult("清空失败");
            }
        }

        #endregion
    }
}
