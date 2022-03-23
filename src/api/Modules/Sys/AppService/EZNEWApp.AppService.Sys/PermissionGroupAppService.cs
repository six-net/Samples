using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZNEWApp.AppServiceContract.Sys;
using EZNEW.DependencyInjection;
using EZNEW.Development.UnitOfWork;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter.Filter;
using EZNEWApp.Domain.Sys.Service;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.BusinessContract.Sys;

namespace EZNEWApp.AppService.Sys
{
    /// <summary>
    /// 权限分组业务逻辑
    /// </summary>
    public class PermissionGroupAppService : IPermissionGroupAppService
    {
        IPermissionGroupBusiness permissionGroupBusiness;

        public PermissionGroupAppService(IPermissionGroupBusiness permissionGroupBusiness)
        {
            this.permissionGroupBusiness = permissionGroupBusiness;
        }

        #region 保存权限分组

        /// <summary>
        /// 保存权限分组
        /// </summary>
        /// <param name="savePermissionGroupParameter">权限分组保存信息</param>
        /// <returns>返回执行结果</returns>
        public Result<PermissionGroup> SavePermissionGroup(SavePermissionGroupParameter savePermissionGroupParameter)
        {
            return permissionGroupBusiness.SavePermissionGroup(savePermissionGroupParameter);
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
            return permissionGroupBusiness.GetPermissionGroup(filter);
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
            return permissionGroupBusiness.GetPermissionGroupList(filter);
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
            return permissionGroupBusiness.GetPermissionGroupPaging(filter);
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
            return permissionGroupBusiness.RemovePermissionGroup(removePermissionGroupParameter);
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
            return permissionGroupBusiness.ModifyPermissionGroupSort(modifyPermissionGroupSortParameter);
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
            return permissionGroupBusiness.ExistPermissionGroupName(existPermissionGroupNameParameter);
        }

        #endregion
    }
}
