using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Entity.Sys;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Develop.CQuery;
using EZNEW.Develop.Domain.Repository;

namespace EZNEW.Repository.Sys
{
    /// <summary>
    /// 角色权限存储
    /// </summary>
    public class RolePermissionRepository : DefaultRelationRepository<Role, Permission, RolePermissionEntity, IRolePermissionDataAccess>, IRolePermissionRepository
    {
        public override RolePermissionEntity CreateEntityByRelationData(Tuple<Role, Permission> data)
        {
            return new RolePermissionEntity()
            {
                RoleId = data.Item1.Id,
                PermissionId = data.Item2.Id
            };
        }

        public override IQuery CreateQueryByFirst(IEnumerable<Role> datas)
        {
            if (datas.IsNullOrEmpty())
            {
                return null;
            }
            IEnumerable<long> roleIds = datas.Select(c => c.Id);
            return QueryManager.Create<RolePermissionEntity>(c => roleIds.Contains(c.RoleId));
        }

        /// <summary>
        /// create query by first
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public override IQuery CreateQueryByFirst(IQuery query)
        {
            if (query == null)
            {
                return null;
            }
            var roleQuery = query.LightClone();
            roleQuery.ClearQueryFields();
            roleQuery.AddQueryFields<RoleEntity>(c => c.Id);
            var rolePermissionQuery = QueryManager.Create<RolePermissionEntity>();
            rolePermissionQuery.And<RolePermissionEntity>(ur => ur.RoleId, CriteriaOperator.In, roleQuery);
            return rolePermissionQuery;
        }

        public override IQuery CreateQueryBySecond(IEnumerable<Permission> datas)
        {
            if (datas.IsNullOrEmpty())
            {
                return null;
            }
            IEnumerable<long> permissionIds = datas.Select(c => c.Id);
            return QueryManager.Create<RolePermissionEntity>(c => permissionIds.Contains(c.PermissionId));
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
            var rolePermissionQuery = QueryManager.Create<RolePermissionEntity>();
            rolePermissionQuery.And<RolePermissionEntity>(ur => ur.PermissionId, CriteriaOperator.In, permissionQuery);
            return rolePermissionQuery;
        }

        public override Tuple<Role, Permission> CreateRelationDataByEntity(RolePermissionEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            return new Tuple<Role, Permission>(Role.Create(entity.RoleId), Permission.Create(entity.PermissionId));
        }
    }
}
