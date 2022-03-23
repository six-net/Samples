using System;
using System.Collections.Generic;
using System.Linq;
using EZNEWApp.BusinessContract.Sys;
using EZNEW.DependencyInjection;
using EZNEW.Development.UnitOfWork;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Service;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Parameter.Filter;

namespace EZNEWApp.Business.Sys
{
    /// <summary>
    /// 权限业务逻辑操作
    /// </summary>
    public class PermissionBusiness : IPermissionBusiness
    {
        static readonly IPermissionService permissionService = ContainerManager.Resolve<IPermissionService>();
        static readonly IOperationPermissionService permissionOperationService = ContainerManager.Resolve<IOperationPermissionService>();

        #region 保存权限

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="savePermissionParameter">权限保存信息</param>
        /// <returns>返回执行结果</returns>
        public Result<Permission> SavePermission(SavePermissionParameter savePermissionParameter)
        {
            if (savePermissionParameter is null)
            {
                throw new ArgumentNullException(nameof(savePermissionParameter));
            }

            using (var work = WorkManager.Create())
            {
                if (savePermissionParameter == null)
                {
                    return Result<Permission>.FailedResult("没有指定任何要保存的权限信息");
                }
                var saveResult = permissionService.Save(savePermissionParameter.Permission);
                if (!saveResult.Success)
                {
                    return saveResult;
                }

                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? saveResult : Result<Permission>.FailedResult("保存失败");
            }
        }

        #endregion

        #region 获取权限

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="filter">权限筛选信息</param>
        /// <returns>返回权限</returns>
        public Permission GetPermission(PermissionFilter filter)
        {
            return permissionService.Get(filter);
        }

        #endregion

        #region 获取权限列表

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="filter">权限筛选信息</param>
        /// <returns>返回权限列表</returns>
        public List<Permission> GetPermissionList(PermissionFilter filter)
        {
            return permissionService.GetList(filter);
        }

        #endregion

        #region 获取权限分页

        /// <summary>
        /// 获取权限分页
        /// </summary>
        /// <param name="filter">权限筛选信息</param>
        /// <returns>返回权限分页</returns>
        public PagingInfo<Permission> GetPermissionPaging(PermissionFilter filter)
        {
            return permissionService.GetPaging(filter);
        }

        #endregion

        #region 删除权限

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="removePermissionParameter">权限删除信息</param>
        /// <returns>返回执行结果</returns>
        public Result RemovePermission(RemovePermissionParameter removePermissionParameter)
        {
            if (removePermissionParameter is null)
            {
                throw new ArgumentNullException(nameof(removePermissionParameter));
            }

            using (var work = WorkManager.Create())
            {
                permissionService.Remove(removePermissionParameter.Ids);

                var commitResult = work.Commit();
                return commitResult.Success ? Result.SuccessResult("删除成功") : Result.FailedResult("删除失败");
            }
        }

        #endregion

        #region 修改权限状态

        /// <summary>
        /// 修改权限状态
        /// </summary>
        /// <param name="modifyPermissionStatusParameter">权限状态修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyPermissionStatus(ModifyPermissionStatusParameter modifyPermissionStatusParameter)
        {
            using (var work = WorkManager.Create())
            {
                if (modifyPermissionStatusParameter?.StatusInfos.IsNullOrEmpty() ?? true)
                {
                    return Result.FailedResult("没有指定任何要修改的权限信息");
                }
                var modifyResult = permissionService.ModifyStatus(modifyPermissionStatusParameter);
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var commitResult = work.Commit();
                return commitResult.Success ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
        }

        #endregion

        #region 检查权限编码是否存在

        /// <summary>
        /// 检查权限编码是否存在
        /// </summary>
        /// <param name="existPermissionCodeParameter">权限编码检查信息</param>
        /// <returns>返回权限编码是否存在</returns>
        public bool ExistPermissionCode(ExistPermissionCodeParameter existPermissionCodeParameter)
        {
            if (existPermissionCodeParameter == null)
            {
                return false;
            }
            return permissionService.ExistCode(existPermissionCodeParameter.Code, existPermissionCodeParameter.ExcludeId);
        }

        #endregion

        #region 检查权限名称是否存在

        /// <summary>
        /// 检查权限名称是否存在
        /// </summary>
        /// <param name="existPermissionNameParameter">权限名称检查信息</param>
        /// <returns>返回权限名称是否存在</returns>
        public bool ExistPermissionName(ExistPermissionNameParameter existPermissionNameParameter)
        {
            if (existPermissionNameParameter == null)
            {
                return false;
            }
            return permissionService.ExistName(existPermissionNameParameter.Name, existPermissionNameParameter.ExcludeId);
        }

        #endregion

        #region 修改权限授权的操作功能

        /// <summary>
        /// 修改权限授权的操作功能
        /// </summary>
        /// <param name="modifyPermissionOperationParameter">权限授权操作修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyPermissionOperation(ModifyOperationPermissionParameter modifyPermissionOperationParameter)
        {
            if (modifyPermissionOperationParameter is null)
            {
                throw new ArgumentNullException(nameof(modifyPermissionOperationParameter));
            }

            using (var work = WorkManager.Create())
            {
                var modifyResult = permissionOperationService.Modify(modifyPermissionOperationParameter);
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var commitResult = work.Commit();
                return commitResult.Success ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
        }

        #endregion

        #region 清除权限授权的操作功能

        /// <summary>
        /// 清除权限授权的操作功能
        /// </summary>
        /// <param name="permissionIds">权限编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearOperation(IEnumerable<long> permissionIds)
        {
            if (permissionIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定权限");
            }

            using (var work = WorkManager.Create())
            {
                var clearResult = permissionOperationService.ClearByPermission(permissionIds);
                if (!clearResult.Success)
                {
                    return clearResult;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? Result.SuccessResult("清除成功") : Result.FailedResult("清除失败");
            }
        }

        #endregion
    }
}
