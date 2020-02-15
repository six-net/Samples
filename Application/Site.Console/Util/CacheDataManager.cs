using EZNEW.AppServiceContract.Sys;
using EZNEW.Cache;
using EZNEW.Cache.Keys.Request;
using EZNEW.Cache.Set.Request;
using EZNEW.DTO.Sys.Query;
using EZNEW.DTO.Sys.Query.Filter;
using EZNEW.Framework.Extension;
using EZNEW.Framework.IoC;
using EZNEW.Web.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Site.Console.Util
{
    public static class CacheDataManager
    {
        static Timer Timer = null;
        static double RefreshAuthInterval = 1 * 60 * 1000;//1 min
        static IRoleAppService RoleAppService = null;
        static IAuthAppService AuthAppService = null;

        static CacheDataManager()
        {
            RoleAppService = ContainerManager.Resolve<IRoleAppService>();
            AuthAppService = ContainerManager.Resolve<IAuthAppService>();
            AuthorizeManager.AuthorizeVerifyProcessAsync = AuthorizationManager.AuthenticationAsync;
            Timer = new Timer(RefreshAuthInterval);
            Timer.AutoReset = true;
            Timer.Enabled = true;
            Timer.Elapsed += Timer_Elapsed;
            Timer.Start();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            InitData();
            RefreshAllLoginUsers();
        }

        #region 刷新登录用户信息

        public static void RefreshAllLoginUsers()
        {
            var loginUsers = IdentityManager.GetAllLoginUserIds();
            loginUsers?.ForEach(u =>
            {
                if (long.TryParse(u, out var uid) && uid > 0)
                {
                    RefreshLoginUser(uid, false);
                }
            });
        }

        /// <summary>
        /// 刷新用户登录信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="superAdmin">超级用户</param>
        public static void RefreshLoginUser(long userId, bool superAdmin = false)
        {
            if (userId < 1 || superAdmin)
            {
                return;
            }

            #region 判断登录信息

            var userCacheKey = CacheUtil.GetUserCacheKey(userId.ToString());
            var userData = CacheManager.GetData<UserDto>(userCacheKey);
            if (userData == null)
            {
                CacheManager.Set.Remove(new SetRemoveOption()
                {
                    Key = CacheUtil.AllLoginUserCacheKey,
                    RemoveValues = new List<string>(1) { userId.ToString() }
                });
                return;
            }

            #endregion

            #region 刷新授权信息

            UserOperationFilterDto operationFilter = new UserOperationFilterDto()
            {
                UserFilter = new UserFilterDto()
                {
                    SysNos = new List<long>(1) { userId }
                }
            };
            var operations = AuthAppService.GetAuthorityOperationList(operationFilter);
            if (operations.IsNullOrEmpty())
            {
                return;
            }
            CacheKey userAuthKey = CacheUtil.GetUserAuthOperationCacheKey(userId.ToString());
            CacheManager.Keys.Delete(new DeleteOption()
            {
                Keys = new List<CacheKey>()
                {
                    userAuthKey
                }
            });
            operations.ForEach(c =>
            {
                CacheManager.Set.Add(new SetAddOption()
                {
                    Key = userAuthKey,
                    Value = $"{c.ControllerCode}/{c.ActionCode}"
                });
            });

            #endregion
        }

        #endregion

        #region 初始化数据

        public static void InitData()
        {
            InitRoleData();
            InitAuthOperationGroup();
            InitAuthOperation();
        }

        #endregion

        #region 角色

        #region 初始化角色数据

        static void InitRoleData()
        {
            var roles = RoleAppService.GetRoleList(null);
            roles?.ForEach(r =>
            {
                RecordRole(r);
            });
        }

        #endregion

        #region 记录角色数据

        static void RecordRole(RoleDto role)
        {
            if (role == null)
            {
                return;
            }
            CacheKey roleKey = CacheUtil.GetRoleCacheKey(role.SysNo.ToString());
            CacheManager.SetData(roleKey, role);
        }

        #endregion

        #endregion

        #region 操作分组

        #region 初始化操作分组

        static void InitAuthOperationGroup()
        {
            var operationGroups = AuthAppService.GetAuthorityOperationGroupList(null);
            operationGroups?.ForEach(c =>
            {
                RecordAuthOperationGroup(c);
            });
        }

        #endregion

        #region 记录操作分组

        /// <summary>
        /// 记录操作分组
        /// </summary>
        /// <param name="authorityOperationGroup">操作分组</param>
        static void RecordAuthOperationGroup(AuthorityOperationGroupDto authorityOperationGroup)
        {
            if (authorityOperationGroup == null)
            {
                return;
            }
            var groupCacheKey = CacheUtil.GetOperationGroupCacheKey(authorityOperationGroup.SysNo.ToString());
            CacheManager.SetData(groupCacheKey, authorityOperationGroup);
        }

        #endregion

        #endregion

        #region 授权操作

        #region 初始化授权操作

        static void InitAuthOperation()
        {
            var operations = AuthAppService.GetAuthorityOperationList(null);
            if (operations.IsNullOrEmpty())
            {
                return;
            }
            operations.ForEach(c =>
            {
                RecordAuthOperation(c);
            });
        }

        #endregion

        #region 记录授权操作

        /// <summary>
        /// 记录授权操作
        /// </summary>
        /// <param name="authorityOperation">授权操作</param>
        static void RecordAuthOperation(AuthorityOperationDto authorityOperation)
        {
            if (authorityOperation == null)
            {
                return;
            }
            var operationValue = $"{authorityOperation.ControllerCode}/{authorityOperation.ActionCode}";
            var operationCacheKey = CacheUtil.GetOperationCacheKey(operationValue);
            CacheManager.SetData(operationCacheKey, authorityOperation);
        }

        #endregion

        #endregion
    }

    public static class CacheUtil
    {
        static CacheUtil()
        {
            AllLoginUserCacheKey = new CacheKey();
            AllLoginUserCacheKey.AddName("AllLoginUser");
        }

        #region 获取用户&角色缓存键值

        public static CacheKey GetUserRoleCacheKey(string userId)
        {
            var cacheKey = new CacheKey();
            cacheKey.AddName("UserRole", userId);
            return cacheKey;
        }

        #endregion

        #region 获取用户授权缓存键值

        /// <summary>
        /// 获取用户授权缓存键值
        /// </summary>
        /// <param name="userIdentityKey">用户标识值</param>
        /// <returns></returns>
        public static CacheKey GetUserAuthOperationCacheKey(string userIdentityKey)
        {
            CacheKey cacheKey = new CacheKey();
            cacheKey.AddName("UserAuthOperation", userIdentityKey);
            return cacheKey;
        }

        #endregion

        #region 获取授权操作缓存键值

        /// <summary>
        /// 获取授权操作缓存键值
        /// </summary>
        /// <param name="operationKey">授权操作值</param>
        /// <returns></returns>
        public static CacheKey GetOperationCacheKey(string operationKey)
        {
            CacheKey cacheKey = new CacheKey();
            cacheKey.AddName("AuthOperation", operationKey);
            return cacheKey;
        }

        #endregion

        #region 获取操作分组缓存键值

        /// <summary>
        /// 获取操作分组缓存键值
        /// </summary>
        /// <param name="groupKey"></param>
        /// <returns></returns>
        public static CacheKey GetOperationGroupCacheKey(string groupKey)
        {
            var groupCacheKey = new CacheKey();
            groupCacheKey.AddName("AuthOperationGroup", groupKey);
            return groupCacheKey;
        }

        #endregion

        #region 获取角色缓存键值

        public static CacheKey GetRoleCacheKey(string roleId)
        {
            var roleKey = new CacheKey();
            roleKey.AddName("Role", roleId);
            return roleKey;
        }

        #endregion

        #region 获取用户缓存键值

        public static CacheKey GetUserCacheKey(string userId)
        {
            var cacheKey = new CacheKey();
            cacheKey.AddName("LoginUser", userId);
            return cacheKey;
        }

        #endregion

        #region 记录所有登录用户缓存键值

        public static CacheKey AllLoginUserCacheKey;

        #endregion
    }
}
