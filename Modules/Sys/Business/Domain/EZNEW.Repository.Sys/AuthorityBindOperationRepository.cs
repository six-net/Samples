using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Framework.Extension;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Entity.Sys;
using EZNEW.Develop.CQuery;
using EZNEW.Query.Sys;
using EZNEW.Develop.UnitOfWork;
using EZNEW.Develop.Domain.Repository.Warehouse;
using EZNEW.Framework.IoC;
using EZNEW.Develop.Domain.Repository;

namespace EZNEW.Repository.Sys
{
    public class AuthorityBindOperationRepository : DefaultRelationRepository<Authority, AuthorityOperation, AuthorityBindOperationEntity, IAuthorityBindOperationDataAccess>, IAuthorityBindOperationRepository
    {
        /// <summary>
        /// create entity by relation data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override AuthorityBindOperationEntity CreateEntityByRelationData(Tuple<Authority, AuthorityOperation> data)
        {
            if (data == null)
            {
                return null;
            }
            return new AuthorityBindOperationEntity()
            {
                AuthorityOperationSysNo = data.Item2.SysNo,
                AuthoritySysNo = data.Item1.SysNo
            };
        }

        /// <summary>
        /// create query by first
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public override IQuery CreateQueryByFirst(IEnumerable<Authority> datas)
        {
            if (datas.IsNullOrEmpty())
            {
                return null;
            }
            var authSysNos = datas.Select(c => c.SysNo);
            var query = QueryFactory.Create<AuthorityBindOperationQuery>(a => authSysNos.Contains(a.AuthoritySysNo));
            return query;
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
            var copyQuery = query.Clone();
            copyQuery.QueryFields.Clear();
            copyQuery.AddQueryFields<AuthorityQuery>(c => c.SysNo);
            var removeQuery = QueryFactory.Create<AuthorityBindOperationQuery>();
            removeQuery.And<AuthorityBindOperationQuery>(ur => ur.AuthoritySysNo, CriteriaOperator.In, copyQuery);
            return removeQuery;
        }

        /// <summary>
        /// create query by second
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public override IQuery CreateQueryBySecond(IEnumerable<AuthorityOperation> datas)
        {
            if (datas.IsNullOrEmpty())
            {
                return null;
            }
            var operationIds = datas.Select(c => c.SysNo);
            var query = QueryFactory.Create<AuthorityBindOperationQuery>(a => operationIds.Contains(a.AuthorityOperationSysNo));
            return query;
        }

        /// <summary>
        /// create query by second
        /// </summary>
        /// <param name="query">query</param>
        /// <returns></returns>
        public override IQuery CreateQueryBySecond(IQuery query)
        {
            if (query == null)
            {
                return null;
            }
            var copyQuery = query.Clone();
            copyQuery.QueryFields.Clear();
            copyQuery.AddQueryFields<AuthorityOperationQuery>(c => c.SysNo);
            var removeQuery = QueryFactory.Create<AuthorityBindOperationQuery>();
            removeQuery.And<AuthorityBindOperationQuery>(ur => ur.AuthorityOperationSysNo, CriteriaOperator.In, copyQuery);
            return removeQuery;
        }

        /// <summary>
        /// create relation data by entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override Tuple<Authority, AuthorityOperation> CreateRelationDataByEntity(AuthorityBindOperationEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            return new Tuple<Authority, AuthorityOperation>(Authority.CreateAuthority(entity.AuthoritySysNo), AuthorityOperation.CreateAuthorityOperation(entity.AuthorityOperationSysNo));
        }
    }
}
