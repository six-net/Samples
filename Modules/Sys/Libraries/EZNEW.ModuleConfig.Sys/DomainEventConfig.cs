using EZNEW.Module.Sys;
using EZNEW.Cache;
using EZNEW.Develop.Domain.Event;
using EZNEW.Domain.Sys.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EZNEW.ModuleConfig.Sys
{
    /// <summary>
    /// 领域事件配置
    /// </summary>
    internal static class DomainEventConfig
    {
        internal static void Init()
        {
            #region 保存用户数据

            #region 刷新登录缓存信息

            DomainEventBus.GlobalSubscribe<DefaultAggregationSaveDomainEvent<User>>(e =>
            {
                if (e?.Object != null && e.Object.Status != UserStatus.正常)
                {
                    Task.Run(() =>
                    {
                        var userId = e.Object.SysNo.ToString();
                        var loginUserCacheKey = new CacheKey("LoginUser", userId);
                        CacheManager.Keys.Delete(new Cache.Keys.Request.DeleteOption()
                        {
                            Keys = new List<CacheKey>(1) { loginUserCacheKey }
                        });
                        var loginRecordCacheKey = new CacheKey("AllLoginUser");
                        CacheManager.Set.Remove(new Cache.Set.Request.SetRemoveOption()
                        {
                            Key = loginRecordCacheKey,
                            RemoveValues = new List<string>(1) { userId }
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
