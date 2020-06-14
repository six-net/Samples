using EZNEW.Develop.Domain.Repository.Event;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.ModuleConfig.Sys
{
    /// <summary>
    /// Repository config
    /// </summary>
    public static class RepositoryConfig
    {
        public static void Init()
        {
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
        }
    }
}
