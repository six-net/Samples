using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Model;
using EZNEW.Development.UnitOfWork;
using EZNEW.Paging;
using EZNEWApp.Domain.Sys.Service;
using EZNEWApp.BusinessContract.Sys;
using EZNEWApp.Domain.Sys.Model;
using EZNEW.DependencyInjection;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Parameter.Filter;

namespace EZNEWApp.Business.Sys
{
    /// <summary>
    /// 用户逻辑
    /// </summary>
    public class UserBusiness : IUserBusiness
    {
        static readonly IUserService userService = ContainerManager.Resolve<IUserService>();
        static readonly IUserRoleService userRoleService = ContainerManager.Resolve<IUserRoleService>();

        #region 保存用户

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="saveUserParameter">用户保存信息</param>
        /// <returns>返回用户保存结果</returns>
        public Result<User> SaveUser(SaveUserParameter saveUserParameter)
        {
            if (saveUserParameter is null)
            {
                throw new ArgumentNullException(nameof(saveUserParameter));
            }

            using (var work = WorkManager.Create())
            {
                var saveResult = userService.Save(saveUserParameter.User);
                if (!saveResult.Success)
                {
                    return Result<User>.FailedResult(saveResult.Message);
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? saveResult : Result<User>.FailedResult("保存失败");
            }
        }

        #endregion

        #region 获取用户

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userFilter">用户筛选信息</param>
        /// <returns>返回用户信息</returns>
        public User GetUser(UserFilter userFilter)
        {
            return userService.Get(userFilter);
        }

        #endregion

        #region 获取用户列表

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="userFilter">用户筛选信息</param>
        /// <returns>返回用户列表</returns>
        public List<User> GetUserList(UserFilter userFilter)
        {
            return userService.GetList(userFilter);
        }

        #endregion

        #region 获取用户分页

        /// <summary>
        /// 获取用户分页
        /// </summary>
        /// <param name="userFilter">用户筛选信息</param>
        /// <returns>返回用户分页</returns>
        public PagingInfo<User> GetUserPaging(UserFilter userFilter)
        {
            return userService.GetPaging(userFilter);
        }

        #endregion

        #region 删除用户

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="removeUserParameter">删除用户信息</param>
        /// <returns>返回用户删除结果</returns>
        public Result RemoveUser(RemoveUserParameter removeUserParameter)
        {
            if (removeUserParameter is null)
            {
                throw new ArgumentNullException(nameof(removeUserParameter));
            }

            using (var work = WorkManager.Create())
            {
                var deleteResult = userService.Remove(removeUserParameter.Ids);
                if (!deleteResult.Success)
                {
                    return deleteResult;
                }
                var commitResult = work.Commit();
                return commitResult.Success ? Result.SuccessResult("删除成功") : Result.FailedResult("删除失败");
            }
        }

        #endregion

        #region 登陆

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginParameter">登录信息</param>
        /// <returns>返回登录结果</returns>
        public Result<User> Login(LoginParameter loginParameter)
        {
            if (loginParameter == null)
            {
                return Result<User>.FailedResult("用户登录信息为空");
            }
            return userService.Login(loginParameter);
        }

        #endregion

        #region 修改密码

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="modifyPasswordParameter">密码修改信息</param>
        /// <returns>返回密码修改结果</returns>
        public Result ModifyPassword(ModifyUserPasswordParameter modifyPasswordParameter)
        {
            if (modifyPasswordParameter is null)
            {
                throw new ArgumentNullException(nameof(modifyPasswordParameter));
            }

            using (var work = WorkManager.Create())
            {
                var modifyResult = userService.ModifyPassword(modifyPasswordParameter);
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
        }

        #endregion

        #region 修改用户状态

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="modifyUserStatusParameter">用户状态修改信息</param>
        /// <returns>返回状态修改执行结果</returns>
        public Result ModifyStatus(ModifyUserStatusParameter modifyUserStatusParameter)
        {
            if (modifyUserStatusParameter is null)
            {
                throw new ArgumentNullException(nameof(modifyUserStatusParameter));
            }

            using (var work = WorkManager.Create())
            {
                var modifyResult = userService.ModifyStatus(modifyUserStatusParameter);
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var commitResult = work.Commit();
                return commitResult.Success ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
        }

        #endregion

        #region 修改用户绑定角色

        /// <summary>
        /// 修改用户绑定角色
        /// </summary>
        /// <param name="modifyUserRoleParameter">用户角色修改信息</param>
        /// <returns>返回用户角色修改结果</returns>
        public Result ModifyUserRole(ModifyUserRoleParameter modifyUserRoleParameter)
        {
            if (modifyUserRoleParameter is null)
            {
                throw new ArgumentNullException(nameof(modifyUserRoleParameter));
            }

            using (var work = WorkManager.Create())
            {
                var result = userRoleService.Modify(modifyUserRoleParameter);
                if (!result.Success)
                {
                    return result;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
        }

        #endregion

        #region 清除用户绑定的角色

        /// <summary>
        /// 清除用户绑定的角色
        /// </summary>
        /// <param name="userIds">用户系统编号</param>
        /// <returns>执行结果</returns>
        public Result ClearRole(IEnumerable<long> userIds)
        {
            if (userIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有任何要操作的用户");
            }
            using (var work = WorkManager.Create())
            {
                var result = userRoleService.ClearByUser(userIds);
                if (!result.Success)
                {
                    return result;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? result : Result.FailedResult("修改失败");
            }
        }

        #endregion

        #region 检查用户名是否存在

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="existUserNameParameter">验证参数</param>
        /// <returns>返回用户名是否存在</returns>
        public bool ExistUserName(ExistUserNameParameter existUserNameParameter)
        {
            return userService.ExistUserName(existUserNameParameter);
        }

        #endregion
    }
}
