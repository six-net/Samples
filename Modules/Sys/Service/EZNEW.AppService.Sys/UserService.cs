using System.Collections.Generic;
using EZNEW.BusinessContract.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.Paging;
using EZNEW.AppServiceContract.Sys;
using EZNEW.Response;
using EZNEW.DTO.Sys.Filter;
using EZNEW.DTO.Sys;

namespace EZNEW.AppService.Sys
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class UserAppService : IUserAppService
    {
        /// <summary>
        /// 用户业务
        /// </summary>
        readonly IUserBusiness userBusiness = null;

        public UserAppService(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        #region 保存用户

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="saveUserDto">用户保存信息</param>
        /// <returns>返回执行结果</returns>
        public Result<UserDto> SaveUser(SaveUserDto saveUserDto)
        {
            return userBusiness.SaveUser(saveUserDto);
        }

        #endregion

        #region 获取用户

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="filter">用户筛选信息</param>
        /// <returns>返回用户</returns>
        public UserDto GetUser(UserFilterDto filter)
        {
            return userBusiness.GetUser(filter);
        }

        #endregion

        #region 获取用户列表

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="filter">用户筛选信息</param>
        /// <returns>返回用户列表</returns>
        public List<UserDto> GetUserList(UserFilterDto filter)
        {
            return userBusiness.GetUserList(filter);
        }

        #endregion

        #region 获取用户分页

        /// <summary>
        /// 获取用户分页
        /// </summary>
        /// <param name="filter">用户筛选信息</param>
        /// <returns>返回用户分页</returns>
        public IPaging<UserDto> GetUserPaging(UserFilterDto filter)
        {
            return userBusiness.GetUserPaging(filter);
        }

        #endregion

        #region 删除用户

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="removeUserDto">删除信息</param>
        /// <returns>返回执行结果</returns>
        public Result RemoveUser(RemoveUserDto removeUserDto)
        {
            return userBusiness.RemoveUser(removeUserDto);
        }

        #endregion

        #region 用户登录

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginDto">登录用户信息</param>
        /// <returns>返回登录结果</returns>
        public Result<UserDto> Login(LoginDto loginDto)
        {
            return userBusiness.Login(loginDto);
        }

        #endregion

        #region 修改密码

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="modifyPasswordDto">用户密码修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyPassword(ModifyUserPasswordDto modifyPasswordDto)
        {
            return userBusiness.ModifyPassword(modifyPasswordDto);
        }

        #endregion

        #region 修改用户状态

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="modifyUserStatusDto">用户状态修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyStatus(ModifyUserStatusDto modifyUserStatusDto)
        {
            return userBusiness.ModifyStatus(modifyUserStatusDto);
        }

        #endregion

        #region 修改用户绑定角色

        /// <summary>
        /// 修改用户绑定角色
        /// </summary>
        /// <param name="modifyUserRoleDto">用户角色修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyUserRole(ModifyUserRoleDto modifyUserRoleDto)
        {
            return userBusiness.ModifyUserRole(modifyUserRoleDto);
        }

        #endregion

        #region 清除用户绑定的角色

        /// <summary>
        /// 清除用户绑定的角色
        /// </summary>
        /// <param name="userIds">用户系统编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearRole(IEnumerable<long> userIds)
        {
            return userBusiness.ClearRole(userIds);
        }

        #endregion
    }
}
