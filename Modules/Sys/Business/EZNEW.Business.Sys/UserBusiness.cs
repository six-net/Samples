using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Develop.CQuery;
using EZNEW.Develop.UnitOfWork;
using EZNEW.Domain.Sys.Service;
using EZNEW.DTO.Sys.Query;
using EZNEW.Domain.Sys.Service.Param;
using EZNEW.DTO.Sys.Query.Filter;
using EZNEW.Query.Sys;
using EZNEW.BusinessContract.Sys;
using EZNEW.Domain.Sys.Model;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DependencyInjection;
using EZNEW.Response;
using EZNEW.Paging;

namespace EZNEW.Business.Sys
{
    /// <summary>
    /// 用户逻辑
    /// </summary>
    public class UserBusiness : IUserBusiness
    {
        static IUserService userService = ContainerManager.Resolve<IUserService>();
        static IUserRoleService userRoleService = ContainerManager.Resolve<IUserRoleService>();

        public UserBusiness()
        {
        }

        #region 保存用户

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="saveInfo">保存信息</param>
        /// <returns></returns>
        public Result<UserDto> SaveUser(SaveUserCmdDto saveInfo)
        {
            if (saveInfo == null || saveInfo.User == null)
            {
                return Result<UserDto>.FailedResult("没有指定任何要保存的用户信息");
            }
            using (var businessWork = WorkManager.Create())
            {
                var user = saveInfo.User.MapTo<User>();
                var userSaveResult = userService.SaveUser(user);
                if (!userSaveResult.Success)
                {
                    return Result<UserDto>.FailedResult(userSaveResult.Message);
                }
                var commitResult = businessWork.Commit();
                Result<UserDto> result = null;
                if (commitResult.EmptyResultOrSuccess)
                {
                    result = Result<UserDto>.SuccessResult("保存成功");
                    result.Data = userSaveResult.Data.MapTo<UserDto>();
                }
                else
                {
                    result = Result<UserDto>.FailedResult("保存失败");
                }
                return result;
            }
        }

        #endregion

        #region 获取用户

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="filter">筛选信息</param>
        /// <returns></returns>
        public UserDto GetUser(UserFilterDto filter)
        {
            IQuery query = CreateQueryObject(filter);
            return userService.GetUser(query).MapTo<UserDto>();
        }

        #endregion

        #region 获取用户列表

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="filter">筛选信息</param>
        /// <returns></returns>
        public List<UserDto> GetUserList(UserFilterDto filter)
        {
            var userList = userService.GetUserList(CreateQueryObject(filter));
            return userList.Select(c => c.MapTo<UserDto>()).ToList();
        }

        #endregion

        #region 获取用户分页

        /// <summary>
        /// 获取用户分页
        /// </summary>
        /// <param name="filter">筛选信息</param>
        /// <returns></returns>
        public IPaging<UserDto> GetUserPaging(UserFilterDto filter)
        {
            var userPaging = userService.GetUserPaging(CreateQueryObject(filter));
            return userPaging.ConvertTo<UserDto>();
        }

        #endregion

        #region 删除用户

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="deleteInfo">删除信息</param>
        /// <returns></returns>
        public Result DeleteUser(DeleteUserCmdDto deleteInfo)
        {
            if (deleteInfo == null || deleteInfo.UserIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何要删除的用户信息");
            }
            using (var businessWork = WorkManager.Create())
            {
                var deleteResult = userService.DeleteUser(deleteInfo.UserIds);
                if (!deleteResult.Success)
                {
                    return deleteResult;
                }
                var commitResult = businessWork.Commit();
                return commitResult.ExecutedSuccess ? Result.SuccessResult("删除成功") : Result.FailedResult("删除失败");
            }
        }

        #endregion

        #region 登陆

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userDto">登录用户信息</param>
        /// <returns></returns>
        public Result<UserDto> Login(UserDto userDto)
        {
            if (userDto == null)
            {
                return Result<UserDto>.FailedResult("用户登录信息为空");
            }
            var loginResult = userService.Login(new UserLogin()
            {
                UserName = userDto.UserName,
                Pwd = userDto.Pwd
            });
            if (!loginResult.Success)
            {
                return Result<UserDto>.FailedResult(loginResult.Message);
            }
            var result = Result<UserDto>.SuccessResult("登陆成功");
            result.Data = loginResult.Data.MapTo<UserDto>();
            return result;
        }

        #endregion

        #region 修改密码

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="modifyInfo">修改信息</param>
        /// <returns></returns>
        public Result ModifyPassword(ModifyPasswordCmdDto modifyInfo)
        {
            using (var businessWork = WorkManager.Create())
            {
                #region 参数判断

                if (modifyInfo == null)
                {
                    return Result.FailedResult("没有指定任何修改信息");
                }

                #endregion

                var modifyResult = userService.ModifyPassword(modifyInfo.MapTo<ModifyUserPassword>());
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var commitResult = businessWork.Commit();
                return commitResult.EmptyResultOrSuccess ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
        }

        #endregion

        #region 修改用户状态

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="statusInfo">状态信息</param>
        /// <returns>执行结果</returns>
        public Result ModifyStatus(ModifyUserStatusCmdDto statusInfo)
        {
            using (var businessWork = WorkManager.Create())
            {
                if (statusInfo == null || statusInfo.UserId <= 0)
                {
                    return Result.FailedResult("没有指定要修改状态的用户信息");
                }
                var modifyResult = userService.ModifyStatus(new UserStatusInfo()
                {
                    UserId = statusInfo.UserId,
                    Status = statusInfo.Status
                });
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var commitVal = businessWork.Commit();
                return commitVal.ExecutedSuccess ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
        }

        #endregion

        #region 修改用户绑定角色

        /// <summary>
        /// 修改用户绑定角色
        /// </summary>
        /// <param name="bindInfo">绑定信息</param>
        public Result ModifyUserBindRole(ModifyUserBindRoleCmdDto bindInfo)
        {
            if (bindInfo == null || (bindInfo.Binds.IsNullOrEmpty() && bindInfo.UnBinds.IsNullOrEmpty()))
            {
                return Result.FailedResult("没有指定任何要修改的绑定信息");
            }
            using (var businessWork = WorkManager.Create())
            {
                //解绑
                if (!bindInfo.UnBinds.IsNullOrEmpty())
                {
                    userRoleService.UnBindUserAndRole(bindInfo.UnBinds.Select(c => new Tuple<User, Role>(User.CreateUser(c.Item1?.SysNo ?? 0), Role.CreateRole(c.Item2?.SysNo ?? 0))).ToArray());
                }
                //绑定
                if (!bindInfo.Binds.IsNullOrEmpty())
                {
                    userRoleService.BindUserAndRole(bindInfo.Binds.Select(c => new Tuple<User, Role>(User.CreateUser(c.Item1?.SysNo ?? 0), Role.CreateRole(c.Item2?.SysNo ?? 0))).ToArray());
                }

                var commitResult = businessWork.Commit();
                return commitResult.ExecutedSuccess ? Result.SuccessResult("修改成功") : Result.FailedResult("修改失败");
            }
        }

        #endregion

        #region 清除用户绑定的角色

        /// <summary>
        /// 清除用户绑定的角色
        /// </summary>
        /// <param name="userSysNos">用户系统编号</param>
        /// <returns>执行结果</returns>
        public Result ClearUserRole(IEnumerable<long> userSysNos)
        {
            if (userSysNos.IsNullOrEmpty())
            {
                return Result.FailedResult("没有任何要操作的用户");
            }
            using (var work = WorkManager.Create())
            {
                var result = userRoleService.ClearUserRole(userSysNos);
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

        #region 根据查询条件生成查询对象

        /// <summary>
        /// 根据查询条件生成查询对象
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        public IQuery CreateQueryObject(UserFilterDto filter, bool useBaseFilter = false)
        {
            if (filter == null)
            {
                return null;
            }
            IQuery query = null;

            if (useBaseFilter)
            {
                query = QueryManager.Create<UserQuery>(filter);

                #region 数据筛选

                if (!filter.SysNos.IsNullOrEmpty())
                {
                    query.In<UserQuery>(c => c.SysNo, filter.SysNos);
                }
                if (!filter.NameMateKey.IsNullOrEmpty())
                {
                    query.And<UserQuery>(QueryOperator.OR, CriteriaOperator.Like, filter.NameMateKey, null, u => u.UserName, u => u.RealName);
                }
                if (!filter.UserName.IsNullOrEmpty())
                {
                    query.Like<UserQuery>(c => c.UserName, filter.UserName);
                }
                if (!filter.RealName.IsNullOrEmpty())
                {
                    query.Like<UserQuery>(c => c.RealName, filter.RealName);
                }
                if (!filter.Pwd.IsNullOrEmpty())
                {
                    query.Equal<UserQuery>(c => c.Pwd, filter.Pwd);
                }
                if (filter.UserType.HasValue)
                {
                    query.Equal<UserQuery>(c => c.UserType, filter.UserType.Value);
                }
                if (filter.Status.HasValue)
                {
                    query.Equal<UserQuery>(c => c.Status, filter.Status.Value);
                }
                if (!filter.Mobile.IsNullOrEmpty())
                {
                    query.Equal<UserQuery>(c => c.Contact.Mobile, filter.Mobile);
                }
                if (!filter.Email.IsNullOrEmpty())
                {
                    query.Equal<UserQuery>(c => c.Contact.Email, filter.Email);
                }
                if (!filter.QQ.IsNullOrEmpty())
                {
                    query.Equal<UserQuery>(c => c.Contact.QQ, filter.QQ);
                }
                if (!filter.ContactMateKey.IsNullOrEmpty())
                {
                    query.And<UserQuery>(QueryOperator.OR, CriteriaOperator.Like, filter.ContactMateKey, null, u => u.Contact.Mobile, u => u.Contact.Email, u => u.Contact.QQ);
                }
                if (filter.SuperUser.HasValue)
                {
                    query.Equal<UserQuery>(c => c.SuperUser, filter.SuperUser.Value);
                }
                if (filter.CreateDate.HasValue)
                {
                    query.Equal<UserQuery>(c => c.CreateDate, filter.CreateDate.Value);
                }
                if (filter.LastLoginDate.HasValue)
                {
                    query.Equal<UserQuery>(c => c.LastLoginDate, filter.LastLoginDate.Value);
                }

                #endregion
            }
            else
            {
                if (filter is AdminUserFilterDto)
                {
                    query = CreateAdminQueryObject(filter as AdminUserFilterDto);
                }
                else
                {
                    query = CreateQueryObject(filter, true);
                }
            }
            return query;
        }

        /// <summary>
        /// 管理用户查询对象
        /// </summary>
        /// <param name="adminUserFilter">管理用户筛选对象</param>
        /// <returns></returns>
        public IQuery CreateAdminQueryObject(AdminUserFilterDto adminUserFilter)
        {
            if (adminUserFilter == null)
            {
                return null;
            }
            IQuery userQuery = CreateQueryObject(adminUserFilter, true) ?? QueryManager.Create<UserQuery>();

            #region 角色筛选

            if (adminUserFilter.RoleFilter != null)
            {
                IQuery roleQuery = this.Instance<IRoleBusiness>().CreateQueryObject(adminUserFilter.RoleFilter);
                if (roleQuery != null && !roleQuery.Criterias.IsNullOrEmpty())
                {
                    roleQuery.AddQueryFields<RoleQuery>(c => c.SysNo);
                    IQuery userRoleQuery = QueryManager.Create<UserRoleQuery>();
                    userRoleQuery.And<UserRoleQuery>(u => u.RoleSysNo, CriteriaOperator.In, roleQuery);
                    userRoleQuery.AddQueryFields<UserRoleQuery>(u => u.UserSysNo);
                    userQuery.And<UserQuery>(c => c.SysNo, CriteriaOperator.In, userRoleQuery);
                }
            }

            #endregion

            return userQuery;
        }

        #endregion
    }
}
