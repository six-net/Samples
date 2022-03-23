using System.Collections.Generic;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter.Filter;

namespace EZNEWApp.BusinessContract.Sys
{
    /// <summary>
    /// 权限业务逻辑接口
    /// </summary>
    public interface IPermissionBusiness
    {
        #region 保存权限

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="savePermissionParameter">权限保存信息</param>
        /// <returns>执行结果</returns>
        Result<Permission> SavePermission(SavePermissionParameter savePermissionParameter);

        #endregion

        #region 获取权限

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="filter">权限筛选信息</param>
        /// <returns>返回权限</returns>
        Permission GetPermission(PermissionFilter filter);

        #endregion

        #region 获取权限列表

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="filter">权限筛选信息</param>
        /// <returns>返回权限列表</returns>
        List<Permission> GetPermissionList(PermissionFilter filter);

        #endregion

        #region 获取权限分页

        /// <summary>
        /// 获取权限分页
        /// </summary>
        /// <param name="filter">权限筛选信息</param>
        /// <returns>返回权限分页</returns>
        PagingInfo<Permission> GetPermissionPaging(PermissionFilter filter);

        #endregion

        #region 删除权限

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="removePermissionParameter">权限删除信息</param>
        /// <returns>返回执行结果</returns>
        Result RemovePermission(RemovePermissionParameter removePermissionParameter);

        #endregion

        #region 修改权限状态

        /// <summary>
        /// 修改权限状态
        /// </summary>
        /// <param name="modifyPermissionStatusParameter">权限状态修改信息</param>
        /// <returns>返回执行结果</returns>
        Result ModifyPermissionStatus(ModifyPermissionStatusParameter modifyPermissionStatusParameter);

        #endregion

        #region 检查权限编码是否存在

        /// <summary>
        /// 检查权限编码是否存在
        /// </summary>
        /// <param name="existPermissionCodeParameter">权限编码检查信息</param>
        /// <returns>返回权限编码是否存在</returns>
        bool ExistPermissionCode(ExistPermissionCodeParameter existPermissionCodeParameter);

        #endregion

        #region 检查权限名称是否存在

        /// <summary>
        /// 检查权限名称是否存在
        /// </summary>
        /// <param name="existPermissionNameParameter">权限名称检查信息</param>
        /// <returns>返回权限名称是否存在</returns>
        bool ExistPermissionName(ExistPermissionNameParameter existPermissionNameParameter);

        #endregion

        #region 修改权限授权的操作功能

        /// <summary>
        /// 修改权限授权的操作功能
        /// </summary>
        /// <param name="modifyPermissionOperationParameter">权限授权操作修改信息</param>
        /// <returns>返回执行结果</returns>
        Result ModifyPermissionOperation(ModifyOperationPermissionParameter modifyPermissionOperationParameter);

        #endregion

        #region 清除权限授权的操作功能

        /// <summary>
        /// 清除权限授权的操作功能
        /// </summary>
        /// <param name="permissionIds">权限编号</param>
        /// <returns>返回执行结果</returns>
        Result ClearOperation(IEnumerable<long> permissionIds);

        #endregion
    }
}
