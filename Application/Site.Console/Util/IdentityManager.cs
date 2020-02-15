using EZNEW.Application.Identity.Auth;
using EZNEW.Application.Identity.User;
using EZNEW.AppServiceContract.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Query;
using EZNEW.DTO.Sys.Query.Filter;
using EZNEW.Framework.Extension;
using EZNEW.Framework.IoC;
using EZNEW.Framework.Response;
using EZNEW.ViewModel.Sys.Request;
using EZNEW.Web.Security.Authentication;
using EZNEW.Web.Security.Authentication.Cookie;
using EZNEW.Web.Security.Authorization;
using EZNEW.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using EZNEW.Cache;
using EZNEW.Cache.Set.Request;
using EZNEW.Cache.Keys.Request;

namespace Site.Console.Util
{
    public static class IdentityManager
    {
        static IUserAppService UserService = null;

        static IdentityManager()
        {
            UserService = ContainerManager.Container.Resolve<IUserAppService>();
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
            #region 记录登录用户,不记录超级管理员

            if (!user.SuperUser)
            {
                CacheManager.Set.Add(new SetAddOption()
                {
                    Key = CacheUtil.AllLoginUserCacheKey,
                    Value = user.SysNo.ToString()
                });
                var userCacheKey = CacheUtil.GetUserCacheKey(user.SysNo.ToString());
                CacheManager.SetDataByRelativeExpiration(userCacheKey, user, TimeSpan.FromHours(1), true);
                CacheDataManager.RefreshLoginUser(user.SysNo, user.SuperUser);
            }

            #endregion

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
            var loginUser = GetLoginUser();
            if (loginUser == null)
            {
                return;
            }

            #region 移除登录记录

            if (!loginUser.IsAdmin)
            {
                var userId = loginUser.Id.ToString();
                //移除登录记录
                CacheManager.Set.Remove(new SetRemoveOption()
                {
                    Key = CacheUtil.AllLoginUserCacheKey,
                    RemoveValues = new List<string>()
                    {
                        userId
                    }
                });
                //移除登录用户信息
                var userCacheKey = CacheUtil.GetUserCacheKey(userId);
                CacheManager.Keys.Delete(new DeleteOption()
                {
                    Keys = new List<CacheKey>()
                    {
                        userCacheKey
                    }
                });
                //移除用户授权
                AuthorizationManager.RemoveUserAuthorize(loginUser.Id);
            }

            #endregion

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
            #region 登录凭据

            var authUser = AuthenticationUser<long>.GetUserFromPrincipal(context.Principal);
            if (authUser == null)
            {
                return await Task.FromResult(false).ConfigureAwait(false);
            }
            if (authUser.IsAdmin)
            {
                return await Task.FromResult(true).ConfigureAwait(false);
            }

            #endregion

            #region 登录用户判断

            var userCacheKey = CacheUtil.GetUserCacheKey(authUser.Id.ToString());
            var userData = CacheManager.GetData<UserDto>(userCacheKey);
            if (userData == null || userData.Status != UserStatus.正常)
            {
                return await Task.FromResult(false).ConfigureAwait(false);
            }
            CacheManager.SetDataByRelativeExpiration(userCacheKey, userData, TimeSpan.FromHours(1), true);

            #endregion

            return await Task.FromResult(true).ConfigureAwait(false);
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

        #region 获取所有登录用户

        /// <summary>
        /// 获取所有登录用户编号
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllLoginUserIds()
        {
            return CacheManager.Set.Members(new SetMembersOption()
            {
                Key = CacheUtil.AllLoginUserCacheKey
            })?.Responses?[0].Members ?? new List<string>(0);
        }

        #endregion
    }
}
