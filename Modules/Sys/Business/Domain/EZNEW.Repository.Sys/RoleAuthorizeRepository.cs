using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Entity.Sys;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Develop.CQuery;
using EZNEW.Query.Sys;
using EZNEW.Develop.Domain.Repository;

namespace EZNEW.Repository.Sys
{
    /// <summary>
    /// 角色权限存储
    /// </summary>
    public class RoleAuthorizeRepository : DefaultRelationRepository<Role, Authority, RoleAuthorizeEntity, IRoleAuthorizeDataAccess>, IRoleAuthorizeRepository
    {
        public override RoleAuthorizeEntity CreateEntityByRelationData(Tuple<Role, Authority> data)
        {
            return new RoleAuthorizeEntity()
            {
                RoleSysNo = data.Item1.SysNo,
                AuthoritySysNo = data.Item2.SysNo
            };
        }

        public override IQuery CreateQueryByFirst(IEnumerable<Role> datas)
        {
            if (datas.IsNullOrEmpty())
            {
                return null;
            }
            IEnumerable<long> roleIds = datas.Select(c => c.SysNo);
            IQuery query = QueryManager.Create<RoleAuthorizeQuery>(c => roleIds.Contains(c.RoleSysNo));
            return query;
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
            var copyQuery = query.Copy();
            copyQuery.ClearQueryFields();
            copyQuery.AddQueryFields<RoleQuery>(c => c.SysNo);
            var removeQuery = QueryManager.Create<RoleAuthorizeQuery>();
            removeQuery.And<RoleAuthorizeQuery>(ur => ur.RoleSysNo, CriteriaOperator.In, copyQuery);
            return removeQuery;
        }

        public override IQuery CreateQueryBySecond(IEnumerable<Authority> datas)
        {
            if (datas.IsNullOrEmpty())
            {
                return null;
            }
            IEnumerable<long> authIds = datas.Select(c => c.SysNo);
            IQuery query = QueryManager.Create<RoleAuthorizeQuery>(c => authIds.Contains(c.AuthoritySysNo));
            return query;
        }

        public override IQuery CreateQueryBySecond(IQuery query)
        {
            if (query == null)
            {
                return null;
            }
            var copyQuery = query.Copy();
            copyQuery.ClearQueryFields();
            copyQuery.AddQueryFields<AuthorityQuery>(c => c.SysNo);
            var removeQuery = QueryManager.Create<RoleAuthorizeQuery>();
            removeQuery.And<RoleAuthorizeQuery>(ur => ur.AuthoritySysNo, CriteriaOperator.In, copyQuery);
            return removeQuery;
        }

        public override Tuple<Role, Authority> CreateRelationDataByEntity(RoleAuthorizeEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            return new Tuple<Role, Authority>(Role.CreateRole(entity.RoleSysNo), Authority.CreateAuthority(entity.AuthoritySysNo));
        }
    }
}
