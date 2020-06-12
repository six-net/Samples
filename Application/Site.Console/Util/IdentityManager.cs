using EZNEW.Module.Sys;
using EZNEW.AppServiceContract.Sys;
using EZNEW.DTO.Sys.Query;
using EZNEW.Response;
using EZNEW.ViewModel.Sys.Request;
using EZNEW.Web.Security.Authentication;
using EZNEW.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EZNEW.Cache;
using EZNEW.Cache.Set.Request;
using EZNEW.Cache.Keys.Request;
using EZNEW.DependencyInjection;
using Microsoft.AspNetCore.Http;
using EZNEW.Web.Security.Authorization;

namespace Site.Console.Util
{
    public static class IdentityManager
    {
        static readonly IUserAppService UserService = null;

        static IdentityManager()
        {
            UserService = ContainerManager.Container.Resolve<IUserAppService>();
            AuthorizationManager.ConfigureAuthorizationVerify(AuthorizeManager.Authentication);
        }

        #region 登陆

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="loginInfo">登陆信息</param>
        /// <returns>登录结果</returns>
        public static Result Login(LoginViewModel loginInfo)
        {
            if (loginInfo == null)
            {
                return Result.FailedResult("登陆信息不完整");
            }
            if (!VerificationCodeHelper.CheckLoginCode(loginInfo.VerificationCode))
            {
                return Result.FailedResult("验证码错误");
            }
            var result = UserService.Login(new UserDto()
            {
                UserName = loginInfo.LoginName,
                Pwd = loginInfo.Pwd
            });
            if (result == null || !result.Success || result.Data == null)
            {
                return Result.FailedResult("用户名或密码错误");
            }
            SaveLoginCredential(result.Object);
            return Result.SuccessResult("登陆成功");
        }

        /// <summary>
        /// 保存登陆信息
        /// </summary>
        /// <param name="user">用户信息</param>
        static void SaveLoginCredential(UserDto user)
        {
            if (null == user)
            {
                return;
            }

            #region 记录登录凭据

            AuthenticationUser<long> authUser = new AuthenticationUser<long>()
            {
                Id = user.SysNo,
                Name = user.UserName,
                RealName = user.RealName,
                IsAdmin = user.SuperUser
            };
            HttpContextHelper.Current.SignInAsync(authUser, new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)

            }).GetAwaiter().GetResult();

            #endregion
        }

        #endregion

        #region 登出

        /// <summary>
        /// 登出
        /// </summary>
        public static void LoginOut()
        {
            HttpContextHelper.Current.SignOutAsync().Wait();
        }

        #endregion

        #region 登陆验证

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="context">登录上下文信息</param>
        /// <returns>登录验证是否通过</returns>
        public static async Task<bool> ValidatePrincipalAsync(CookieValidatePrincipalContext context)
        {
            var authUser = AuthenticationUser<long>.GetUserFromPrincipal(context.Principal);
            return await Task.FromResult(authUser != null).ConfigureAwait(false);
        }

        #endregion

        #region 获取登陆用户

        /// <summary>
        /// 获取登录用户
        /// </summary>
        /// <returns>登录用户信息</returns>
        public static AuthenticationUser<long> GetLoginUser()
        {
            return AuthenticationUser<long>.GetUserFromPrincipal(HttpContextHelper.Current.User);
        }

        #endregion
    }
}
