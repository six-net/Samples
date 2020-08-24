using System.Collections.Generic;
using EZNEW.Develop.CQuery;
using EZNEW.Develop.Domain;
using EZNEW.Entity.Sys;
using EZNEW.Paging;

namespace EZNEW.Domain.Sys.Parameter.Filter
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
        /// 分组等级
        /// </summary>
        public int? Level { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }

        #endregion

        #region 创建查询对象

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <returns>返回查询对象</returns>
        public override IQuery CreateQuery()
        {
            IQuery query = base.CreateQuery() ?? QueryManager.Create<PermissionGroupEntity>(this);
            if (!Ids.IsNullOrEmpty())
            {
                query.In<PermissionGroupEntity>(c => c.Id, Ids);
            }
            if (!ExcludeIds.IsNullOrEmpty())
            {
                query.NotIn<PermissionGroupEntity>(c => c.Id, ExcludeIds);
            }
            if (!string.IsNullOrWhiteSpace(Name))
            {
                query.Equal<PermissionGroupEntity>(c => c.Name, Name);
            }
            if (Sort.HasValue)
            {
                query.Equal<PermissionGroupEntity>(c => c.Sort, Sort.Value);
            }
            if (Parent.HasValue)
            {
                query.Equal<PermissionGroupEntity>(c => c.Parent, Parent.Value);
            }
            if (Level.HasValue)
            {
                query.Equal<PermissionGroupEntity>(c => c.Level, Level.Value);
            }
            if (!string.IsNullOrWhiteSpace(Remark))
            {
                query.Equal<PermissionGroupEntity>(c => c.Remark, Remark);
            }
            return query;
        } 

        #endregion
    }
}
