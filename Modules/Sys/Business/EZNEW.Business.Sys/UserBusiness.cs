using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Response;
using EZNEW.Develop.UnitOfWork;
using EZNEW.Paging;
using EZNEW.Domain.Sys.Service;
using EZNEW.BusinessContract.Sys;
using EZNEW.Domain.Sys.Model;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DependencyInjection;
using EZNEW.DTO.Sys;
using EZNEW.DTO.Sys.Filter;
using EZNEW.Domain.Sys.Parameter.Filter;
using EZNEW.Domain.Sys.Parameter;

namespace EZNEW.Business.Sys
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
        /// <param name="saveUserDto">用户保存信息</param>
        /// <returns>返回用户保存结果</returns>
        public Result<UserDto> SaveUser(SaveUserDto saveUserDto)
        {
            if (saveUserDto?.User == null)
            {
                return Result<UserDto>.FailedResult("没有指定任何要保存的用户信息");
            }
            using (var businessWork = WorkManager.Create())
            {
                var user = saveUserDto.User.MapTo<User>();
                var userSaveResult = userService.Save(user);
                if (!userSaveResult.Success)
                {
                    return Result<UserDto>.FailedResult(userSaveResult.Message);
                }
                var commitResult = businessWork.Commit();
                Result<UserDto> result;
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
        /// <param name="userFilterDto">用户筛选信息</param>
        /// <returns>返回用户信息</returns>
        public UserDto GetUser(UserFilterDto userFilterDto)
        {
            return userService.Get(userFilterDto?.ConvertToFilter()).MapTo<UserDto>();
        }

        #endregion

        #region 获取用户列表

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="userFilterDto">用户筛选信息</param>
        /// <returns>返回用户列表</returns>
        public List<UserDto> GetUserList(UserFilterDto userFilterDto)
        {
            var userList = userService.GetList(userFilterDto?.ConvertToFilter());
            return userList.Select(c => c.MapTo<UserDto>()).ToList();
        }

        #endregion

        #region 获取用户分页

        /// <summary>
        /// 获取用户分页
        /// </summary>
        /// <param name="userFilterDto">用户筛选信息</param>
        /// <returns>返回用户分页</returns>
        public IPaging<UserDto> GetUserPaging(UserFilterDto userFilterDto)
        {
            var userPaging = userService.GetPaging(userFilterDto?.ConvertToFilter());
            return userPaging.ConvertTo<UserDto>();
        }

        #endregion

        #region 删除用户

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="deleteUserDto">删除用户信息</param>
        /// <returns>返回用户删除结果</returns>
        public Result RemoveUser(RemoveUserDto deleteUserDto)
        {
            if (deleteUserDto?.Ids.IsNullOrEmpty() ?? true)
            {
                return Result.FailedResult("没有指定任何要删除的用户信息");
            }
            using (var businessWork = WorkManager.Create())
            {
                var deleteResult = userService.Remove(deleteUserDto.Ids);
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
        /// <param name="loginDto">登录信息</param>
        /// <returns>返回登录结果</returns>
        public Result<UserDto> Login(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return Result<UserDto>.FailedResult("用户登录信息为空");
            }
            var loginResult = userService.Login(loginDto.MapTo<LoginParameter>());
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
        /// <param name="modifyPasswordDto">密码修改信息</param>
        /// <returns>返回密码修改结果</returns>
        public Result ModifyPassword(ModifyUserPasswordDto modifyPasswordDto)
        {
            #region 参数判断

            if (modifyPasswordDto == null)
            {
                return Result.FailedResult("没有指定任何修改信息");
            }

            #endregion

            using (var businessWork = WorkManager.Create())
            {
                var modifyResult = userService.ModifyPassword(modifyPasswordDto.MapTo<ModifyUserPasswordParameter>());
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
        /// <param name="modifyUserStatusDto">用户状态修改信息</param>
        /// <returns>返回状态修改执行结果</returns>
        public Result ModifyStatus(ModifyUserStatusDto modifyUserStatusDto)
        {
            if (modifyUserStatusDto?.StatusInfos.IsNullOrEmpty() ?? true)
            {
                return Result.FailedResult("没有指定要修改状态的用户信息");
            }
            using (var businessWork = WorkManager.Create())
            {
                var modifyResult = userService.ModifyStatus(modifyUserStatusDto.MapTo<ModifyUserStatusParameter>());
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
        /// <param name="modifyUserRoleDto">用户角色修改信息</param>
        /// <returns>返回用户角色修改结果</returns>
        public Result ModifyUserRole(ModifyUserRoleDto modifyUserRoleDto)
        {
            using (var businessWork = WorkManager.Create())
            {
                var result = userRoleService.Modify(modifyUserRoleDto.MapTo<ModifyUserRoleParameter>());
                if (!result.Success)
                {
                    return result;
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
