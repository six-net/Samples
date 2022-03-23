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
    /// <summary>
    /// 功能&权限业务实现
    /// </summary>
    public class OperationPermissionBusiness : IOperationPermissionBusiness
    {
        IOperationPermissionService operationPermissionService;

        public OperationPermissionBusiness(IOperationPermissionService operationPermissionService)
        {
            this.operationPermissionService = operationPermissionService;
        }

        #region 修改权限&操作授权

        /// <summary>
        /// 修改权限&操作绑定
        /// </summary>
        /// <param name="modifyPermissionOperation">权限操作修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result Modify(ModifyOperationPermissionParameter modifyOperationPermissionParameter)
        {
            if (modifyOperationPermissionParameter is null)
            {
                throw new ArgumentNullException(nameof(modifyOperationPermissionParameter));
            }

            using (var work = WorkManager.Create())
            {
                var modifyResult = operationPermissionService.Modify(modifyOperationPermissionParameter);
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var commitResult = work.Commit();
                return commitResult.Success ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
        }

        #endregion

        #region 清除权限绑定的所有操作

        /// <summary>
        /// 清除权限绑定的所有操作
        /// </summary>
        /// <param name="permissionIds">权限编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearByPermission(IEnumerable<long> permissionIds)
        {
            if (permissionIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何权限信息");
            }
            using (var work = WorkManager.Create())
            {
                var result = operationPermissionService.ClearByPermission(permissionIds);
                if (!result.Success)
                {
                    return result;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? result : Result.FailedResult("清空失败");
            }
        }

        #endregion

        #region 清除操作功能授权的所有权限

        /// <summary>
        /// 清除操作功能授权的所有权限
        /// </summary>
        /// <param name="operationIds">操作编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearByOperation(IEnumerable<long> operationIds)
        {
            if (operationIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何操作功能信息");
            }
            using (var work = WorkManager.Create())
            {
                var result = operationPermissionService.ClearByOperation(operationIds);
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
