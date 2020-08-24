using System.Collections.Generic;
using EZNEW.Paging;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.Response;
using EZNEW.DTO.Sys.Filter;
using EZNEW.DTO.Sys;

namespace EZNEW.AppServiceContract.Sys
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface IUserAppService
    {
        #region 保存用户

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="saveUserDto">用户保存信息</param>
        /// <returns>返回用户保存结果</returns>
        Result<UserDto> SaveUser(SaveUserDto saveUserDto);

        #endregion

        #region 获取用户

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="filter">用户筛选信息</param>
        /// <returns>返回用户信息</returns>
        UserDto GetUser(UserFilterDto filter);

        #endregion

        #region 获取用户列表

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="filter">用户筛选信息</param>
        /// <returns>返回用户列表</returns>
        List<UserDto> GetUserList(UserFilterDto filter);

        #endregion

        #region 获取用户分页

        /// <summary>
        /// 获取用户分页
        /// </summary>
        /// <param name="filter">用户筛选信息</param>
        /// <returns>返回用户分页</returns>
        IPaging<UserDto> GetUserPaging(UserFilterDto filter);

        #endregion

        #region 删除用户

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="removeUserDto">用户删除信息</param>
        /// <returns>返回删除用户操作结果</returns>
        Result RemoveUser(RemoveUserDto removeUserDto);

        #endregion

        #region 用户登录

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginDto">登录信息</param>
        /// <returns>返回登录信息</returns>
        Result<UserDto> Login(LoginDto loginDto);

        #endregion

        #region 修改密码

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="modifyPasswordDto">密码修改信息</param>
        /// <returns>返回密码修改结果</returns>
        Result ModifyPassword(ModifyUserPasswordDto modifyPasswordDto);

        #endregion

        #region 修改用户状态

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="modifyUserStatusDto">用户状态修改信息</param>
        /// <returns>返回状态修改结果</returns>
        Result ModifyStatus(ModifyUserStatusDto modifyUserStatusDto);

        #endregion

        #region 修改用户绑定角色

        /// <summary>
        /// 修改用户绑定角色
        /// </summary>
        /// <param name="modifyUserRoleDto">用户角色修改信息</param>
        /// <returns>返回角色修改结果</returns>
        Result ModifyUserRole(ModifyUserRoleDto modifyUserRoleDto);

        #endregion

        #region 清除用户绑定的角色

        /// <summary>
        /// 清除用户绑定的角色
        /// </summary>
        /// <param name="userIds">用户系统编号</param>
        /// <returns>返回执行结果</returns>
        Result ClearRole(IEnumerable<long> userIds);

        #endregion
    }
}
