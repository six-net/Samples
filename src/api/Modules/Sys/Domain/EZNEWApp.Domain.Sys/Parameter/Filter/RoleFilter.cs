using System;
using System.Collections.Generic;
using EZNEW.Development.Query;
using EZNEW.Development.Domain;
using EZNEWApp.Domain.Sys.Model;
using EZNEW.Paging;

namespace EZNEWApp.Domain.Sys.Parameter.Filter
{
    /// <summary>
    /// 角色信息筛选
    /// </summary>
    public class RoleFilter : PagingFilter, IDomainParameter
    {
        #region	数据筛选

        /// <summary>
        /// 角色编号
        /// </summary>
        public List<long> Ids { get; set; }

        /// <summary>
        /// 要排除的编号
        /// </summary>
        public List<long> ExcludeIds { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 名称匹配关键字
        /// </summary>
        public string NameMateKey { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Gets the creation time
        /// </summary>
        public DateTimeOffset? CreationTime { get; set; }

        /// <summary>
        /// Gets the update time
        /// </summary>
        public DateTimeOffset? UpdateTime { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 查询上级角色
        /// </summary>
        public bool QuerySuperiorRole { get; set; }

        /// <summary>
        /// 用户筛选
        /// </summary>
        public UserFilter UserFilter { get; set; }

        #endregion

        #region 数据加载

        /// <summary>
        /// 是否加载上级
        /// </summary>
        public bool LoadParent { get; set; }

        #endregion

        #region 创建查询对象

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <returns>返回查询对象</returns>
        public override IQuery CreateQuery()
        {
            var query = base.CreateQuery() ?? QueryManager.Create<Role>(this);

            #region 数据筛选

            if (!Ids.IsNullOrEmpty())
            {
                query.In<Role>(c => c.Id, Ids);
            }
            if (!ExcludeIds.IsNullOrEmpty())
            {
                query.NotIn<Role>(c => c.Id, ExcludeIds);
            }
            if (!string.IsNullOrWhiteSpace(Name))
            {
                query.Equal<Role>(c => c.Name, Name);
            }
            if (!string.IsNullOrWhiteSpace(NameMateKey))
            {
                query.Like<Role>(c => c.Name, NameMateKey.Trim());
            }
            if (Status.HasValue)
            {
                query.Equal<Role>(c => c.Status, Status.Value);
            }
            if (CreationTime.HasValue)
            {
                query.Equal<Role>(c => c.CreationTime, CreationTime.Value);
            }
            if (!string.IsNullOrWhiteSpace(Remark))
            {
                query.Equal<Role>(c => c.Remark, Remark);
            }

            #endregion

            #region 用户筛选

            if (UserFilter != null)
            {
                IQuery userQuery = UserFilter.CreateQuery();
                if (userQuery != null)
                {
                    IQuery userRoleQuery = QueryManager.Create<UserRole>();
                    userRoleQuery.EqualInnerJoin(userQuery);
                    query.EqualInnerJoin(userRoleQuery);
                }
            }

            #endregion

            return query;
        }

        #endregion
    }
}
