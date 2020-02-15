using EZNEW.Develop.CQuery;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Service.Param;
using EZNEW.Framework.Paging;
using EZNEW.Framework.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.Domain.Sys.Service
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public interface IUserService
    {
        #region 保存用户

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        Result<User> SaveUser(User user);

        #endregion

        #region 删除账户

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="users">要删除的用户信息</param>
        /// <returns></returns>
        Result DeleteUser(IEnumerable<User> users);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userIds">用户编号</param>
        /// <returns></returns>
        Result DeleteUser(IEnumerable<long> userIds);

        #endregion

        #region 登陆

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user">登录用户信息</param>
        /// <returns></returns>
        Result<User> Login(UserLogin loginInfo);

        #endregion

        #region 获取用户信息

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        User GetUser(IQuery query);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        User GetUser(long userId);

        #endregion

        #region 获取用户列表

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="query">筛选条件</param>
        /// <returns></returns>
        List<User> GetUserList(IQuery query);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="userIds">用户编号</param>
        /// <returns></returns>
        List<User> GetUserList(IEnumerable<long> userIds);

        #endregion

        #region 获取用户分页

        /// <summary>
        /// 获取用户分页
        /// </summary>
        /// <param name="query">筛选信息</param>
        /// <returns></returns>
        IPaging<User> GetUserPaging(IQuery query);

        #endregion

        #region 修改密码

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="modifyInfo">修改信息</param>
        /// <returns></returns>
        Result ModifyPassword(ModifyUserPassword modifyInfo);

        #endregion

        #region 修改用户状态

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="userStatus">状态信息</param>
        /// <returns>执行结果</returns>
        Result ModifyStatus(params UserStatusInfo[] userStatus);

        #endregion

        #region 验证用户名是否存在

        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="excludeUserId">检查除指定用户以外的用户信息</param>
        /// <returns></returns>
        bool ExistUserName(string userName, long? excludeUserId = null);

        #endregion
    }
}
