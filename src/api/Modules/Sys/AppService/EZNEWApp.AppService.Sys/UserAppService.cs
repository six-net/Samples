using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Model;
using EZNEW.Development.UnitOfWork;
using EZNEW.Paging;
using EZNEWApp.Domain.Sys.Service;
using EZNEWApp.AppServiceContract.Sys;
using EZNEWApp.Domain.Sys.Model;
using EZNEW.DependencyInjection;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Parameter.Filter;
using EZNEWApp.BusinessContract.Sys;

namespace EZNEWApp.AppService.Sys
{
    /// <summary>
    /// 用户逻辑
    /// </summary>
    public class UserAppService : IUserAppService
    {
        IUserBusiness userBusiness;

        public UserAppService(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        #region 保存用户

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="saveUserParameter">用户保存信息</param>
        /// <returns>返回用户保存结果</returns>
        public Result<User> SaveUser(SaveUserParameter saveUserParameter)
        {
            return userBusiness.SaveUser(saveUserParameter);
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
            return userBusiness.GetUser(userFilter);
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
            return userBusiness.GetUserList(userFilter);
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
            return userBusiness.GetUserPaging(userFilter);
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
            return userBusiness.RemoveUser(removeUserParameter);
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
            return userBusiness.Login(loginParameter);
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
            return userBusiness.ModifyPassword(modifyPasswordParameter);
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
            return userBusiness.ModifyStatus(modifyUserStatusParameter);
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
            return userBusiness.ModifyUserRole(modifyUserRoleParameter);
        }

        #endregion

        #region 清除用户绑定的角色

        /// <summary>
        /// 清除用户绑定的角色
        /// </summary>
        /// <param name="userIds">用户系统编号</param>
        /// <returns>执行结果</returns>
        public Result CleanRole(IEnumerable<long> userIds)
        {
            return userBusiness.ClearRole(userIds);
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
            return userBusiness.ExistUserName(existUserNameParameter);
        }

        #endregion
    }
}
