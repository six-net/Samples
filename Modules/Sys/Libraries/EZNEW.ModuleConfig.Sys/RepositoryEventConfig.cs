using EZNEW.Develop.Domain.Repository.Event;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.ModuleConfig.Sys
{
    /// <summary>
    /// 领域事件配置
    /// </summary>
    public static class RepositoryEventConfig
    {
        public static void Configure()
        {
            #region User

            //删除用户->删除用户&角色绑定
            RepositoryEventBus.SubscribeRemove<IUserRepository, IUserRoleRepository, User>(c => c.RemoveByFirst, c => c.RemoveByFirst);
            //删除用户->删除用户授权
            RepositoryEventBus.SubscribeRemove<IUserRepository, IUserPermissionRepository, User>(c => c.RemoveByFirst, c => c.RemoveByFirst);

            #endregion

            #region Role

            //删除角色->删除角色&用户绑定
            RepositoryEventBus.SubscribeRemove<IRoleRepository, IUserRoleRepository, Role>(c => c.RemoveBySecond, c => c.RemoveBySecond);
            //删除角色->删除角色授权
            RepositoryEventBus.SubscribeRemove<IRoleRepository, IRolePermissionRepository, Role>(c => c.RemoveByFirst, c => c.RemoveByFirst);

            #endregion

            #region AuthorityOperationGroup

            //删除授权操作分组->删除分组下的授权操作
            RepositoryEventBus.SubscribeRemove<IOperationGroupRepository, IOperationRepository, OperationGroup>(c => c.RemoveOperationByGroup, c => c.RemoveOperationByGroup);

            #endregion

            #region AuthorityOperation

            //删除授权操作->删除权限&授权操作绑定
            RepositoryEventBus.SubscribeRemove<IOperationRepository, IPermissionOperationRepository, Operation>(c => c.RemoveBySecond, c => c.RemoveBySecond);

            #endregion

            #region AuthorityGroup

            //删除权限分组->删除分组下的权限数据
            RepositoryEventBus.SubscribeRemove<IPermissionGroupRepository, IPermissionRepository, PermissionGroup>(c => c.RemovePermissionByGroup, c => c.RemovePermissionByGroup);

            #endregion

            #region Authority

            //删除权限->删除权限&授权操作绑定
            RepositoryEventBus.SubscribeRemove<IPermissionRepository, IPermissionOperationRepository, Permission>(c => c.RemoveByFirst, c => c.RemoveByFirst);
            //删除权限->删除角色授权
            RepositoryEventBus.SubscribeRemove<IPermissionRepository, IRolePermissionRepository, Permission>(c => c.RemoveBySecond, c => c.RemoveBySecond);
            //删除权限->删除用户授权
            RepositoryEventBus.SubscribeRemove<IPermissionRepository, IUserPermissionRepository, Permission>(c => c.RemoveBySecond, c => c.RemoveBySecond);

            #endregion
        }
    }
}
