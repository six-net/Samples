using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.BusinessContract.Sys;
using EZNEW.DependencyInjection;
using EZNEW.Develop.UnitOfWork;
using EZNEW.Domain.Sys.Parameter;
using EZNEW.Domain.Sys.Service;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.Response;

namespace EZNEW.Business.Sys
{
    /// <summary>
    /// 用户授权业务逻辑
    /// </summary>
    public class UserPermissionBusiness : IUserPermissionBusiness
    {
        static readonly IUserPermissionService userPermissionService = ContainerManager.Resolve<IUserPermissionService>();

        #region 修改用户授权

        /// <summary>
        /// 修改用户授权
        /// </summary>
        /// <param name="modifyUserPermissionDto">用户授权修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyUserPermission(ModifyUserPermissionDto modifyUserPermissionDto)
        {
            if (modifyUserPermissionDto?.UserPermissions.IsNullOrEmpty() ?? true)
            {
                return Result.FailedResult("没有指定任何要修改的用户授权信息");
            }
            using (var businessWork = WorkManager.Create())
            {
                var modifyResult = userPermissionService.Modify(modifyUserPermissionDto.MapTo<ModifyUserPermission>());
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var commitResult = businessWork.Commit();
                return commitResult.ExecutedSuccess ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
        }

        #endregion

        #region 清除用户授权

        /// <summary>
        /// 清除用户授权
        /// </summary>
        /// <param name="userIds">用户系统编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearUserPermission(IEnumerable<long> userIds)
        {
            if (userIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何用户信息");
            }
            using (var work = WorkManager.Create())
            {
                var result = userPermissionService.ClearByUser(userIds);
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
