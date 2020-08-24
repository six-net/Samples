using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.DTO.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Filter;
using EZNEW.Paging;
using EZNEW.Response;

namespace EZNEW.AppServiceContract.Sys
{
    /// <summary>
    /// 权限分组应用服务
    /// </summary>
    public interface IPermissionGroupAppService
    {
        #region 保存权限分组

        /// <summary>
        /// 保存权限分组
        /// </summary>
        /// <param name="savePermissionGroupDto">权限分组保存信息</param>
        /// <returns>返回执行结果</returns>
        Result<PermissionGroupDto> SavePermissionGroup(SavePermissionGroupDto savePermissionGroupDto);

        #endregion

        #region 获取权限分组

        /// <summary>
        /// 获取权限分组
        /// </summary>
        /// <param name="filter">权限分组筛选信息</param>
        /// <returns>返回权限分组</returns>
        PermissionGroupDto GetPermissionGroup(PermissionGroupFilterDto filter);

        #endregion

        #region 获取权限分组列表

        /// <summary>
        /// 获取权限分组列表
        /// </summary>
        /// <param name="filter">权限分组筛选信息</param>
        /// <returns>返回权限分组列表</returns>
        List<PermissionGroupDto> GetPermissionGroupList(PermissionGroupFilterDto filter);

        #endregion

        #region 获取权限分组分页

        /// <summary>
        /// 获取权限分组分页
        /// </summary>
        /// <param name="filter">权限分组筛选信息</param>
        /// <returns>返回权限分组分页</returns>
        IPaging<PermissionGroupDto> GetPermissionGroupPaging(PermissionGroupFilterDto filter);

        #endregion

        #region 删除权限分组

        /// <summary>
        /// 删除权限分组
        /// </summary>
        /// <param name="removePermissionGroupDto">权限分组删除信息</param>
        /// <returns>返回执行结果</returns>
        Result RemovePermissionGroup(RemovePermissionGroupDto removePermissionGroupDto);

        #endregion

        #region 修改分组排序

        /// <summary>
        /// 修改分组排序
        /// </summary>
        /// <param name="modifyPermissionGroupSortDto">权限分组排序修改信息</param>
        /// <returns>返回执行结果</returns>
        Result ModifyPermissionGroupSort(ModifyPermissionGroupSortDto modifyPermissionGroupSortDto);

        #endregion

        #region 验证权限分组名称是否存在

        /// <summary>
        /// 验证权限分组名称是否存在
        /// </summary>
        /// <param name="existPermissionGroupNameDto">权限分组名称检查信息</param>
        /// <returns>返回权限分组名称是否存在</returns>
        bool ExistPermissionGroupName(ExistPermissionGroupNameDto existPermissionGroupNameDto);

        #endregion
    }
}
