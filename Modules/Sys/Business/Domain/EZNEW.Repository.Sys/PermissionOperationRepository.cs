using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Entity.Sys;
using EZNEW.Develop.CQuery;
using EZNEW.Develop.Domain.Repository;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;

namespace EZNEW.Repository.Sys
{
    public class PermissionOperationRepository : DefaultRelationRepository<Permission, Operation, PermissionOperationEntity, IPermissionOperationDataAccess>, IPermissionOperationRepository
    {
        /// <summary>
        /// create entity by relation data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override PermissionOperationEntity CreateEntityByRelationData(Tuple<Permission, Operation> data)
        {
            if (data == null)
            {
                return null;
            }
            return new PermissionOperationEntity()
            {
                OperationId = data.Item2.Id,
                PermissionId = data.Item1.Id
            };
        }

        /// <summary>
        /// create query by first
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public override IQuery CreateQueryByFirst(IEnumerable<Permission> datas)
        {
            if (datas.IsNullOrEmpty())
            {
                return null;
            }
            var permissionIds = datas.Select(c => c.Id);
            return QueryManager.Create<PermissionOperationEntity>(a => permissionIds.Contains(a.PermissionId));
        }

        /// <summary>i
        /// create query by first
        /// </summary>
        /// <param name="query">query</param>
        /// <returns></returns>
        public override IQuery CreateQueryByFirst(IQuery query)
        {
            if (query == null)
            {
                return null;
            }
            var permissionQuery = query.LightClone();
            permissionQuery.ClearQueryFields();
            permissionQuery.AddQueryFields<PermissionEntity>(c => c.Id);
            var permissionOperationQuery = QueryManager.Create<PermissionOperationEntity>();
            permissionOperationQuery.And<PermissionOperationEntity>(ur => ur.PermissionId, CriteriaOperator.In, permissionQuery);
            return permissionOperationQuery;
        }

        /// <summary>
        /// create query by second
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public override IQuery CreateQueryBySecond(IEnumerable<Operation> datas)
        {
            if (datas.IsNullOrEmpty())
            {
                return null;
            }
            var operationIds = datas.Select(c => c.Id);
            return QueryManager.Create<PermissionOperationEntity>(a => operationIds.Contains(a.OperationId));
        }

        /// <summary>
        /// Create query by second
        /// </summary>
        /// <param name="query">query</param>
        /// <returns></returns>
        public override IQuery CreateQueryBySecond(IQuery query)
        {
            if (query == null)
            {
                return null;
            }
            var operationQuery = query.LightClone();
            operationQuery.ClearQueryFields();
            operationQuery.AddQueryFields<OperationEntity>(c => c.Id);
            var permissionOperationQuery = QueryManager.Create<PermissionOperationEntity>();
            permissionOperationQuery.And<PermissionOperationEntity>(ur => ur.OperationId, CriteriaOperator.In, operationQuery);
            return permissionOperationQuery;
        }

        /// <summary>
        /// Create relation data by entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns></returns>
        public override Tuple<Permission, Operation> CreateRelationDataByEntity(PermissionOperationEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            return new Tuple<Permission, Operation>(Permission.Create(entity.PermissionId), Operation.Create(entity.OperationId));
        }
    }
}
