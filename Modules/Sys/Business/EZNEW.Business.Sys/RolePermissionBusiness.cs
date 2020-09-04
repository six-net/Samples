using EZNEW.BusinessContract.Sys;
using EZNEW.DependencyInjection;
using EZNEW.Develop.UnitOfWork;
using EZNEW.Domain.Sys.Parameter;
using EZNEW.Domain.Sys.Service;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.Business.Sys
{
    /// <summary>
    /// 角色授权业务逻辑
    /// </summary>
    public class RolePermissionBusiness : IRolePermissionBusiness
    {
        static readonly IRolePermissionService rolePermissionService = ContainerManager.Resolve<IRolePermissionService>();

        #region 修改角色授权

        /// <summary>
        /// 修改角色授权
        /// </summary>
        /// <param name="modifyRolePermissionDto">角色授权修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyRolePermission(ModifyRolePermissionDto modifyRolePermissionDto)
        {
            if (modifyRolePermissionDto == null)
            {
                return Result.FailedResult("没有指定任何要修改的角色授权信息");
            }
            using (var businessWork = WorkManager.Create())
            {
                var modifyResult = rolePermissionService.Modify(modifyRolePermissionDto.MapTo<ModifyRolePermissionParameter>());
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var commitResult = businessWork.Commit();
                return commitResult.ExecutedSuccess ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
        }

        #endregion

        #region 清除角色授权

        /// <summary>
        /// 清除角色授权
        /// </summary>
        /// <param name="roleIds">角色系统编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearRolePermission(IEnumerable<long> roleIds)
        {
            if (roleIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何角色信息");
            }
            using (var work = WorkManager.Create())
            {
                var result = rolePermissionService.ClearByRole(roleIds);
                if (!result.Success)
                {
                    return result;
                }
                var commitResult = work.Commit();
                if (!commitResult.EmptyResultOrSuccess)
                {
                    result = Result.FailedResult("修改失败");
                }
                return result;
            }
        }

        #endregion
    }
}
