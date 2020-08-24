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
    /// 权限应用服务
    /// </summary>
    public interface IPermissionAppService
    {
        #region 保存权限

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="savePermissionDto">权限保存信息</param>
        /// <returns>执行结果</returns>
        Result<PermissionDto> SavePermission(SavePermissionDto savePermissionDto);

        #endregion

        #region 获取权限

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="filter">权限筛选信息</param>
        /// <returns>返回权限</returns>
        PermissionDto GetPermission(PermissionFilterDto filter);

        #endregion

        #region 获取权限列表

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="filter">权限筛选信息</param>
        /// <returns>返回权限列表</returns>
        List<PermissionDto> GetPermissionList(PermissionFilterDto filter);

        #endregion

        #region 获取权限分页

        /// <summary>
        /// 获取权限分页
        /// </summary>
        /// <param name="filter">权限筛选信息</param>
        /// <returns>返回权限分页</returns>
        IPaging<PermissionDto> GetPermissionPaging(PermissionFilterDto filter);

        #endregion

        #region 删除权限

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="removePermissionDto">权限删除信息</param>
        /// <returns>返回执行结果</returns>
        Result RemovePermission(RemovePermissionDto removePermissionDto);

        #endregion

        #region 修改权限状态

        /// <summary>
        /// 修改权限状态
        /// </summary>
        /// <param name="modifyPermissionStatusDto">权限状态修改信息</param>
        /// <returns>返回执行结果</returns>
        Result ModifyPermissionStatus(ModifyPermissionStatusDto modifyPermissionStatusDto);

        #endregion

        #region 检查权限编码是否存在

        /// <summary>
        /// 检查权限编码是否存在
        /// </summary>
        /// <param name="existPermissionCodeDto">权限编码检查信息</param>
        /// <returns>返回权限编码是否存在</returns>
        bool ExistPermissionCode(ExistPermissionCodeDto existPermissionCodeDto);

        #endregion

        #region 检查权限名称是否存在

        /// <summary>
        /// 检查权限名称是否存在
        /// </summary>
        /// <param name="existPermissionNameDto">权限名称检查信息</param>
        /// <returns>返回权限名称是否存在</returns>
        bool ExistPermissionName(ExistPermissionNameDto existPermissionNameDto);

        #endregion

        #region 修改权限授权的操作功能

        /// <summary>
        /// 修改权限授权的操作功能
        /// </summary>
        /// <param name="modifyPermissionOperationDto">权限授权操作修改信息</param>
        /// <returns>返回执行结果</returns>
        Result ModifyPermissionOperation(ModifyPermissionOperationDto modifyPermissionOperationDto);

        #endregion

        #region 清除权限授权的操作功能

        /// <summary>
        /// 清除权限授权的操作功能
        /// </summary>
        /// <param name="permissionIds">权限编号</param>
        /// <returns>返回执行结果</returns>
        Result ClearPermissionOperation(IEnumerable<long> permissionIds);

        #endregion
    }
}
