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
    /// 操作功能筛选
    /// </summary>
    public class OperationFilter : PagingFilter, IDomainParameter
    {
        #region 筛选条件

        /// <summary>
        /// 主键编号
        /// </summary>
        public List<long> Ids { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string ControllerCode { get; set; }

        /// <summary>
        /// 操作方法
        /// </summary>
        public string ActionCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 操作分组
        /// </summary>
        public long? Group { get; set; }

        /// <summary>
        /// 访问限制等级
        /// </summary>
        public OperationAccessLevel? AccessLevel { get; set; }

        /// <summary>
        /// 方法描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// ControllerCode/ActionCode/Name 匹配关键字
        /// </summary>
        public string OperationMateKey { get; set; }

        /// <summary>
        /// 加载操作分组数据
        /// </summary>
        public bool LoadGroup { get; set; }

        #endregion

        #region 根据筛选条件创建查询对象

        /// <summary>
        /// 根据筛选条件创建查询对象
        /// </summary>
        /// <returns>返回查询对象</returns>
        public override IQuery CreateQuery()
        {
            var query = base.CreateQuery() ?? QueryManager.Create<OperationEntity>(this);

            #region 数据筛选

            if (!Ids.IsNullOrEmpty())
            {
                query.In<OperationEntity>(c => c.Id, Ids);
            }
            if (!string.IsNullOrWhiteSpace(OperationMateKey))
            {
                query.And<OperationEntity>(QueryOperator.OR, CriteriaOperator.Like, OperationMateKey, u => u.Name, u => u.ControllerCode, u => u.ActionCode);
            }
            if (!string.IsNullOrWhiteSpace(ControllerCode))
            {
                query.Equal<OperationEntity>(c => c.ControllerCode, ControllerCode);
            }
            if (!string.IsNullOrWhiteSpace(ActionCode))
            {
                query.Equal<OperationEntity>(c => c.ActionCode, ActionCode);
            }
            if (!string.IsNullOrWhiteSpace(Name))
            {
                query.Equal<OperationEntity>(c => c.Name, Name);
            }
            if (Sort.HasValue)
            {
                query.Equal<OperationEntity>(c => c.Sort, Sort.Value);
            }
            if (Group.HasValue)
            {
                query.Equal<OperationEntity>(c => c.Group, Group.Value);
            }
            if (AccessLevel.HasValue)
            {
                query.Equal<OperationEntity>(c => c.AccessLevel, AccessLevel.Value);
            }
            if (!string.IsNullOrWhiteSpace(Remark))
            {
                query.Equal<OperationEntity>(c => c.Remark, Remark);
            }

            #endregion

            #region 数据加载

            if (LoadGroup)
            {
                query.SetLoadProperty<Operation>(true, c => c.Group);
            }

            #endregion

            return query;
        } 

        #endregion
    }
}
