using EZNEW.Develop.Domain.Event;
using EZNEW.Domain.Sys.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.IoC
{
    /// <summary>
    /// domain event config
    /// </summary>
    public class DomainEventConfig
    {
        public static void Init()
        {
            //DomainEventBus.GlobalSubscribe<SaveRoleEvent>(new RoleSavedThenSendEmailEventHandler());
            //DomainEventBus.GlobalSubscribe<SaveRoleEvent>(e =>
            //{
            //    Console.WriteLine("正在保存角色：{0}", e.Role?.Name);
            //    return DomainEventExecuteResult.EmptyResult();
            //});
            //DomainEventBus.GlobalSubscribeAll(e => 
            //{
            //    Console.WriteLine("全局领域事件");
            //    return new DomainEventExecuteResult();
            //});

            //DomainEventBus.GlobalSubscribe<DefaultAggregationSaveDomainEvent<AdminUser>>(e => 
            //{
            //    Console.WriteLine("保存了管理用户");
            //    return DomainEventExecuteResult.EmptyResult();
            //});
        }
    }
}
