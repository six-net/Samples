using System.Collections.Generic;
using System.Linq;
using EZNEW.Paging;
using EZNEW.Response;
using EZNEW.Develop.CQuery;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using EZNEW.DependencyInjection;
using EZNEW.ExpressionUtil;
using EZNEW.Entity.Sys;
using EZNEW.Domain.Sys.Parameter.Filter;
using EZNEW.Domain.Sys.Parameter;
using EZNEW.Module.Sys;

namespace EZNEW.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class UserService : IUserService
    {
        static readonly IUserRepository userRepository = ContainerManager.Resolve<IUserRepository>();

        #region 保存用户

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>返回保存执行结果</returns>
        public Result<User> Save(User user)
        {
            return user?.Save() ?? Result<User>.FailedResult("用户保存失败");
        }

        #endregion

        #region 删除账户

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="users">要删除的用户信息</param>
        /// <returns>返回执行结果</returns>
        public Result Remove(IEnumerable<User> users)
        {
            if (users.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何要删除的用户");
            }
            foreach (var user in users)
            {
                var removeResult = user.Remove();
                if (!removeResult.Success)
                {
                    return removeResult;
                }
            }
            return Result.SuccessResult("用户删除成功");
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userIds">用户编号</param>
        /// <returns>返回执行结果</returns>
        public Result Remove(IEnumerable<long> userIds)
        {
            if (userIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定要删除的用户");
            }
            IEnumerable<User> users = userIds.Select(c => User.Create(c)).ToList();
            return Remove(users);
        }

        #endregion

        #region 登陆

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginInfo">登录用户信息</param>
        /// <returns>返回登录执行结果</returns>
        public Result<User> Login(LoginParameter loginInfo)
        {
            IQuery userQuery = QueryManager.Create<UserEntity>(u => u.UserName == loginInfo.UserName && u.Password == User.EncryptPassword(loginInfo.Password));
            User user = userRepository.Get(userQuery);
            if (user == null || !user.AllowLogin())
            {
                return Result<User>.FailedResult("登陆失败");
            }
            return Result<User>.SuccessResult("登陆成功", "", user);
        }

        #endregion

        #region 获取用户信息

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns>返回用户</returns>
        User GetUser(IQuery query)
        {
            return userRepository.Get(query);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回用户</returns>
        public User Get(long userId)
        {
            if (userId <= 0)
            {
                return null;
            }
            IQuery query = QueryManager.Create<UserEntity>(c => c.Id == userId);
            return GetUser(query);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userFilter">用户筛选信息</param>
        /// <returns>返回用户</returns>
        public User Get(UserFilter userFilter)
        {
            return GetUser(userFilter?.CreateQuery());
        }

        #endregion

        #region 获取用户列表

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="query">筛选条件</param>
        /// <returns>返回用户列表</returns>
        List<User> GetUserList(IQuery query)
        {
            return userRepository.GetList(query);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="userIds">用户编号</param>
        /// <returns>返回用户列表</returns>
        public List<User> GetList(IEnumerable<long> userIds)
        {
            if (userIds.IsNullOrEmpty())
            {
                return new List<User>(0);
            }
            IQuery query = QueryManager.Create<UserEntity>(c => userIds.Contains(c.Id));
            return GetUserList(query);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="userFilter">用户筛选条件</param>
        /// <returns>返回用户列表</returns>
        public List<User> GetList(UserFilter userFilter)
        {
            return GetUserList(userFilter?.CreateQuery());
        }

        #endregion

        #region 获取用户分页

        /// <summary>
        /// 获取用户分页
        /// </summary>
        /// <param name="query">筛选信息</param>
        /// <returns>返回用户分页</returns>
        IPaging<User> GetPaging(IQuery query)
        {
            return userRepository.GetPaging(query);
        }

        /// <summary>
        /// 获取用户分页
        /// </summary>
        /// <param name="userFilter">用户筛选信息</param>
        /// <returns>返回用户分页</returns>
        public IPaging<User> GetPaging(UserFilter userFilter)
        {
            return GetPaging(userFilter?.CreateQuery());
        }

        #endregion

        #region 修改密码

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="modifyUserPasswordParameter">用户密码修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyPassword(ModifyUserPasswordParameter modifyUserPasswordParameter)
        {
            #region 参数判断

            if (modifyUserPasswordParameter?.UserId < 1)
            {
                return Result.FailedResult("请指定要修改密码的用户");
            }

            #endregion

            //获取用户
            User nowUser = Get(modifyUserPasswordParameter.UserId);
            if (nowUser == null)
            {
                return Result.FailedResult("用户不存在");
            }
            //修改用户密码
            var modifyResult = nowUser.ModifyPassword(modifyUserPasswordParameter);
            if (modifyResult.Success)
            {
                nowUser.Save();
            }
            return modifyResult;
        }

        #endregion

        #region 修改用户状态

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="modifyUserStatus">用户状态修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyStatus(ModifyUserStatusParameter modifyUserStatus)
        {
            if (modifyUserStatus?.StatusInfos.IsNullOrEmpty() ?? true)
            {
                return Result.FailedResult("没有指定要修改状态的用户信息");
            }
            var userList = GetList(modifyUserStatus.StatusInfos.Keys);
            if (userList.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定要修改状态的用户信息");
            }
            foreach (var user in userList)
            {
                if (user == null || !modifyUserStatus.StatusInfos.TryGetValue(user.Id, out var newStatus))
                {
                    continue;
                }
                user.Status = newStatus;
                user.Save();
            }
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 验证用户名是否存在

        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="excludeId">检查除指定用户以外的用户信息</param>
        /// <returns>返回用户名是否存在</returns>
        public bool ExistName(string userName, long? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return false;
            }
            IQuery query = QueryManager.Create<UserEntity>(c => c.UserName == userName);
            if (excludeId.HasValue)
            {
                query.And<UserEntity>(c => c.Id != excludeId);
            }
            return userRepository.Exist(query);
        }

        #endregion
    }
}
