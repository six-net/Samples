using System;
using System.Collections.Generic;
using EZNEW.Development.Query;
using EZNEW.Development.Domain;
using EZNEW.Paging;
using EZNEWApp.Domain.Sys.Model;

namespace EZNEWApp.Domain.Sys.Parameter.Filter
{
    /// <summary>
    /// 权限分组筛选信息
    /// </summary>
    public class PermissionGroupFilter : PagingFilter, IDomainParameter
    {
        #region	筛选条件

        /// <summary>
        /// 编号
        /// </summary>
        public List<long> Ids { get; set; }

        /// <summary>
        /// 排除的数据
        /// </summary>
        public List<long> ExcludeIds { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 上级分组
        /// </summary>
        public long? Parent { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Gets the creation time
        /// </summary>
        public DateTimeOffset? CreationTime { get; set; }

        /// <summary>
        /// Gets the update time
        /// </summary>
        public DateTimeOffset? UpdateTime { get; set; }

        /// <summary>
        /// 只查询第一级数据
        /// </summary>
        public bool LevelOne { get; set; }

        #endregion

        #region 创建查询对象

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <returns>返回查询对象</returns>
        public override IQuery CreateQuery()
        {
            IQuery query = base.CreateQuery() ?? QueryManager.Create<PermissionGroup>(this);
            if (LevelOne)
            {
                query.LessThanOrEqual<PermissionGroup>(c => c.Parent, 0);
            }
            if (!Ids.IsNullOrEmpty())
            {
                query.And<PermissionGroup>(c => Ids.Contains(c.Id));
            }
            if (!ExcludeIds.IsNullOrEmpty())
            {
                query.And<PermissionGroup>(c => !ExcludeIds.Contains(c.Id));
            }
            if (!string.IsNullOrWhiteSpace(Name))
            {
                query.And<PermissionGroup>(c => c.Name == Name);
            }
            if (Sort.HasValue)
            {
                query.And<PermissionGroup>(c => c.Sort == Sort.Value);
            }
            if (Parent.HasValue)
            {
                query.Equal<PermissionGroup>(c => c.Parent, Parent.Value);
            }
            if (!string.IsNullOrWhiteSpace(Remark))
            {
                query.And<PermissionGroup>(c => c.Remark == Remark);
            }
            return query;
        }

        #endregion
    }
}
