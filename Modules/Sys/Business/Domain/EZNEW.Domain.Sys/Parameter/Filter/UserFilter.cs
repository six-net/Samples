using System;
using System.Collections.Generic;
using EZNEW.Develop.CQuery;
using EZNEW.Develop.Domain;
using EZNEW.Entity.Sys;
using EZNEW.Module.Sys;
using EZNEW.Paging;

namespace EZNEW.Domain.Sys.Parameter.Filter
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
        /// 添加时间
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 名称匹配关键字(UserName/RealName)
        /// </summary>
        public string NameMateKey { get; set; }

        /// <summary>
        /// 联系关键字(QQ/Email/Mobile)
        /// </summary>
        public string ContactMateKey { get; set; }

        #endregion

        /// <summary>
        /// 根据筛选条件创建查询对象
        /// </summary>
        /// <returns>返回查询对象</returns>
        public override IQuery CreateQuery()
        {
            var query = base.CreateQuery();

            query = query ?? QueryManager.Create<UserEntity>(this);

            #region 数据筛选

            if (!Ids.IsNullOrEmpty())
            {
                query.In<UserEntity>(c => c.Id, Ids);
            }
            if (!string.IsNullOrWhiteSpace(NameMateKey))
            {
                query.And<UserEntity>(QueryOperator.OR, CriteriaOperator.Like, NameMateKey, u => u.UserName, u => u.RealName);
            }
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                query.Equal<UserEntity>(c => c.UserName, UserName);
            }
            if (!string.IsNullOrWhiteSpace(RealName))
            {
                query.Like<UserEntity>(c => c.RealName, RealName);
            }
            if (!string.IsNullOrWhiteSpace(Password))
            {
                query.Equal<UserEntity>(c => c.Password, Password);
            }
            if (UserType.HasValue)
            {
                query.Equal<UserEntity>(c => c.UserType, UserType.Value);
            }
            if (Status.HasValue)
            {
                query.Equal<UserEntity>(c => c.Status, Status.Value);
            }
            if (!string.IsNullOrWhiteSpace(Mobile))
            {
                query.Equal<UserEntity>(c => c.Mobile, Mobile);
            }
            if (!string.IsNullOrWhiteSpace(Email))
            {
                query.Equal<UserEntity>(c => c.Email, Email);
            }
            if (!string.IsNullOrWhiteSpace(QQ))
            {
                query.Equal<UserEntity>(c => c.QQ, QQ);
            }
            if (!string.IsNullOrWhiteSpace(ContactMateKey))
            {
                query.And<UserEntity>(QueryOperator.OR, CriteriaOperator.Like, ContactMateKey, u => u.Mobile, u => u.Email, u => u.QQ);
            }
            if (SuperUser.HasValue)
            {
                query.Equal<UserEntity>(c => c.SuperUser, SuperUser.Value);
            }
            if (CreateDate.HasValue)
            {
                query.Equal<UserEntity>(c => c.CreateDate, CreateDate.Value);
            }

            #endregion

            return query;
        }
    }
}
