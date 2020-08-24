using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Entity.Sys;
using EZNEW.Develop.CQuery;
using EZNEW.Develop.Domain.Repository;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;

namespace EZNEW.Repository.Sys
{
    /// <summary>
    /// 用户绑定角色存储
    /// </summary>
    public class UserRoleRepository : DefaultRelationRepository<User, Role, UserRoleEntity, IUserRoleDataAccess>, IUserRoleRepository
    {
        /// <summary>
        /// Create query by first type datas
        /// </summary>
        /// <param name="datas">Datas</param>
        /// <returns></returns>
        public override IQuery CreateQueryByFirst(IEnumerable<User> datas)
        {
            if (datas.IsNullOrEmpty())
            {
                return null;
            }
            IEnumerable<long> userIds = datas.Select(c => c.Id);
            return QueryManager.Create<UserRoleEntity>(c => userIds.Contains(c.UserId));
        }

        /// <summary>
        /// Create query by second type datas
        /// </summary>
        /// <param name="datas">Datas</param>
        /// <returns></returns>
        public override IQuery CreateQueryBySecond(IEnumerable<Role> datas)
        {
            if (datas.IsNullOrEmpty())
            {
                return null;
            }
            IEnumerable<long> roleIds = datas.Select(c => c.Id);
            return QueryManager.Create<UserRoleEntity>(c => roleIds.Contains(c.RoleId));
        }

        /// <summary>
        /// Create entity by relation data
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns></returns>
        public override UserRoleEntity CreateEntityByRelationData(Tuple<User, Role> data)
        {
            return new UserRoleEntity()
            {
                UserId = data.Item1.Id,
                RoleId = data.Item2.Id
            };
        }

        /// <summary>
        /// Create relation data
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns></returns>
        public override Tuple<User, Role> CreateRelationDataByEntity(UserRoleEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            return new Tuple<User, Role>(User.Create(entity.UserId), Role.Create(entity.RoleId));
        }

        /// <summary>
        /// Create query by first
        /// </summary>
        /// <param name="query">First type data query</param>
        /// <returns></returns>
        public override IQuery CreateQueryByFirst(IQuery query)
        {
            if (query == null)
            {
                return null;
            }
            var userQuery = query.LightClone();
            userQuery.ClearQueryFields();
            userQuery.AddQueryFields<UserEntity>(c => c.Id);
            var userRoleQuery = QueryManager.Create<UserRoleEntity>();
            userRoleQuery.And<UserRoleEntity>(ur => ur.UserId, CriteriaOperator.In, userQuery);
            return userRoleQuery;
        }

        /// <summary>
        /// Create query by second
        /// </summary>
        /// <param name="query">Second type data query</param>
        /// <returns></returns>
        public override IQuery CreateQueryBySecond(IQuery query)
        {
            if (query == null)
            {
                return null;
            }
            var roleQuery = query.LightClone();
            roleQuery.ClearQueryFields();
            roleQuery.AddQueryFields<RoleEntity>(c => c.Id);
            var userRoleQuery = QueryManager.Create<UserRoleEntity>();
            userRoleQuery.And<UserRoleEntity>(ur => ur.RoleId, CriteriaOperator.In, roleQuery);
            return userRoleQuery;
        }
    }
}
