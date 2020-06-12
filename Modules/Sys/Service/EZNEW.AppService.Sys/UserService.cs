using System.Collections.Generic;
using EZNEW.BusinessContract.Sys;
using EZNEW.DTO.Sys.Query;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Query.Filter;
using EZNEW.AppServiceContract.Sys;
using EZNEW.Paging;
using EZNEW.Response;

namespace EZNEW.AppService.Sys
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class UserAppService : IUserAppService
    {
        IUserBusiness userBusiness = null;
        public UserAppService(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        #region 保存用户

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="saveInfo">保存信息</param>
        /// <returns></returns>
        public Result<UserDto> SaveUser(SaveUserCmdDto saveInfo)
        {
            return userBusiness.SaveUser(saveInfo);
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
            return userBusiness.GetUser(filter);
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
            return userBusiness.GetUserList(filter);
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
            return userBusiness.GetUserPaging(filter);
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
            return userBusiness.DeleteUser(deleteInfo);
        }

        #endregion

        #region 用户登录

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userDto">登录用户信息</param>
        /// <returns></returns>
        public Result<UserDto> Login(UserDto userDto)
        {
            return userBusiness.Login(userDto);
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
            return userBusiness.ModifyPassword(modifyInfo);
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
            return userBusiness.ModifyStatus(statusInfo);
        }

        #endregion

        #region 修改用户绑定角色

        /// <summary>
        /// 修改用户绑定角色
        /// </summary>
        /// <param name="bindInfo">绑定信息</param>
        public Result ModifyUserBindRole(ModifyUserBindRoleCmdDto bindInfo)
        {
            return userBusiness.ModifyUserBindRole(bindInfo);
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
            return userBusiness.ClearUserRole(userSysNos);
        }

        #endregion
    }
}
