using EZNEW.Module.Sys;
using EZNEW.AppServiceContract.Sys;
using EZNEW.Cache;
using EZNEW.Cache.Keys.Request;
using EZNEW.Cache.Set.Request;
using EZNEW.Cache.Set.Response;
using EZNEW.DependencyInjection;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Query;
using EZNEW.Web.Security.Authentication;
using EZNEW.Web.Security.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Site.Console.Util
{
    /// <summary>
    /// 授权管理
    /// </summary>
    public static class AuthorizeManager
    {
        private static readonly IAuthAppService AuthAppService = null;

        static AuthorizeManager()
        {
            AuthAppService = ContainerManager.Resolve<IAuthAppService>();
        }

        #region 授权验证

        /// <summary>
        /// 授权验证
        /// </summary>
        /// <param name="operation">授权操作</param>
        /// <returns></returns>
        public static bool Authorization(AuthenticationUser<long> user, AuthorityOperationCmdDto operation)
        {
            //TODO:默认不做授权认证
            return true;
        }

        /// <summary>
        /// 操作授权验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static VerifyAuthorizationResult Authentication(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                return VerifyAuthorizationResult.ChallengeResult();
            }

            #region 操作信息

            string controllerName = context.RouteData.Values["controller"].ToString().ToUpper();
            string actionName = context.RouteData.Values["action"].ToString().ToUpper();
            string methodName = context.HttpContext.Request.Method;
            AuthorityOperationCmdDto operation = new AuthorityOperationCmdDto()
            {
                ControllerCode = controllerName,
                ActionCode = actionName
            };

            #endregion

            //登陆用户
            var loginUser = IdentityManager.GetLoginUser();
            if (loginUser == null)
            {
                return VerifyAuthorizationResult.ChallengeResult();
            }
            var allowAccess = Authorization(loginUser, operation);
            return allowAccess ? VerifyAuthorizationResult.SuccessResult() : VerifyAuthorizationResult.ForbidResult();
        }

        /// <summary>
        /// 授权验证
        /// </summary>
        /// <param name="request">认证授权信息</param>
        /// <returns></returns>
        public static VerifyAuthorizationResult Authentication(VerifyAuthorizationOption request)
        {
            if (request == null)
            {
                return VerifyAuthorizationResult.ForbidResult();
            }
            var operation = new AuthorityOperationCmdDto()
            {
                ActionCode = request.ActionCode,
                ControllerCode = request.ControllerCode
            };
            var user = AuthenticationUser<long>.GetUserFromClaims(request.Claims?.Select(c => new Claim(c.Key, c.Value)).ToList());
            var allowAccess = Authorization(user, operation);
            return new VerifyAuthorizationResult()
            {
                Status = allowAccess ? AuthorizationVerificationStatus.Success : AuthorizationVerificationStatus.Forbid
            };
        }

        #endregion
    }
}
