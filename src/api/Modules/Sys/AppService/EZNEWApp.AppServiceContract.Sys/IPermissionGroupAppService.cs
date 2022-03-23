using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter.Filter;

namespace EZNEWApp.AppServiceContract.Sys
{
    /// <summary>
    /// 权限分组逻辑
    /// </summary>
    public interface IPermissionGroupAppService
    {
        #region 保存权限分组

        /// <summary>
        /// 保存权限分组
        /// </summary>
        /// <param name="savePermissionGroupParameter">权限分组保存信息</param>
        /// <returns>返回执行结果</returns>
        Result<PermissionGroup> SavePermissionGroup(SavePermissionGroupParameter savePermissionGroupParameter);

        #endregion

        #region 获取权限分组

        /// <summary>
        /// 获取权限分组
        /// </summary>
        /// <param name="filter">权限分组筛选信息</param>
        /// <returns>返回权限分组</returns>
        PermissionGroup GetPermissionGroup(PermissionGroupFilter filter);

        #endregion

        #region 获取权限分组列表

        /// <summary>
        /// 获取权限分组列表
        /// </summary>
        /// <param name="filter">权限分组筛选信息</param>
        /// <returns>返回权限分组列表</returns>
        List<PermissionGroup> GetPermissionGroupList(PermissionGroupFilter filter);

        #endregion

        #region 获取权限分组分页

        /// <summary>
        /// 获取权限分组分页
        /// </summary>
        /// <param name="filter">权限分组筛选信息</param>
        /// <returns>返回权限分组分页</returns>
        PagingInfo<PermissionGroup> GetPermissionGroupPaging(PermissionGroupFilter filter);

        #endregion

        #region 删除权限分组

        /// <summary>
        /// 删除权限分组
        /// </summary>
        /// <param name="removePermissionGroupParameter">权限分组删除信息</param>
        /// <returns>返回执行结果</returns>
        Result RemovePermissionGroup(RemovePermissionGroupParameter removePermissionGroupParameter);

        #endregion

        #region 修改分组排序

        /// <summary>
        /// 修改分组排序
        /// </summary>
        /// <param name="modifyPermissionGroupSortParameter">权限分组排序修改信息</param>
        /// <returns>返回执行结果</returns>
        Result ModifyPermissionGroupSort(ModifyPermissionGroupSortParameter modifyPermissionGroupSortParameter);

        #endregion

        #region 验证权限分组名称是否存在

        /// <summary>
        /// 验证权限分组名称是否存在
        /// </summary>
        /// <param name="existPermissionGroupNameParameter">权限分组名称检查信息</param>
        /// <returns>返回权限分组名称是否存在</returns>
        bool ExistPermissionGroupName(ExistPermissionGroupNameParameter existPermissionGroupNameParameter);

        #endregion
    }
}
