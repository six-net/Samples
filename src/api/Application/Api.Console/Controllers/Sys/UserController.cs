using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EZNEWApp.Module.Sys;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEW.Web.Mvc;
using EZNEW.Web.Security.Authorization;
using EZNEWApp.AppServiceContract.Sys;
using EZNEWApp.Domain.Sys.Parameter.Filter;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using Api.Console.Model.Response;
using Microsoft.AspNetCore.Http;

namespace Api.Console.Controllers.Sys
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserController : ApiBaseController
    {
        readonly IUserAppService userAppService;

        public UserController(IUserAppService userAppService)
        {
            this.userAppService = userAppService;
        }

        #region 获取登录用户信息

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="loginRequest">登录请求参数</param>
        /// <returns>返回登录响应信息</returns>
        [HttpGet]
        [Route("current/info")]
        public Result<GetLoginUserInfoResponse> GetLoginUserInfo()
        {
            return Result<GetLoginUserInfoResponse>.SuccessResult(new GetLoginUserInfoResponse()
            {
                Roles = new List<string>() { "admin", User?.Name ?? string.Empty },
                Permissions = new List<string>() { "admin", "READ", "WRITE", "DELETE" },
                UserName = "lidingbin",
                Avatar = "https://portrait.gitee.com/uploads/avatars/user/1755/5266797_eznew_1578983509.png!avatar60"
            });
        }

        #endregion

        #region 查询用户

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="filter">查询参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("query")]
        public PagingInfo<User> QueryUser(UserFilter filter)
        {
            filter.UserType = UserType.Management;
            return userAppService.GetUserPaging(filter);
        }

        #endregion

        #region 获取用户配置

        /// <summary>
        /// 获取用户配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("config")]
        public GetUserConfigurationResponse GetUserConfiguration()
        {
            return new GetUserConfigurationResponse()
            {
                StatusCollection = UserStatus.Enable.GetEnumKeyValueCollection()
            };
        }

        #endregion

        #region 保存用户

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("save")]
        public Result<User> SaveUser(User user)
        {
            user.UserType = UserType.Management;
            SaveUserParameter saveUserParameter = new SaveUserParameter()
            {
                User = user
            };
            return  userAppService.SaveUser(saveUserParameter);
        }

        #endregion

        #region 删除用户

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="removeUserParameter">删除用户参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public ActionResult RemoveUser(RemoveUserParameter removeUserParameter)
        {
            Result result = userAppService.RemoveUser(removeUserParameter);
            return Json(result);
        }

        #endregion

        #region 检查登陆名

        /// <summary>
        /// 检查登录名
        /// </summary>
        /// <param name="existUserNameParameter">参数信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("check-user-name")]
        public bool CheckUserName(ExistUserNameParameter existUserNameParameter)
        {
            return !userAppService.ExistUserName(existUserNameParameter);
        }

        #endregion
    }
}
