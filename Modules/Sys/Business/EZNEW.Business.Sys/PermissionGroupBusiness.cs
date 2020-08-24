using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZNEW.BusinessContract.Sys;
using EZNEW.DependencyInjection;
using EZNEW.Develop.UnitOfWork;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Parameter.Filter;
using EZNEW.Domain.Sys.Service;
using EZNEW.Domain.Sys.Service.Impl;
using EZNEW.DTO.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Filter;
using EZNEW.Paging;
using EZNEW.Response;

namespace EZNEW.Business.Sys
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
        /// <param name="savePermissionGroupDto">权限分组保存信息</param>
        /// <returns>返回执行结果</returns>
        public Result<PermissionGroupDto> SavePermissionGroup(SavePermissionGroupDto savePermissionGroupDto)
        {
            if (savePermissionGroupDto?.PermissionGroup == null)
            {
                return Result<PermissionGroupDto>.FailedResult("分组信息不完整");
            }
            using (var businessWork = WorkManager.Create())
            {
                var saveResult = permissionGroupService.Save(savePermissionGroupDto.PermissionGroup.MapTo<PermissionGroup>());
                if (!saveResult.Success)
                {
                    return Result<PermissionGroupDto>.FailedResult(saveResult.Message);
                }
                var commitResult = businessWork.Commit();
                Result<PermissionGroupDto> result = null;
                if (commitResult.EmptyResultOrSuccess)
                {
                    result = Result<PermissionGroupDto>.SuccessResult("保存成功");
                    result.Data = saveResult.Data.MapTo<PermissionGroupDto>();
                }
                else
                {
                    result = Result<PermissionGroupDto>.FailedResult("保存失败");
                }
                return result;
            }
        }

        #endregion

        #region 获取权限分组

        /// <summary>
        /// 获取权限分组
        /// </summary>
        /// <param name="filter">权限分组筛选信息</param>
        /// <returns>返回权限分组</returns>
        public PermissionGroupDto GetPermissionGroup(PermissionGroupFilterDto filter)
        {
            var authorityGroup = permissionGroupService.Get(filter?.ConvertToFilter());
            return authorityGroup.MapTo<PermissionGroupDto>();
        }

        #endregion

        #region 获取权限分组列表

        /// <summary>
        /// 获取权限分组列表
        /// </summary>
        /// <param name="filter">权限分组筛选信息</param>
        /// <returns>返回权限分组列表</returns>
        public List<PermissionGroupDto> GetPermissionGroupList(PermissionGroupFilterDto filter)
        {
            var authorityGroupList = permissionGroupService.GetList(filter?.ConvertToFilter());
            return authorityGroupList.Select(c => c.MapTo<PermissionGroupDto>()).ToList();
        }

        #endregion

        #region 获取权限分组分页

        /// <summary>
        /// 获取权限分组分页
        /// </summary>
        /// <param name="filter">权限分组筛选信息</param>
        /// <returns>返回权限分组分页</returns>
        public IPaging<PermissionGroupDto> GetPermissionGroupPaging(PermissionGroupFilterDto filter)
        {
            var authorityGroupPaging = permissionGroupService.GetPaging(filter?.ConvertToFilter());
            return authorityGroupPaging.ConvertTo<PermissionGroupDto>();
        }

        #endregion

        #region 删除权限分组

        /// <summary>
        /// 删除权限分组
        /// </summary>
        /// <param name="removePermissionGroupDto">权限分组删除信息</param>
        /// <returns>返回执行结果</returns>
        public Result RemovePermissionGroup(RemovePermissionGroupDto removePermissionGroupDto)
        {
            using (var businessWork = WorkManager.Create())
            {
                #region 参数判断

                if (removePermissionGroupDto?.Ids.IsNullOrEmpty() ?? true)
                {
                    return Result.FailedResult("没有指定要删除的权限分组");
                }

                #endregion

                var result = permissionGroupService.Remove(removePermissionGroupDto.Ids);
                if (!result.Success)
                {
                    return result;
                }
                var exectVal = businessWork.Commit();
                return exectVal.ExecutedSuccess ? Result.SuccessResult("删除成功") : Result.FailedResult("删除失败");
            }
        }

        #endregion

        #region 修改分组排序

        /// <summary>
        /// 修改分组排序
        /// </summary>
        /// <param name="modifyPermissionGroupSortDto">权限分组排序修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyPermissionGroupSort(ModifyPermissionGroupSortDto modifyPermissionGroupSortDto)
        {
            #region 参数判断

            if (modifyPermissionGroupSortDto?.Id < 1)
            {
                return Result.FailedResult("没有指定要修改的分组");
            }

            #endregion

            using (var businessWork = WorkManager.Create())
            {
                #region 修改分组状态信息

                var modifyResult = permissionGroupService.ModifySort(modifyPermissionGroupSortDto.Id, modifyPermissionGroupSortDto.NewSort);
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }

                #endregion

                var executeVal = businessWork.Commit();
                return executeVal.ExecutedSuccess ? Result.SuccessResult("排序修改成功") : Result.FailedResult("排序修改失败");
            }
        }

        #endregion

        #region 验证权限分组名称是否存在

        /// <summary>
        /// 验证权限分组名称是否存在
        /// </summary>
        /// <param name="existPermissionGroupNameDto">权限分组名称验证信息</param>
        /// <returns>返回权限分组名称是否存在</returns>
        public bool ExistPermissionGroupName(ExistPermissionGroupNameDto existPermissionGroupNameDto)
        {
            if (existPermissionGroupNameDto == null)
            {
                return false;
            }
            return permissionGroupService.ExistName(existPermissionGroupNameDto.Name, existPermissionGroupNameDto.ExcludeId);
        }

        #endregion
    }
}
