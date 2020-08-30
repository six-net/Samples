using System;
using System.Collections.Generic;
using EZNEW.Develop.CQuery;
using EZNEW.Develop.Domain;
using EZNEW.Domain.Sys.Model;
using EZNEW.Entity.Sys;
using EZNEW.Module.Sys;
using EZNEW.Paging;

namespace EZNEW.Domain.Sys.Parameter.Filter
{
    /// <summary>
    /// 权限筛选信息
    /// </summary>
    public class PermissionFilter : PagingFilter, IDomainParameter
    {
        #region 筛选条件

        /// <summary>
        /// 编号
        /// </summary>
        public List<long> Ids { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        public List<string> Codes { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public PermissionType? Type { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public PermissionStatus? Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 权限分组
        /// </summary>
        public long? Group { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 名称/编码关键字
        /// </summary>
        public string NameCodeMateKey { get; set; }

        #endregion

        #region 数据加载

        /// <summary>
        /// 加载分组
        /// </summary>
        public bool LoadGroup
        {
            get; set;
        }

        #endregion

        #region 创建查询对象

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <returns></returns>
        public override IQuery CreateQuery()
        {
            var query = base.CreateQuery() ?? QueryManager.Create<PermissionEntity>(this);

            #region 数据筛选

            if (!Ids.IsNullOrEmpty())
            {
                query.In<PermissionEntity>(c => c.Id, Ids);
            }
            if (!Codes.IsNullOrEmpty())
            {
                query.In<PermissionEntity>(c => c.Code, Codes);
            }
            if (!string.IsNullOrWhiteSpace(Name))
            {
                query.Equal<PermissionEntity>(c => c.Name, Name);
            }
            if (!string.IsNullOrWhiteSpace(NameCodeMateKey))
            {
                query.And<PermissionEntity>(QueryOperator.OR, CriteriaOperator.Like, NameCodeMateKey, a => a.Code, a => a.Name);
            }
            if (Type.HasValue)
            {
                query.Equal<PermissionEntity>(c => c.Type, Type.Value);
            }
            if (Status.HasValue)
            {
                query.Equal<PermissionEntity>(c => c.Status, Status.Value);
            }
            if (Sort.HasValue)
            {
                query.Equal<PermissionEntity>(c => c.Sort, Sort.Value);
            }
            if (Group.HasValue)
            {
                query.Equal<PermissionEntity>(c => c.Group, Group.Value);
            }
            if (!string.IsNullOrWhiteSpace(Remark))
            {
                query.Equal<PermissionEntity>(c => c.Remark, Remark);
            }
            if (CreateDate.HasValue)
            {
                query.Equal<PermissionEntity>(c => c.CreateDate, CreateDate.Value);
            }

            #endregion

            #region 数据加载

            if (LoadGroup)
            {
                query.SetLoadProperty<Permission>(true, c => c.Group);
            }

            #endregion

            return query;
        } 

        #endregion
    }
}
