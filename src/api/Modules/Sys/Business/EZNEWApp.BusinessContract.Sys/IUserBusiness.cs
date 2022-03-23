using System.Collections.Generic;
using EZNEW.Model;
using EZNEW.Paging;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Parameter.Filter;

namespace EZNEWApp.BusinessContract.Sys
{
    /// <summary>
    /// 用户业务接口
    /// </summary>
    public interface IUserBusiness
    {
        #region 保存用户

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="saveUserParameter">用户保存信息</param>
        /// <returns>返回用户保存结果</returns>
        Result<User> SaveUser(SaveUserParameter saveUserParameter);

        #endregion

        #region 获取用户

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userFilter">用户筛选信息</param>
        /// <returns>返回用户信息</returns>
        User GetUser(UserFilter userFilter);

        #endregion

        #region 获取用户列表

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="userFilter">用户筛选信息</param>
        /// <returns>返回用户列表</returns>
        List<User> GetUserList(UserFilter userFilter);

        #endregion

        #region 获取用户分页

        /// <summary>
        /// 获取用户分页
        /// </summary>
        /// <param name="userFilter">用户筛选信息</param>
        /// <returns>返回用户分页</returns>
        PagingInfo<User> GetUserPaging(UserFilter userFilter);

        #endregion

        #region 删除用户

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="removeUserParameter">用户删除信息</param>
        /// <returns>返回删除用户操作结果</returns>
        Result RemoveUser(RemoveUserParameter removeUserParameter);

        #endregion

        #region 用户登录

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginParameter">登录信息</param>
        /// <returns>返回登录信息</returns>
        Result<User> Login(LoginParameter loginParameter);

        #endregion

        #region 修改密码

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="modifyPasswordParameter">密码修改信息</param>
        /// <returns>返回密码修改结果</returns>
        Result ModifyPassword(ModifyUserPasswordParameter modifyPasswordParameter);

        #endregion

        #region 修改用户状态

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="modifyUserStatusParameter">用户状态修改信息</param>
        /// <returns>返回状态修改结果</returns>
        Result ModifyStatus(ModifyUserStatusParameter modifyUserStatusParameter);

        #endregion

        #region 修改用户绑定角色

        /// <summary>
        /// 修改用户绑定角色
        /// </summary>
        /// <param name="modifyUserRoleParameter">用户角色修改信息</param>
        /// <returns>返回角色修改结果</returns>
        Result ModifyUserRole(ModifyUserRoleParameter modifyUserRoleParameter);

        #endregion

        #region 清除用户绑定的角色

        /// <summary>
        /// 清除用户绑定的角色
        /// </summary>
        /// <param name="userIds">用户系统编号</param>
        /// <returns>执行结果</returns>
        Result ClearRole(IEnumerable<long> userIds);

        #endregion

        #region 检查用户名是否存在

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="existUserNameParameter">验证参数</param>
        /// <returns>返回用户名是否存在</returns>
        bool ExistUserName(ExistUserNameParameter existUserNameParameter);

        #endregion
    }
}
