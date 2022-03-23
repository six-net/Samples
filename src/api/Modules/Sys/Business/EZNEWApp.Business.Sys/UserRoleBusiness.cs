using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Development.UnitOfWork;
using EZNEW.Model;
using EZNEWApp.BusinessContract.Sys;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Service;

namespace EZNEWApp.Business.Sys
{
    /// <summary>
    /// 用户&角色实现
    /// </summary>
    public class UserRoleBusiness : IUserRoleBusiness
    {
        readonly IUserRoleService userRoleService;

        public UserRoleBusiness(IUserRoleService userRoleService)
        {
            this.userRoleService = userRoleService;
        }

        #region 清除角色下所有的用户

        /// <summary>
        /// 清除角色下所有的用户
        /// </summary>
        /// <param name="roleIds">角色系统编号</param>
        /// <returns>执行结果</returns>
        public Result ClearByRole(IEnumerable<long> roleIds)
        {
            using (var businessWork = WorkManager.Create())
            {
                var clearResult = userRoleService.ClearByRole(roleIds);
                if (!(clearResult?.Success ?? false))
                {
                    return clearResult;
                }
                var commitResult = businessWork.Commit();
                if (commitResult.EmptyOrSuccess)
                {
                    return Result.SuccessResult("清除成功");
                }
                return Result.FailedResult("清除失败");
            }
        }

        #endregion

        #region 清除用户绑定的所有角色

        /// <summary>
        /// 清除用户绑定的所有角色
        /// </summary>
        /// <param name="userIds">用户系统编号</param>
        /// <returns>执行结果</returns>
        public Result ClearByUser(IEnumerable<long> userIds)
        {
            using (var businessWork = WorkManager.Create())
            {
                var clearResult = userRoleService.ClearByUser(userIds);
                if (!(clearResult?.Success ?? false))
                {
                    return clearResult;
                }
                var commitResult = businessWork.Commit();
                if (commitResult.EmptyOrSuccess)
                {
                    return Result.SuccessResult("清除成功");
                }
                return Result.FailedResult("清除失败");
            }
        }

        #endregion

        #region 修改用户&角色绑定关系

        /// <summary>
        /// 修改用户&角色绑定关系
        /// </summary>
        /// <param name="modifyUserRoleParameter">用户&角色修绑定关系修改信息</param>
        /// <returns>返回操作结果</returns>
        public Result Modify(ModifyUserRoleParameter modifyUserRoleParameter)
        {
            using (var businessWork = WorkManager.Create())
            {
                var modifyResult = userRoleService.Modify(modifyUserRoleParameter);
                if (!(modifyResult?.Success ?? false))
                {
                    return modifyResult;
                }
                var commitResult = businessWork.Commit();
                if (commitResult.EmptyOrSuccess)
                {
                    return Result.SuccessResult("修改成功");
                }
                return Result.FailedResult("修改失败");
            }
        }

        #endregion
    }
}
