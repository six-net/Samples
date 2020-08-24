using System.Collections.Generic;
using System.Linq;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Entity.Sys;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Develop.Domain.Repository;
using EZNEW.Develop.CQuery;

namespace EZNEW.Repository.Sys
{
    /// <summary>
    /// 用户授权存储
    /// </summary>
    public class UserPermissionRepository : DefaultAggregationRelationRepository<UserPermission, User, Permission, UserPermissionEntity, IUserPermissionDataAccess>, IUserPermissionRepository
    {
        public override IQuery CreateQueryByFirst(IEnumerable<User> datas)
        {
            if (datas.IsNullOrEmpty())
            {
                return null;
            }
            var userIds = datas.Select(c => c.Id);
            return QueryManager.Create<UserPermissionEntity>(c => userIds.Contains(c.UserId));
        }

        public override IQuery CreateQueryByFirst(IQuery query)
        {
            if (query == null)
            {
                return null;
            }
            var userQuery = query.LightClone();
            userQuery.ClearQueryFields();
            userQuery.AddQueryFields<UserEntity>(c => c.Id);
            var userPermissionQuery = QueryManager.Create<UserPermissionEntity>();
            userPermissionQuery.And<UserPermissionEntity>(ur => ur.UserId, CriteriaOperator.In, userQuery);
            return userPermissionQuery;
        }

        public override IQuery CreateQueryBySecond(IEnumerable<Permission> datas)
        {
            if (datas.IsNullOrEmpty())
            {
                return null;
            }
            var permissionIds = datas.Select(c => c.Id);
            return QueryManager.Create<UserPermissionEntity>(c => permissionIds.Contains(c.PermissionId));
        }

        public override IQuery CreateQueryBySecond(IQuery query)
        {
            if (query == null)
            {
                return null;
            }
            var permissionQuery = query.LightClone();
            permissionQuery.ClearQueryFields();
            permissionQuery.AddQueryFields<PermissionEntity>(c => c.Id);
            var userPermissionQuery = QueryManager.Create<UserPermissionEntity>();
            userPermissionQuery.And<UserPermissionEntity>(ur => ur.PermissionId, CriteriaOperator.In, permissionQuery);
            return userPermissionQuery;
        }
    }
}
