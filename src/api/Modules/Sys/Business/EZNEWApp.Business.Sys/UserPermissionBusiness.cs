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
    /// 用户授权业务逻辑
    /// </summary>
    public class UserPermissionBusiness : IUserPermissionBusiness
    {
        readonly IUserPermissionService userPermissionService =null;

        public UserPermissionBusiness(IUserPermissionService userPermissionService)
        {
            this.userPermissionService = userPermissionService;
        }

        #region 修改用户授权

        /// <summary>
        /// 修改用户授权
        /// </summary>
        /// <param name="modifyUserPermissionParameter">用户授权修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result Modify(ModifyUserPermissionParameter modifyUserPermissionParameter)
        {
            if (modifyUserPermissionParameter is null)
            {
                throw new ArgumentNullException(nameof(modifyUserPermissionParameter));
            }

            using (var work = WorkManager.Create())
            {
                var modifyResult = userPermissionService.Modify(modifyUserPermissionParameter);
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
        }

        #endregion

        #region 根据用户清空用户授权信息

        /// <summary>
        /// 根据用户清空用户授权信息
        /// </summary>
        /// <param name="userIds">用户系统编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearByUser(IEnumerable<long> userIds)
        {
            if (userIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何用户信息");
            }
            using (var work = WorkManager.Create())
            {
                var result = userPermissionService.ClearByUser(userIds);
                if (!result.Success)
                {
                    return result;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? result : Result.FailedResult("清空失败");
            }
        }

        #endregion

        #region 根据权限清空用户授权信息

        /// <summary>
        /// 根据权限清空用户授权信息
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
                var result = userPermissionService.ClearByPermission(permissionIds);
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
