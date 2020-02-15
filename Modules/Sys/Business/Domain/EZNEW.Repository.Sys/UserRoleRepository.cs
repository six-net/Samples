using EZNEW.DataAccessContract.Sys;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Framework.Extension;
using EZNEW.Query.Sys;
using EZNEW.Entity.Sys;
using EZNEW.Develop.CQuery;
using EZNEW.Develop.UnitOfWork;
using EZNEW.Develop.Domain.Repository;

namespace EZNEW.Repository.Sys
{
    /// <summary>
    /// 用户绑定角色存储
    /// </summary>
    public class UserRoleRepository : DefaultRelationRepository<User, Role, UserRoleEntity, IUserRoleDataAccess>, IUserRoleRepository
    {
        /// <summary>
        /// create query by first type datas
        /// </summary>
        /// <param name="datas">datas</param>
        /// <returns></returns>
        public override IQuery CreateQueryByFirst(IEnumerable<User> datas)
        {
            if (datas.IsNullOrEmpty())
            {
                return null;
            }
            IEnumerable<long> userIds = datas.Select(c => c.SysNo).Distinct();
            IQuery query = QueryFactory.Create<UserRoleQuery>(c => userIds.Contains(c.UserSysNo));
            return query;
        }

        /// <summary>
        /// create query by second type datas
        /// </summary>
        /// <param name="datas">datas</param>
        /// <returns></returns>
        public override IQuery CreateQueryBySecond(IEnumerable<Role> datas)
        {
            if (datas.IsNullOrEmpty())
            {
                return null;
            }
            IEnumerable<long> roleIds = datas.Select(c => c.SysNo).Distinct();
            IQuery query = QueryFactory.Create<UserRoleQuery>(c => roleIds.Contains(c.RoleSysNo));
            return query;
        }

        /// <summary>
        /// create entity by relation data
        /// </summary>
        /// <param name="data">data</param>
        /// <returns></returns>
        public override UserRoleEntity CreateEntityByRelationData(Tuple<User, Role> data)
        {
            return new UserRoleEntity()
            {
                UserSysNo = data.Item1.SysNo,
                RoleSysNo = data.Item2.SysNo
            };
        }

        /// <summary>
        /// create relation data
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override Tuple<User, Role> CreateRelationDataByEntity(UserRoleEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            return new Tuple<User, Role>(User.CreateUser(entity.UserSysNo), Role.CreateRole(entity.RoleSysNo));
        }

        /// <summary>
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
            copyQuery.AddQueryFields<UserQuery>(c => c.SysNo);
            var removeQuery = QueryFactory.Create<UserRoleQuery>();
            removeQuery.And<UserRoleQuery>(ur => ur.UserSysNo, CriteriaOperator.In, copyQuery);
            return removeQuery;
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
            copyQuery.AddQueryFields<RoleQuery>(c => c.SysNo);
            var removeQuery = QueryFactory.Create<UserRoleQuery>();
            removeQuery.And<UserRoleQuery>(ur => ur.RoleSysNo, CriteriaOperator.In, copyQuery);
            return removeQuery;
        }
    }
}
