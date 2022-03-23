using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZNEWApp.BusinessContract.Sys;
using EZNEW.DependencyInjection;
using EZNEW.Development.UnitOfWork;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter.Filter;
using EZNEWApp.Domain.Sys.Service;
using EZNEWApp.Domain.Sys.Service.Impl;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Parameter;

namespace EZNEWApp.Business.Sys
{
    /// <summary>
    /// 权限分组业务逻辑
    /// </summary>
    public class PermissionGroupBusiness : IPermissionGroupBusiness
    {
        static readonly IPermissionGroupService permissionGroupService = ContainerManager.Resolve<IPermissionGroupService>();

        #region 保存权限分组

        /// <summary>
        /// 保存权限分组
        /// </summary>
        /// <param name="savePermissionGroupParameter">权限分组保存信息</param>
        /// <returns>返回执行结果</returns>
        public Result<PermissionGroup> SavePermissionGroup(SavePermissionGroupParameter savePermissionGroupParameter)
        {
            if (savePermissionGroupParameter is null)
            {
                throw new ArgumentNullException(nameof(savePermissionGroupParameter));
            }

            using (var work = WorkManager.Create())
            {
                var saveResult = permissionGroupService.Save(savePermissionGroupParameter.PermissionGroup);
                if (!saveResult.Success)
                {
                    return Result<PermissionGroup>.FailedResult(saveResult.Message);
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? saveResult : Result<PermissionGroup>.FailedResult("保存失败");
            }
        }

        #endregion

        #region 获取权限分组

        /// <summary>
        /// 获取权限分组
        /// </summary>
        /// <param name="filter">权限分组筛选信息</param>
        /// <returns>返回权限分组</returns>
        public PermissionGroup GetPermissionGroup(PermissionGroupFilter filter)
        {
            return permissionGroupService.Get(filter);
        }

        #endregion

        #region 获取权限分组列表

        /// <summary>
        /// 获取权限分组列表
        /// </summary>
        /// <param name="filter">权限分组筛选信息</param>
        /// <returns>返回权限分组列表</returns>
        public List<PermissionGroup> GetPermissionGroupList(PermissionGroupFilter filter)
        {
            return  permissionGroupService.GetList(filter);
        }

        #endregion

        #region 获取权限分组分页

        /// <summary>
        /// 获取权限分组分页
        /// </summary>
        /// <param name="filter">权限分组筛选信息</param>
        /// <returns>返回权限分组分页</returns>
        public PagingInfo<PermissionGroup> GetPermissionGroupPaging(PermissionGroupFilter filter)
        {
            return permissionGroupService.GetPaging(filter);
        }

        #endregion

        #region 删除权限分组

        /// <summary>
        /// 删除权限分组
        /// </summary>
        /// <param name="removePermissionGroupParameter">权限分组删除信息</param>
        /// <returns>返回执行结果</returns>
        public Result RemovePermissionGroup(RemovePermissionGroupParameter removePermissionGroupParameter)
        {
            if (removePermissionGroupParameter is null)
            {
                throw new ArgumentNullException(nameof(removePermissionGroupParameter));
            }

            using (var work = WorkManager.Create())
            {
                var removeResult = permissionGroupService.Remove(removePermissionGroupParameter.Ids);
                if (!removeResult.Success)
                {
                    return removeResult;
                }
                var commitResult = work.Commit();
                return commitResult.Success ? Result.SuccessResult("删除成功") : Result.FailedResult("删除失败");
            }
        }

        #endregion

        #region 修改分组排序

        /// <summary>
        /// 修改分组排序
        /// </summary>
        /// <param name="modifyPermissionGroupSortParameter">权限分组排序修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyPermissionGroupSort(ModifyPermissionGroupSortParameter modifyPermissionGroupSortParameter)
        {
            if (modifyPermissionGroupSortParameter is null)
            {
                throw new ArgumentNullException(nameof(modifyPermissionGroupSortParameter));
            }

            using (var work = WorkManager.Create())
            {
                var modifyResult = permissionGroupService.ModifySort(modifyPermissionGroupSortParameter.Id, modifyPermissionGroupSortParameter.NewSort);
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }

                var commitResult = work.Commit();
                return commitResult.Success ? Result.SuccessResult("排序修改成功") : Result.FailedResult("排序修改失败");
            }
        }

        #endregion

        #region 验证权限分组名称是否存在

        /// <summary>
        /// 验证权限分组名称是否存在
        /// </summary>
        /// <param name="existPermissionGroupNameParameter">权限分组名称验证信息</param>
        /// <returns>返回权限分组名称是否存在</returns>
        public bool ExistPermissionGroupName(ExistPermissionGroupNameParameter existPermissionGroupNameParameter)
        {
            if (existPermissionGroupNameParameter == null)
            {
                return false;
            }
            return permissionGroupService.ExistName(existPermissionGroupNameParameter.Name, existPermissionGroupNameParameter.ExcludeId);
        }

        #endregion
    }
}
