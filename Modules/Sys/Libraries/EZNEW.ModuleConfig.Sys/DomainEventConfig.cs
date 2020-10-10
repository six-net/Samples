using System.Collections.Generic;
using System.Threading.Tasks;
using EZNEW.Cache;
using EZNEW.Develop.Domain.Event;
using EZNEW.Domain.Sys.Model;
using EZNEW.Module.Sys;

namespace EZNEW.ModuleConfig.Sys
{
    /// <summary>
    /// 领域事件配置
    /// </summary>
    internal static class DomainEventConfig
    {
        internal static void Configure()
        {
            #region 保存用户数据

            #region 刷新登录缓存信息

            DomainEventBus.GlobalSubscribe<DefaultAggregationSaveDomainEvent<User>>(e =>
            {
                if (e?.Object != null && e.Object.Status != UserStatus.Enable)
                {
                    Task.Run(() =>
                    {
                        var userId = e.Object.Id.ToString();
                        var loginUserCacheKey = new CacheKey("LoginUser", userId);
                        CacheManager.Keys.Delete(new Cache.Keys.Request.DeleteOption()
                        {
                            Keys = new List<CacheKey>(1) { loginUserCacheKey }
                        });
                        var loginRecordCacheKey = new CacheKey("AllLoginUser");
                        CacheManager.Set.Remove(new Cache.Set.Request.SetRemoveOption()
                        {
                            Key = loginRecordCacheKey,
                            RemoveMembers = new List<string>(1) { userId }
                        });
                    });
                }
                return DomainEventExecuteResult.EmptyResult();
            }, EventTriggerTime.WorkCompleted);

            #endregion

            #endregion
        }
    }
}
