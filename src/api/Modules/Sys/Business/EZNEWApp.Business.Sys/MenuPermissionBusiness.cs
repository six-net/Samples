using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Development.UnitOfWork;
using EZNEW.Model;
using EZNEWApp.BusinessContract.Sys;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Service;

namespace EZNEWApp.Business.Sys
{
    public class MenuPermissionBusiness : IMenuPermissionBusiness
    {

        IMenuPermissionService menuPermissionService;

        public MenuPermissionBusiness(IMenuPermissionService menuPermissionService)
        {
            this.menuPermissionService = menuPermissionService;
        }

        #region 清除菜单的所有权限

        /// <summary>
        /// 清除菜单的所有权限
        /// </summary>
        /// <param name="menuIds">菜单系统编号</param>
        /// <returns>执行结果</returns>
        public Result ClearByMenu(IEnumerable<long> menuIds)
        {
            if (menuIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何菜单信息");
            }
            using (var work = WorkManager.Create())
            {
                var result = menuPermissionService.ClearByMenu(menuIds);
                if (!result.Success)
                {
                    return result;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? result : Result.FailedResult("清空失败");
            }
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
            if (permissionIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何权限信息");
            }
            using (var work = WorkManager.Create())
            {
                var result = menuPermissionService.ClearByPermission(permissionIds);
                if (!result.Success)
                {
                    return result;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? result : Result.FailedResult("清空失败");
            }
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
            if (modifyMenuPermissionParameter is null)
            {
                throw new ArgumentNullException(nameof(modifyMenuPermissionParameter));
            }

            using (var work = WorkManager.Create())
            {
                var modifyResult = menuPermissionService.Modify(modifyMenuPermissionParameter);
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
        }

        #endregion
    }
}
