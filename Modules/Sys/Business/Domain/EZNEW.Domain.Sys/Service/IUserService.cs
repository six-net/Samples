using System.Collections.Generic;
using EZNEW.Develop.CQuery;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Parameter;
using EZNEW.Domain.Sys.Parameter.Filter;
using EZNEW.Paging;
using EZNEW.Response;

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
        /// <returns>返回执行结果</returns>
        Result<User> Save(User user);

        #endregion

        #region 删除账户

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userIds">用户编号</param>
        /// <returns>返回执行结果</returns>
        Result Remove(IEnumerable<long> userIds);

        #endregion

        #region 登陆

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginInfo">用户登录信息</param>
        /// <returns>返回登录结果</returns>
        Result<User> Login(Login loginInfo);

        #endregion

        #region 获取用户信息

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回用户</returns>
        User Get(long userId);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userFilter">用户筛选信息</param>
        /// <returns>返回用户</returns>
        User Get(UserFilter userFilter);

        #endregion

        #region 获取用户列表

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="userFilter">用户筛选条件</param>
        /// <returns>返回用户列表</returns>
        List<User> GetList(UserFilter userFilter);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="userIds">用户编号</param>
        /// <returns>返回用户列表</returns>
        List<User> GetList(IEnumerable<long> userIds);

        #endregion

        #region 获取用户分页

        /// <summary>
        /// 获取用户分页信息
        /// </summary>
        /// <param name="userFilter">用户筛选信息</param>
        /// <returns>返回用户分页信息</returns>
        IPaging<User> GetPaging(UserFilter userFilter);

        #endregion

        #region 修改密码

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="modifyUserPassword">用户密码修改信息</param>
        /// <returns>返回执行结果</returns>
        Result ModifyPassword(ModifyUserPassword modifyUserPassword);

        #endregion

        #region 修改用户状态

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="modifyUserStatus">用户状态修改信息</param>
        /// <returns>返回执行结果</returns>
        Result ModifyStatus(ModifyUserStatus modifyUserStatus);

        #endregion

        #region 验证用户名是否存在

        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="excludeUserId">需要排除的用户编号</param>
        /// <returns>返回用户名是否存在</returns>
        bool ExistName(string userName, long? excludeUserId = null);

        #endregion
    }
}
