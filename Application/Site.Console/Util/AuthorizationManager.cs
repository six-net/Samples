using EZNEW.Application.Identity.Auth;
using EZNEW.AppServiceContract.Sys;
using EZNEW.Cache;
using EZNEW.Cache.Keys.Request;
using EZNEW.Cache.Set.Request;
using EZNEW.Cache.Set.Response;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Query;
using EZNEW.DTO.Sys.Query.Filter;
using EZNEW.Framework.Extension;
using EZNEW.Framework.IoC;
using EZNEW.Web.Security.Authentication;
using EZNEW.Web.Security.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Timers;

namespace Site.Console.Util
{
    /// <summary>
    /// 授权管理
    /// </summary>
    public static class AuthorizationManager
    {
        static IAuthAppService AuthAppService = null;

        static AuthorizationManager()
        {
            AuthAppService = ContainerManager.Container.Resolve<IAuthAppService>();
        }

        #region 移除用户授权

        /// <summary>
        /// 移除用户授权
        /// </summary>
        /// <param name="userId">用户编号</param>
        public static void RemoveUserAuthorize(long userId)
        {
            var cacheKey = CacheUtil.GetUserAuthOperationCacheKey(userId.ToString());
            CacheManager.Keys.Delete(new DeleteOption()
            {
                Keys = new List<CacheKey>() { cacheKey }
            });
        }

        #endregion

        #region 授权验证

        /// <summary>
        /// 授权验证
        /// </summary>
        /// <param name="operation">授权操作</param>
        /// <returns></returns>
        public static async Task<bool> AuthorizationAsync(AuthenticationUser<long> user, AuthorityOperationCmdDto operation)
        {
            if (operation == null || user == null)
            {
                return false;
            }
            if (user.IsAdmin)
            {
                return true;
            }

            #region 授权操作判断

            string operationValue = $"{operation.ControllerCode}/{operation.ActionCode}";
            var operationCacheKey = CacheUtil.GetOperationCacheKey(operationValue);
            var nowOperation = CacheManager.GetData<AuthorityOperationDto>(operationCacheKey);
            if (nowOperation == null || nowOperation.Status == AuthorityOperationStatus.关闭)
            {
                return false;
            }
            if (nowOperation.AuthorizeType == AuthorityOperationAuthorizeType.无限制)
            {
                return true;
            }

            #endregion

            #region 授权操作分组判断

            var groupKey = nowOperation.Group?.SysNo.ToString() ?? string.Empty;
            if (groupKey.IsNullOrEmpty())
            {
                return false;
            }
            var groupCacheKey = CacheUtil.GetOperationGroupCacheKey(groupKey);
            var nowGroup = CacheManager.GetData<AuthorityOperationGroupDto>(groupCacheKey);
            if (nowGroup == null || nowGroup.Status == AuthorityOperationGroupStatus.关闭)
            {
                return false;
            }
            while (nowGroup.Level > 1)
            {
                var parentGroupKey = nowGroup.Parent?.SysNo.ToString() ?? string.Empty;
                if (parentGroupKey.IsNullOrEmpty())
                {
                    return false;
                }
                var parentGroupCacheKey = CacheUtil.GetOperationGroupCacheKey(parentGroupKey);
                var nowParentGroup = CacheManager.GetData<AuthorityOperationGroupDto>(parentGroupCacheKey);
                nowGroup = nowParentGroup;
                if (nowGroup == null || nowGroup.Status == AuthorityOperationGroupStatus.关闭)
                {
                    return false;
                }
            }

            #endregion

            var cacheKey = CacheUtil.GetUserAuthOperationCacheKey(user.Id.ToString());
            var existResult = CacheManager.Set.Contains(new SetContainsOption()
            {
                Key = cacheKey,
                Value = operationValue
            })?.Responses ?? new List<SetContainsResponse>(0);
            var hasOperation = existResult.IsNullOrEmpty() ? false : (existResult.FirstOrDefault()?.ContainsValue ?? false);
            if (!hasOperation)
            {
                return false;
            }
            return await Task.FromResult(true);
        }

        /// <summary>
        /// 操作授权验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<AuthorizeVerifyResult> AuthenticationAsync(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                return AuthorizeVerifyResult.ChallengeResult();
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
                return AuthorizeVerifyResult.ChallengeResult();
            }
            var allowAccess = await AuthorizationAsync(loginUser, operation).ConfigureAwait(false);
            return allowAccess ? AuthorizeVerifyResult.SuccessResult() : AuthorizeVerifyResult.ForbidResult();
        }

        /// <summary>
        /// 授权验证
        /// </summary>
        /// <param name="request">认证授权信息</param>
        /// <returns></returns>
        public static async Task<AuthorizeVerifyResult> AuthenticationAsync(AuthorizeVerifyRequest request)
        {
            if (request == null)
            {
                return AuthorizeVerifyResult.ForbidResult();
            }
            var operation = new AuthorityOperationCmdDto()
            {
                ActionCode = request.ActionCode,
                ControllerCode = request.ControllerCode
            };
            var user = AuthenticationUser<long>.GetUserFromClaims(request.Claims?.Select(c => new Claim(c.Key, c.Value)).ToList());
            var allowAccess = await AuthorizationAsync(user, operation).ConfigureAwait(false);
            return new AuthorizeVerifyResult()
            {
                VerifyValue = allowAccess ? AuthorizeVerifyValue.Success : AuthorizeVerifyValue.Forbid
            };
        }

        #endregion
    }
}
