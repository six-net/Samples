using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.AppServiceContract.Sys;
using EZNEW.BusinessContract.Sys;
using EZNEW.DTO.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Filter;
using EZNEW.Paging;
using EZNEW.Response;

namespace EZNEW.AppService.Sys
{
    /// <summary>
    /// 权限应用服务
    /// </summary>
    public class PermissionAppService : IPermissionAppService
    {
        /// <summary>
        /// 权限业务
        /// </summary>
        readonly IPermissionBusiness permissionBusiness;

        public PermissionAppService(IPermissionBusiness permissionBusiness)
        {
            this.permissionBusiness = permissionBusiness;
        }

        #region 保存权限

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="savePermissionDto">权限保存信息</param>
        /// <returns>执行结果</returns>
        public Result<PermissionDto> SavePermission(SavePermissionDto savePermissionDto)
        {
            return permissionBusiness.SavePermission(savePermissionDto);
        }

        #endregion

        #region 获取权限

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="filter">权限筛选信息</param>
        /// <returns>返回权限</returns>
        public PermissionDto GetPermission(PermissionFilterDto filter)
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
        public List<PermissionDto> GetPermissionList(PermissionFilterDto filter)
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
        public IPaging<PermissionDto> GetPermissionPaging(PermissionFilterDto filter)
        {
            return permissionBusiness.GetPermissionPaging(filter);
        }

        #endregion

        #region 删除权限

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="removePermissionDto">权限删除信息</param>
        /// <returns>返回执行结果</returns>
        public Result RemovePermission(RemovePermissionDto removePermissionDto)
        {
            return permissionBusiness.RemovePermission(removePermissionDto);
        }

        #endregion

        #region 修改权限状态

        /// <summary>
        /// 修改权限状态
        /// </summary>
        /// <param name="modifyPermissionStatusDto">权限状态修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyPermissionStatus(ModifyPermissionStatusDto modifyPermissionStatusDto)
        {
            return permissionBusiness.ModifyPermissionStatus(modifyPermissionStatusDto);
        }

        #endregion

        #region 检查权限编码是否存在

        /// <summary>
        /// 检查权限编码是否存在
        /// </summary>
        /// <param name="existPermissionCodeDto">权限编码检查信息</param>
        /// <returns>返回权限编码是否存在</returns>
        public bool ExistPermissionCode(ExistPermissionCodeDto existPermissionCodeDto)
        {
            return permissionBusiness.ExistPermissionCode(existPermissionCodeDto);
        }

        #endregion

        #region 检查权限名称是否存在

        /// <summary>
        /// 检查权限名称是否存在
        /// </summary>
        /// <param name="existPermissionNameDto">权限名称检查信息</param>
        /// <returns>返回权限名称是否存在</returns>
        public bool ExistPermissionName(ExistPermissionNameDto existPermissionNameDto)
        {
            return permissionBusiness.ExistPermissionName(existPermissionNameDto);
        }

        #endregion

        #region 修改权限授权的操作功能

        /// <summary>
        /// 修改权限授权的操作功能
        /// </summary>
        /// <param name="modifyPermissionOperationDto">权限授权操作修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyPermissionOperation(ModifyPermissionOperationDto modifyPermissionOperationDto)
        {
            return permissionBusiness.ModifyPermissionOperation(modifyPermissionOperationDto);
        }

        #endregion

        #region 清除权限授权的操作功能

        /// <summary>
        /// 清除权限授权的操作功能
        /// </summary>
        /// <param name="permissionIds">权限编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearPermissionOperation(IEnumerable<long> permissionIds)
        {
            return permissionBusiness.ClearPermissionOperation(permissionIds);
        }

        #endregion
    }
}
