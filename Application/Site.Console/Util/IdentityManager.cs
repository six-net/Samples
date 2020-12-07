using System;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using EZNEW.ViewModel.Sys;
using EZNEW.AppServiceContract.Sys;
using EZNEW.DependencyInjection;
using EZNEW.Response;
using EZNEW.Web.Security.Authentication;
using EZNEW.Web.Utility;
using EZNEW.DTO.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.Web.Security.Authorization;

namespace Site.Console.Util
{
    public static class IdentityManager
    {
        static readonly IUserAppService userService = null;
        static readonly IOperationAppService operationAppService = null;

        static IdentityManager()
        {
            userService = ContainerManager.Resolve<IUserAppService>();
            operationAppService = ContainerManager.Resolve<IOperationAppService>();
            AuthorizationManager.ConfigureAuthorization(CheckAuthorization);
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
            var result = userService.Login(new LoginDto()
            {
                UserName = loginInfo.LoginName,
                Password = loginInfo.Password
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
                Id = user.Id,
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

        #region 授权验证

        /// <summary>
        /// 授权验证
        /// </summary>
        /// <param name="operation">授权操作</param>
        /// <returns></returns>
        public static bool CheckAuthorization(AuthenticationUser<long> user, OperationDto operation)
        {
            if (operation == null || user == null)
            {
                return false;
            }
            if (user.IsAdmin)
            {
                return true;
            }
            var checkAuthDto = new CheckAuthorizationDto()
            {
                UserId = user.Id,
                Operation = operation
            };
            return operationAppService.CheckAuthorization(checkAuthDto);
        }

        /// <summary>
        /// 操作授权验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static AuthorizeResult CheckAuthorization(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                return AuthorizeResult.ChallengeResult();
            }

            #region 操作信息

            string controllerName = context.RouteData.Values["controller"].ToString();
            string actionName = context.RouteData.Values["action"].ToString();
            string methodName = context.HttpContext.Request.Method;
            OperationDto operation = new OperationDto()
            {
                ControllerCode = controllerName,
                ActionCode = actionName
            };

            #endregion

            //登陆用户
            var loginUser = IdentityManager.GetLoginUser();
            if (loginUser == null)
            {
                return AuthorizeResult.ChallengeResult();
            }
            var allowAccess = CheckAuthorization(loginUser, operation);
            return allowAccess ? AuthorizeResult.SuccessResult() : AuthorizeResult.ForbidResult();
        }

        /// <summary>
        /// 授权验证
        /// </summary>
        /// <param name="request">认证授权信息</param>
        /// <returns></returns>
        public static AuthorizeResult CheckAuthorization(AuthorizeOptions request)
        {
            if (request == null)
            {
                return AuthorizeResult.ForbidResult();
            }
            var operation = new OperationDto()
            {
                ActionCode = request.Action,
                ControllerCode = request.Controller
            };
            var user = AuthenticationUser<long>.GetUserFromClaims(request.Claims?.Select(c => new Claim(c.Key, c.Value)).ToList());
            var allowAccess = CheckAuthorization(user, operation);
            return new AuthorizeResult()
            {
                Status = allowAccess ? AuthorizationStatus.Success : AuthorizationStatus.Forbid
            };
        }

        #endregion
    }
}
