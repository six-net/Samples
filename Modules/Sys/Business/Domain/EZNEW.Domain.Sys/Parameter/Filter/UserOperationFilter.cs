using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Develop.CQuery;
using EZNEW.Develop.Domain;
using EZNEW.Entity.Sys;
using EZNEW.Module.Sys;

namespace EZNEW.Domain.Sys.Parameter.Filter
{
    /// <summary>
    /// 用户操作功能筛选
    /// </summary>
    public class UserOperationFilter : OperationFilter, IDomainParameter
    {
        /// <summary>
        /// 用户筛选条件
        /// </summary>
        public UserFilter UserFilter { get; set; }

        /// <summary>
        /// 根据筛选条件创建查询对象
        /// </summary>
        /// <returns>返回查询对象</returns>
        public override IQuery CreateQuery()
        {
            IQuery query = base.CreateQuery() ?? QueryManager.Create<OperationEntity>(this);
            //用户授权的操作或者无限制的操作
            var publicOrUserOperationQuery = QueryManager.Create<OperationEntity>();
            if (UserFilter != null)
            {
                //用户权限
                var userPermissionQuery = new UserPermissionFilter()
                {
                    UserFilter = UserFilter,
                    Status = PermissionStatus.Enable
                }.CreateQuery();
                //权限绑定操作
                var userPermissionOperationQuery = QueryManager.Create<PermissionOperationEntity>();
                userPermissionOperationQuery.EqualInnerJoin(userPermissionQuery);
                userPermissionOperationQuery.AddQueryFields<PermissionOperationEntity>(p => p.OperationId);
                publicOrUserOperationQuery.In<OperationEntity>(c => c.Id, userPermissionOperationQuery);
            }
            publicOrUserOperationQuery.Or<OperationEntity>(a => a.AccessLevel == OperationAccessLevel.Public);//无限制功能
            query.And(publicOrUserOperationQuery);
            return query;
        }
    }
}
