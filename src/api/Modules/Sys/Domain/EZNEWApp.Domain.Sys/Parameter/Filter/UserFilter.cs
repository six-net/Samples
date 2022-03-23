using System;
using System.Collections.Generic;
using EZNEW.Development.Query;
using EZNEW.Development.Domain;
using EZNEWApp.Module.Sys;
using EZNEW.Paging;
using EZNEWApp.Domain.Sys.Model;

namespace EZNEWApp.Domain.Sys.Parameter.Filter
{
    /// <summary>
    /// 用户筛选信息
    /// </summary>
    public class UserFilter : PagingFilter, IDomainParameter
    {
        #region	数据筛选

        /// <summary>
        /// 用户编号
        /// </summary>
        public List<long> Ids { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 真实名称
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public UserType? UserType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public UserStatus? Status { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 超级管理员
        /// </summary>
        public bool? SuperUser { get; set; }

        /// <summary>
        /// Gets the creation time
        /// </summary>
        public DateTimeOffset? CreationTime { get; set; }

        /// <summary>
        /// Gets the update time
        /// </summary>
        public DateTimeOffset? UpdateTime { get; set; }

        /// <summary>
        /// 名称匹配关键字(UserName/RealName)
        /// </summary>
        public string NameMateKey { get; set; }

        /// <summary>
        /// 联系关键字(QQ/Email/Mobile)
        /// </summary>
        public string ContactMateKey { get; set; }

        /// <summary>
        /// 角色筛选
        /// </summary>
        public RoleFilter RoleFilter { get; set; }

        #endregion

        #region 创建查询对象

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <returns>返回查询对象</returns>
        public override IQuery CreateQuery()
        {
            var query = base.CreateQuery();

            query = query ?? QueryManager.Create<User>(this);

            #region 数据筛选

            if (!Ids.IsNullOrEmpty())
            {
                query.In<User>(c => c.Id, Ids);
            }
            if (!string.IsNullOrWhiteSpace(NameMateKey))
            {
                query.And<User>( CriterionOperator.Like, NameMateKey, CriterionConnector.Or, u => u.UserName, u => u.RealName);
            }
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                query.Equal<User>(c => c.UserName, UserName);
            }
            if (!string.IsNullOrWhiteSpace(RealName))
            {
                query.Like<User>(c => c.RealName, RealName);
            }
            if (!string.IsNullOrWhiteSpace(Password))
            {
                query.Equal<User>(c => c.Password, Password);
            }
            if (UserType.HasValue)
            {
                query.Equal<User>(c => c.UserType, UserType.Value);
            }
            if (Status.HasValue)
            {
                query.Equal<User>(c => c.Status, Status.Value);
            }
            if (!string.IsNullOrWhiteSpace(Mobile))
            {
                query.Equal<User>(c => c.Mobile, Mobile);
            }
            if (!string.IsNullOrWhiteSpace(Email))
            {
                query.Equal<User>(c => c.Email, Email);
            }
            if (!string.IsNullOrWhiteSpace(QQ))
            {
                query.Equal<User>(c => c.QQ, QQ);
            }
            if (!string.IsNullOrWhiteSpace(ContactMateKey))
            {
                query.And<User>( CriterionOperator.Like, ContactMateKey, CriterionConnector.Or, u => u.Mobile, u => u.Email, u => u.QQ);
            }
            if (SuperUser.HasValue)
            {
                query.Equal<User>(c => c.SuperUser, SuperUser.Value);
            }
            if (CreationTime.HasValue)
            {
                query.Equal<User>(c => c.CreationTime, CreationTime.Value);
            }

            #endregion

            #region 角色筛选

            if (RoleFilter != null)
            {
                IQuery roleQuery = RoleFilter.CreateQuery();
                if (roleQuery != null && !roleQuery.Conditions.IsNullOrEmpty())
                {
                    IQuery userRoleQuery = QueryManager.Create<UserRole>();
                    userRoleQuery.EqualInnerJoin(roleQuery);
                    query.EqualInnerJoin(userRoleQuery);
                }
            }

            #endregion

            return query;
        } 

        #endregion
    }
}
