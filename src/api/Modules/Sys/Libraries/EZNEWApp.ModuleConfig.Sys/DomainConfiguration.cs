using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Development.Domain.Event;
using EZNEW.Logging;
using EZNEWApp.Domain.Sys.Model;

namespace EZNEWApp.ModuleConfig.Sys
{
    /// <summary>
    /// 领域事件配置
    /// </summary>
    public static class DomainConfiguration
    {
        internal static void Configure()
        {
            //以下为测试代码，生产项目请删除

            //保存用户
            DomainEventBus.Subscribe<DefaultSaveDomainEvent<User>>(e =>
            {
                LogManager.LogInformation($"已保存用户:{e.Object?.GetIdentityValue()}");
                return DomainEventResult.EmptyResult();
            }, EventTriggerTime.WorkCompleted);

            //删除用户
            DomainEventBus.Subscribe<DefaultRemoveDomainEvent<User>>(e=> 
            {
                LogManager.LogInformation($"已删除用户:{e.Object?.GetIdentityValue()}");
                return DomainEventResult.EmptyResult();
            }, EventTriggerTime.WorkCompleted);
        }
    }
}
