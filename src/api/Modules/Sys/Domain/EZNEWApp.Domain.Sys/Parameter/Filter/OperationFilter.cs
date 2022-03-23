using System.Collections.Generic;
using EZNEW.Development.Query;
using EZNEW.Development.Domain;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Module.Sys;
using EZNEW.Paging;
using System;

namespace EZNEWApp.Domain.Sys.Parameter.Filter
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
        public string Controller { get; set; }

        /// <summary>
        /// 操作方法
        /// </summary>
        public string Action { get; set; }

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
        /// Gets the creation time
        /// </summary>
        public DateTimeOffset? CreationTime { get; set; }

        /// <summary>
        /// Gets the update time
        /// </summary>
        public DateTimeOffset? UpdateTime { get; set; }

        /// <summary>
        /// ControllerCode/ActionCode/Name 匹配关键字
        /// </summary>
        public string OperationMateKey { get; set; }

        /// <summary>
        /// 用户筛选条件
        /// </summary>
        public UserFilter UserFilter { get; set; }

        /// <summary>
        /// 权限筛选信息
        /// </summary>
        public PermissionFilter PermissionFilter { get; set; }

        #endregion

        #region 创建查询对象

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <returns>返回查询对象</returns>
        public override IQuery CreateQuery()
        {
            var operationQuery = base.CreateQuery() ?? QueryManager.Create<Operation>(this);

            #region 数据筛选

            if (!Ids.IsNullOrEmpty())
            {
                operationQuery.In<Operation>(c => c.Id, Ids);
            }
            if (!string.IsNullOrWhiteSpace(OperationMateKey))
            {
                operationQuery.And<Operation>(CriterionOperator.Like, OperationMateKey, CriterionConnector.Or, u => u.Name, u => u.Controller, u => u.Action);
            }
            if (!string.IsNullOrWhiteSpace(Controller))
            {
                operationQuery.Equal<Operation>(c => c.Controller, Controller);
            }
            if (!string.IsNullOrWhiteSpace(Action))
            {
                operationQuery.Equal<Operation>(c => c.Action, Action);
            }
            if (!string.IsNullOrWhiteSpace(Name))
            {
                operationQuery.Equal<Operation>(c => c.Name, Name);
            }
            if (Sort.HasValue)
            {
                operationQuery.Equal<Operation>(c => c.Sort, Sort.Value);
            }
            if (Group.HasValue)
            {
                operationQuery.Equal<Operation>(c => c.Group, Group.Value);
            }
            if (AccessLevel.HasValue)
            {
                operationQuery.Equal<Operation>(c => c.AccessLevel, AccessLevel.Value);
            }
            if (!string.IsNullOrWhiteSpace(Remark))
            {
                operationQuery.Equal<Operation>(c => c.Remark, Remark);
            }

            #endregion

            #region 用户授权操作

            if (UserFilter != null)
            {
                //用户授权的操作或者公共操作
                var publicOrUserOperationQuery = QueryManager.Create<Operation>(a => a.AccessLevel == OperationAccessLevel.Public);
                //用户权限
                var permissionQuery = new PermissionFilter()
                {
                    UserFilter = UserFilter,
                    Status = PermissionStatus.Enable
                }.CreateQuery();
                //权限绑定的操作
                var userPermissionOperationQuery = QueryManager.Create<OperationPermission>();
                userPermissionOperationQuery.EqualInnerJoin(permissionQuery);
                userPermissionOperationQuery.AddQueryFields<OperationPermission>(p => p.OperationId);
                publicOrUserOperationQuery.In<Operation>(c => c.Id, userPermissionOperationQuery, CriterionConnector.Or);
                operationQuery.And(publicOrUserOperationQuery);
            }

            #endregion

            #region 权限筛选

            if (PermissionFilter != null)
            {
                IQuery permissionQuery = PermissionFilter.CreateQuery();
                if (permissionQuery != null && !permissionQuery.Conditions.IsNullOrEmpty())
                {
                    //权限和操作绑定
                    IQuery permissionOperationQuery = QueryManager.Create<OperationPermission>();
                    permissionOperationQuery.EqualInnerJoin(permissionQuery);
                    operationQuery.EqualInnerJoin(permissionOperationQuery);
                }
            }

            #endregion

            return operationQuery;
        }

        #endregion
    }
}
