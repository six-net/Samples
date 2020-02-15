using EZNEW.Develop.CQuery;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Domain.Sys.Service.Param;
using EZNEW.Query.Sys;
using EZNEW.Framework;
using EZNEW.Framework.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Framework.Extension;
using EZNEW.Framework.ExpressionUtil;
using EZNEW.Framework.Paging;
using EZNEW.Application.Identity.User;
using EZNEW.Framework.Response;

namespace EZNEW.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class UserService : IUserService
    {
        static IUserRepository userRepository = ContainerManager.Container.Resolve<IUserRepository>();
        static IUserRoleRepository userRoleRepository = ContainerManager.Container.Resolve<IUserRoleRepository>();

        #region 保存用户

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Result<User> SaveUser(User user)
        {
            if (user == null)
            {
                return Result<User>.FailedResult("用户信息为空");
            }
            Result<User> result = null;
            if (user.SysNo < 1)
            {
                result = AddUser(user);
            }
            else
            {
                result = UpdateUser(user);
            }
            return result;
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        Result<User> AddUser(User user)
        {
            user.ModifyPassword(user.Pwd);//密码加密
            user.Save();
            var result = Result<User>.SuccessResult("保存成功");
            result.Data = user;
            return result;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        Result<User> UpdateUser(User user)
        {
            var nowUser = GetUser(user.SysNo);
            if (nowUser == null)
            {
                return Result<User>.FailedResult("请指定要编辑的用户");
            }
            var excludeModifyPropertys = ExpressionHelper.GetExpressionPropertyNames<User>(u =>
              u.SysNo
            , u => u.Pwd
            , u => u.UserType
            , u => u.CreateDate
            , u => u.LastLoginDate
            , u => u.UserName
            , u => u.SuperUser
            );
            if (nowUser.SuperUser)
            {
                user.Status = UserStatus.正常;
            }
            nowUser.ModifyFromOtherUser(user, excludeModifyPropertys);//更新用户
            nowUser.Save();
            var result = Result<User>.SuccessResult("更新成功");
            result.Data = nowUser;
            return result;
        }

        #endregion

        #region 删除账户

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="users">要删除的用户信息</param>
        /// <returns></returns>
        public Result DeleteUser(IEnumerable<User> users)
        {
            if (users.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何要删除的用户");
            }
            userRepository.Remove(users.ToArray());
            return Result.SuccessResult("删除成功");
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userIds">用户编号</param>
        /// <returns></returns>
        public Result DeleteUser(IEnumerable<long> userIds)
        {
            if (userIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定要删除的用户");
            }
            IEnumerable<User> users = userIds.Select(c => User.CreateUser(c)).ToList();
            return DeleteUser(users);
        }

        #endregion

        #region 登陆

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user">登录用户信息</param>
        /// <returns></returns>
        public Result<User> Login(UserLogin loginInfo)
        {
            var userQuery = QueryFactory.Create<UserQuery>(u => u.UserName == loginInfo.UserName && u.Pwd == User.PasswordEncryption(loginInfo.Pwd));
            User user = userRepository.Get(userQuery);
            if (user == null || !user.AllowLogin())
            {
                return Result<User>.FailedResult("登陆失败");
            }
            var result = Result<User>.SuccessResult("登陆成功");
            result.Data = user;
            return result;
        }

        #endregion

        #region 获取用户信息

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        public User GetUser(IQuery query)
        {
            User user = userRepository.Get(query);
            return user;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public User GetUser(long userId)
        {
            if (userId <= 0)
            {
                return null;
            }
            IQuery query = QueryFactory.Create<UserQuery>(c => c.SysNo == userId);
            return GetUser(query);
        }

        #endregion

        #region 获取用户列表

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="query">筛选条件</param>
        /// <returns></returns>
        public List<User> GetUserList(IQuery query)
        {
            return userRepository.GetList(query);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="userIds">用户编号</param>
        /// <returns></returns>
        public List<User> GetUserList(IEnumerable<long> userIds)
        {
            if (userIds.IsNullOrEmpty())
            {
                return new List<User>(0);
            }
            IQuery query = QueryFactory.Create<UserQuery>(c => userIds.Contains(c.SysNo));
            return GetUserList(query);
        }

        #endregion

        #region 获取用户分页

        /// <summary>
        /// 获取用户分页
        /// </summary>
        /// <param name="query">筛选信息</param>
        /// <returns></returns>
        public IPaging<User> GetUserPaging(IQuery query)
        {
            return userRepository.GetPaging(query);
        }

        #endregion

        #region 修改密码

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="modifyInfo">修改信息</param>
        /// <returns></returns>
        public Result ModifyPassword(ModifyUserPassword modifyInfo)
        {
            #region 参数判断

            if (modifyInfo == null)
            {
                return Result.FailedResult("密码修改信息为空");
            }

            #endregion

            //获取用户
            IQuery query = QueryFactory.Create<UserQuery>(u => u.SysNo == modifyInfo.SysNo);
            User nowUser = userRepository.Get(query);
            if (nowUser == null)
            {
                return Result.FailedResult("用户不存在");
            }
            //验证当前密码
            if (modifyInfo.CheckOldPassword && nowUser.Pwd != User.PasswordEncryption(modifyInfo.NowPassword))
            {
                return Result.FailedResult("当前密码不正确");
            }
            nowUser.ModifyPassword(modifyInfo.NewPassword);//设置新密码
            nowUser.Save();
            return Result.SuccessResult("密码修改成功");
        }

        #endregion

        #region 修改用户状态

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="userStatus">状态信息</param>
        /// <returns>执行结果</returns>
        public Result ModifyStatus(params UserStatusInfo[] userStatus)
        {
            if (userStatus.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定要修改状态的用户信息");
            }
            var userIds = userStatus.Select(c => c.UserId).Distinct().ToList();
            var userList = GetUserList(userIds);
            if (userList.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定要修改状态的用户信息");
            }
            foreach (var user in userList)
            {
                if (user == null)
                {
                    continue;
                }
                var newStatusInfo = userStatus.FirstOrDefault(c => c.UserId == user.SysNo);
                if (newStatusInfo == null)
                {
                    continue;
                }
                user.Status = newStatusInfo.Status;
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
        /// <param name="excludeUserId">检查除指定用户以外的用户信息</param>
        /// <returns></returns>
        public bool ExistUserName(string userName, long? excludeUserId = null)
        {
            if (userName.IsNullOrEmpty())
            {
                return false;
            }
            IQuery query = QueryFactory.Create<UserQuery>(c => c.UserName == userName);
            if (excludeUserId.HasValue)
            {
                query.And<UserQuery>(c => c.SysNo != excludeUserId);
            }
            return userRepository.Exist(query);
        }

        #endregion
    }
}
