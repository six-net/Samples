using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.BusinessContract.Sys;
using EZNEW.DependencyInjection;
using EZNEW.Develop.UnitOfWork;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Parameter;
using EZNEW.Domain.Sys.Service;
using EZNEW.DTO.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Filter;
using EZNEW.Paging;
using EZNEW.Response;

namespace EZNEW.Business.Sys
{
    /// <summary>
    /// 权限业务逻辑操作
    /// </summary>
    public class PermissionBusiness : IPermissionBusiness
    {
        static readonly IPermissionService permissionService = ContainerManager.Resolve<IPermissionService>();
        static readonly IPermissionOperationService permissionOperationService = ContainerManager.Resolve<IPermissionOperationService>();

        #region 保存权限

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="savePermissionDto">权限保存信息</param>
        /// <returns>返回执行结果</returns>
        public Result<PermissionDto> SavePermission(SavePermissionDto savePermissionDto)
        {
            using (var businessWork = WorkManager.Create())
            {
                if (savePermissionDto == null)
                {
                    return Result<PermissionDto>.FailedResult("没有指定任何要保存的权限信息");
                }

                #region 保存权限数据

                var authSaveResult = permissionService.Save(savePermissionDto.Permission.MapTo<Permission>());
                if (!authSaveResult.Success)
                {
                    return Result<PermissionDto>.FailedResult(authSaveResult.Message);
                }

                #endregion

                var commitVal = businessWork.Commit();
                Result<PermissionDto> result = null;
                if (commitVal.EmptyResultOrSuccess)
                {
                    result = Result<PermissionDto>.SuccessResult("保存成功");
                    result.Data = authSaveResult.Data.MapTo<PermissionDto>();
                }
                else
                {
                    result = Result<PermissionDto>.FailedResult("保存失败");
                }
                return result;
            }
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
            var permission = permissionService.Get(filter?.ConvertToFilter());
            return permission.MapTo<PermissionDto>();
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
            var permissionList = permissionService.GetList(filter?.ConvertToFilter());
            return permissionList.Select(c => c.MapTo<PermissionDto>()).ToList();
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
            var permissionPaging = permissionService.GetPaging(filter?.ConvertToFilter());
            return permissionPaging.ConvertTo<PermissionDto>();
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
            using (var businessWork = WorkManager.Create())
            {
                #region 参数判断

                if (removePermissionDto?.Ids.IsNullOrEmpty() ?? true)
                {
                    return Result.FailedResult("没有指定要删除的权限");
                }

                #endregion

                permissionService.Remove(removePermissionDto.Ids);
                var exectVal = businessWork.Commit();
                return exectVal.ExecutedSuccess ? Result.SuccessResult("删除成功") : Result.FailedResult("删除失败");
            }
        }

        #endregion

        #region 修改权限状态

        /// <summary>
        /// 修改权限状态
        /// </summary>
        /// <param name="statusInfo">权限状态修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyPermissionStatus(ModifyPermissionStatusDto modifyPermissionStatusDto)
        {
            using (var businessWork = WorkManager.Create())
            {
                if (modifyPermissionStatusDto?.StatusInfos.IsNullOrEmpty() ?? true)
                {
                    return Result.FailedResult("没有指定任何要修改的权限信息");
                }
                var modifyResult = permissionService.ModifyStatus(modifyPermissionStatusDto.MapTo<ModifyPermissionStatus>());
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var commitVal = businessWork.Commit();
                return commitVal.ExecutedSuccess ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
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
            if (existPermissionCodeDto == null)
            {
                return false;
            }
            return permissionService.ExistCode(existPermissionCodeDto.Code, existPermissionCodeDto.ExcludeId);
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
            if (existPermissionNameDto == null)
            {
                return false;
            }
            return permissionService.ExistName(existPermissionNameDto.Name, existPermissionNameDto.ExcludeId);
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
            if (modifyPermissionOperationDto == null)
            {
                return Result.FailedResult("没有指定任何要修改的信息");
            }
            using (var businessWork = WorkManager.Create())
            {
                var modifyResult = permissionOperationService.Modify(modifyPermissionOperationDto.MapTo<ModifyPermissionOperation>());
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var commitResult = businessWork.Commit();
                return commitResult.ExecutedSuccess ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
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
                return commitResult.EmptyResultOrSuccess ? Result.SuccessResult("清除成功") : Result.FailedResult("清除失败");
            }
        }

        #endregion
    }
}
