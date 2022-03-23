using System.Collections.Generic;
using System.Linq;
using System;
using EZNEW.Model;
using EZNEW.DependencyInjection;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Model;
using EZNEW.Development.Domain.Repository;

namespace EZNEWApp.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 权限&授权操作绑定操作
    /// </summary>
    public class OperationPermissionService : IOperationPermissionService
    {
        readonly IRepository<OperationPermission> operationPermissionRepository;

        public OperationPermissionService(IRepository<OperationPermission> operationPermissionRepository)
        {
            this.operationPermissionRepository = operationPermissionRepository;
        }

        #region 修改权限&操作绑定

        /// <summary>
        /// 修改权限&操作绑定
        /// </summary>
        /// <param name="modifyPermissionOperation">权限授权操作修改信息</param>
        /// <returns></returns>
        public Result Modify(ModifyOperationPermissionParameter modifyPermissionOperation)
        {
            if (modifyPermissionOperation == null || (modifyPermissionOperation.Bindings.IsNullOrEmpty() && modifyPermissionOperation.Unbindings.IsNullOrEmpty()))
            {
                return Result.FailedResult("没有指定任何要修改的信息");
            }
            //解绑
            if (!modifyPermissionOperation.Unbindings.IsNullOrEmpty())
            {
                operationPermissionRepository.Remove(modifyPermissionOperation.Unbindings);
            }
            //绑定
            if (!modifyPermissionOperation.Bindings.IsNullOrEmpty())
            {
                operationPermissionRepository.Save(modifyPermissionOperation.Bindings);
            }
            return Result.SuccessResult("修改成功");
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
                return Result.FailedResult("没有指定权限");
            }
            operationPermissionRepository.RemoveByRelationData(permissionIds.Select(pid => new Permission { Id = pid }));
            return Result.SuccessResult("清除成功");
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
                return Result.FailedResult("没有指定操作功能");
            }
            operationPermissionRepository.RemoveByRelationData(operationIds.Select(oid => new Operation() { Id = oid }));
            return Result.SuccessResult("清除成功");
        }

        #endregion
    }
}
