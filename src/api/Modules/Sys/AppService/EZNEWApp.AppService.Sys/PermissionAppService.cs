using System;
using System.Collections.Generic;
using System.Linq;
using EZNEWApp.AppServiceContract.Sys;
using EZNEW.DependencyInjection;
using EZNEW.Development.UnitOfWork;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Service;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Parameter.Filter;
using EZNEWApp.BusinessContract.Sys;

namespace EZNEWApp.AppService.Sys
{
    /// <summary>
    /// 权限业务逻辑操作
    /// </summary>
    public class PermissionAppService : IPermissionAppService
    {
        IPermissionBusiness permissionBusiness;

        public PermissionAppService(IPermissionBusiness permissionBusiness)
        {
            this.permissionBusiness = permissionBusiness;
        }

        #region 保存权限

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="savePermissionParameter">权限保存信息</param>
        /// <returns>返回执行结果</returns>
        public Result<Permission> SavePermission(SavePermissionParameter savePermissionParameter)
        {
            return permissionBusiness.SavePermission(savePermissionParameter);
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
            return permissionBusiness.GetPermission(filter);
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
            return permissionBusiness.GetPermissionList(filter);
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
            return permissionBusiness.GetPermissionPaging(filter);
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
            return permissionBusiness.RemovePermission(removePermissionParameter);
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
            return permissionBusiness.ModifyPermissionStatus(modifyPermissionStatusParameter);
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
            return permissionBusiness.ExistPermissionCode(existPermissionCodeParameter);
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
            return permissionBusiness.ExistPermissionName(existPermissionNameParameter);
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
            return permissionBusiness.ModifyPermissionOperation(modifyPermissionOperationParameter);
        }

        #endregion

        #region 清除权限授权的操作功能

        /// <summary>
        /// 清除权限授权的操作功能
        /// </summary>
        /// <param name="permissionIds">权限编号</param>
        /// <returns>返回执行结果</returns>
        public Result CleanPermissionOperation(IEnumerable<long> permissionIds)
        {
            return permissionBusiness.ClearOperation(permissionIds);
        }

        #endregion
    }
}
