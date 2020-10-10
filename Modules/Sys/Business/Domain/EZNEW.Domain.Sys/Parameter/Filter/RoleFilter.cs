using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Develop.CQuery;
using EZNEW.Develop.Domain;
using EZNEW.Domain.Sys.Model;
using EZNEW.Entity.Sys;
using EZNEW.Paging;

namespace EZNEW.Domain.Sys.Parameter.Filter
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
        /// 添加时间
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 查询上级角色
        /// </summary>
        public bool QuerySuperiorRole { get; set; }

        #endregion

        #region 数据加载

        /// <summary>
        /// 加载上级
        /// </summary>
        public bool LoadParent
        {
            get; set;
        }

        #endregion

        #region 创建查询对象

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <returns>返回查询对象</returns>
        public override IQuery CreateQuery()
        {
            var query = base.CreateQuery() ?? QueryManager.Create<RoleEntity>(this);

            #region 数据筛选

            if (!Ids.IsNullOrEmpty())
            {
                query.In<RoleEntity>(c => c.Id, Ids);
            }
            if (!ExcludeIds.IsNullOrEmpty())
            {
                query.NotIn<RoleEntity>(c => c.Id, ExcludeIds);
            }
            if (!string.IsNullOrWhiteSpace(Name))
            {
                query.Equal<RoleEntity>(c => c.Name, Name);
            }
            if (!string.IsNullOrWhiteSpace(NameMateKey))
            {
                query.Like<RoleEntity>(c => c.Name, NameMateKey.Trim());
            }
            if (Status.HasValue)
            {
                query.Equal<RoleEntity>(c => c.Status, Status.Value);
            }
            if (CreateDate.HasValue)
            {
                query.Equal<RoleEntity>(c => c.CreateDate, CreateDate.Value);
            }
            if (!string.IsNullOrWhiteSpace(Remark))
            {
                query.Equal<RoleEntity>(c => c.Remark, Remark);
            }

            #endregion

            return query;
        } 

        #endregion
    }
}
