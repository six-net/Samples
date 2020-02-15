using EZNEW.Develop.Domain.Repository.Event;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.IoC
{
    public static class RepositoryConfig
    {
        public static void Init()
        {
            #region Sys

            #region User

            //删除用户->删除用户&角色绑定
            RepositoryEventBus.SubscribeRemove<IUserRepository, IUserRoleRepository, User>(c => c.RemoveByFirst, c => c.RemoveByFirst);
            //删除用户->删除用户授权
            RepositoryEventBus.SubscribeRemove<IUserRepository, IUserAuthorizeRepository, User>(c => c.RemoveByFirst, c => c.RemoveByFirst);
            //查询用户->用户转换
            RepositoryEventBus.SubscribeQuery<IUserRepository, IAdminUserRepository, User>(c => c.LoadAdminUser);

            #endregion

            #region Role

            //删除角色->删除角色&用户绑定
            RepositoryEventBus.SubscribeRemove<IRoleRepository, IUserRoleRepository, Role>(c => c.RemoveBySecond, c => c.RemoveBySecond);
            //删除角色->删除角色授权
            RepositoryEventBus.SubscribeRemove<IRoleRepository, IRoleAuthorizeRepository, Role>(c => c.RemoveByFirst, c => c.RemoveByFirst);
           
            #endregion

            #region AuthorityOperationGroup

            //删除授权操作分组->删除分组下的授权操作
            RepositoryEventBus.SubscribeRemove<IAuthorityOperationGroupRepository, IAuthorityOperationRepository, AuthorityOperationGroup>(c => c.RemoveOperationByGroup, c => c.RemoveOperationByGroup);

            #endregion

            #region AuthorityOperation

            //删除授权操作->删除权限&授权操作绑定
            RepositoryEventBus.SubscribeRemove<IAuthorityOperationRepository, IAuthorityBindOperationRepository, AuthorityOperation>(c => c.RemoveBySecond, c => c.RemoveBySecond);

            #endregion

            #region AuthorityGroup

            //删除权限分组->删除分组下的权限数据
            RepositoryEventBus.SubscribeRemove<IAuthorityGroupRepository, IAuthorityRepository, AuthorityGroup>(c => c.RemoveAuthorityByGroup, c => c.RemoveAuthorityByGroup);

            #endregion

            #region Authority

            //删除权限->删除权限&授权操作绑定
            RepositoryEventBus.SubscribeRemove<IAuthorityRepository, IAuthorityBindOperationRepository, Authority>(c => c.RemoveByFirst, c => c.RemoveByFirst);
            //删除权限->删除角色授权
            RepositoryEventBus.SubscribeRemove<IAuthorityRepository, IRoleAuthorizeRepository, Authority>(c => c.RemoveBySecond, c => c.RemoveBySecond);
            //删除权限->删除用户授权
            RepositoryEventBus.SubscribeRemove<IAuthorityRepository, IUserAuthorizeRepository, Authority>(c => c.RemoveBySecond, c => c.RemoveBySecond);

            #endregion

            #endregion

            //#region CTask

            //#region server node

            ////删除服务节点->删除服务节点&任务承载关系
            //RepositoryEventBus.SubscribeObjectRemove<IServerNodeRepository, IJobServerHostRepository, ServerNode>(r => r.RemoveJobServerHostByServer);
            ////删除服务节点->删除服务节点&执行计划承载关系
            //RepositoryEventBus.SubscribeObjectRemove<IServerNodeRepository, ITriggerServerRepository, ServerNode>(r => r.RemoveTriggerServerByServer);

            //#endregion

            //#region job group

            ////删除工作分组->删除工作分组下的工作
            //RepositoryEventBus.SubscribeRemove<IJobGroupRepository, IJobRepository, JobGroup>(r => r.RemoveByJobGroup, r => r.RemoveByJobGroup);

            //#endregion

            //#region job

            ////删除工作->删除工作&服务节点承载
            //RepositoryEventBus.SubscribeObjectRemove<IJobRepository, IJobServerHostRepository, Job>(r => r.RemoveJobServerHostByJob);
            ////删除工作->删除工作任务计划
            //RepositoryEventBus.SubscribeObjectRemove<IJobRepository, ITriggerRepository, Job>(r => r.RemoveTriggerByJob);

            //#endregion

            //#region job server host

            ////删除任务&服务承载->删除服务&任务执行计划
            //RepositoryEventBus.SubscribeObjectRemove<IJobServerHostRepository, ITriggerServerRepository, JobServerHost>(r => r.RemoveTriggerServerByJobHost);

            //#endregion

            //#region trigger

            ////保存执行计划->根据计划类型不同分别执行保存
            //RepositoryEventBus.SubscribeSave<ITriggerRepository, ISimpleTriggerRepository, Trigger>(c => c.SaveSimpleTrigger);//简单执行计划
            //RepositoryEventBus.SubscribeSave<ITriggerRepository, IExpressionTriggerRepository, Trigger>(c => c.SaveExpressionTrigger);//自定义执行计划
            ////保存执行计划->保存执行计划
            //RepositoryEventBus.SubscribeSave<ITriggerRepository, ITriggerConditionRepository, Trigger>(c => c.SaveTriggerConditionFromTrigger);//自定义执行计划

            ////删除执行计划->根据计划类型删除特定信息
            //RepositoryEventBus.SubscribeObjectRemove<ITriggerRepository, ISimpleTriggerRepository, Trigger>(c => c.RemoveSimpleTrigger);//简单执行计划
            //RepositoryEventBus.SubscribeSave<ITriggerRepository, IExpressionTriggerRepository, Trigger>(c => c.RemoveExpressionTrigger);//自定义执行计划
            ////删除执行计划->删除附加条件
            //RepositoryEventBus.SubscribeSave<ITriggerRepository, ITriggerConditionRepository, Trigger>(c => c.RemoveTriggerConditionByTrigger);
            ////删除执行计划->删除计划&服务绑定
            //RepositoryEventBus.SubscribeSave<ITriggerRepository, ITriggerServerRepository, Trigger>(c => c.RemoveTriggerServerByTrigger);

            ////查询计划查询特定类型信息
            //RepositoryEventBus.SubscribeQuery<ITriggerRepository, ISimpleTriggerRepository, Trigger>(c => c.LoadSimpleTrigger);
            //RepositoryEventBus.SubscribeQuery<ITriggerRepository, IExpressionTriggerRepository, Trigger>(c => c.LoadExpressionTrigger);

            //#endregion

            //#endregion
        }
    }
}
