using System.Collections.Generic;
using System.Linq;
using System;
using EZNEW.Response;
using EZNEW.Develop.UnitOfWork;
using EZNEW.Domain.Sys.Repository;
using EZNEW.DependencyInjection;
using EZNEW.Domain.Sys.Parameter;
using EZNEW.Domain.Sys.Model;

namespace EZNEW.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 权限&授权操作绑定操作
    /// </summary>
    public class PermissionOperationService : IPermissionOperationService
    {
        static readonly IPermissionOperationRepository permissionOperationRepository = ContainerManager.Resolve<IPermissionOperationRepository>();

        #region 修改权限&操作绑定

        /// <summary>
        /// 修改权限&操作绑定
        /// </summary>
        /// <param name="modifyPermissionOperation">权限授权操作修改信息</param>
        /// <returns></returns>
        public Result Modify(ModifyPermissionOperation modifyPermissionOperation)
        {
            if (modifyPermissionOperation == null || (modifyPermissionOperation.Bindings.IsNullOrEmpty() && modifyPermissionOperation.Unbindings.IsNullOrEmpty()))
            {
                return Result.FailedResult("没有指定任何要修改的信息");
            }
            //解绑
            if (!modifyPermissionOperation.Unbindings.IsNullOrEmpty())
            {
                permissionOperationRepository.Remove(modifyPermissionOperation.Unbindings.Select(c => new Tuple<Permission, Operation>(Permission.Create(c.PermissionId), Operation.Create(c.OperationId))));
            }
            //绑定
            if (!modifyPermissionOperation.Bindings.IsNullOrEmpty())
            {
                var bindingInfos = modifyPermissionOperation.Bindings.Select(c => new Tuple<Permission, Operation>(Permission.Create(c.PermissionId), Operation.Create(c.OperationId)));
                permissionOperationRepository.Remove(bindingInfos, new ActivationOption()
                {
                    ForceExecute = true
                });
                permissionOperationRepository.Save(bindingInfos);
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
            permissionOperationRepository.RemoveByFirst(permissionIds.Select(pid => Permission.Create(pid)));
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
            permissionOperationRepository.RemoveBySecond(operationIds.Select(oid => Operation.Create(oid)));
            return Result.SuccessResult("清除成功");
        }

        #endregion
    }
}
