using EZNEW.Application.Identity.User;
using EZNEW.Develop.CQuery;
using EZNEW.Entity.Sys;
using EZNEW.Framework.ValueType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Query.Sys
{
    /// <summary>
    /// 用户查询
    /// </summary>
    [QueryEntity(typeof(UserEntity))]
    public class UserQuery: QueryModel<UserQuery>
    {
        #region	属性

        /// <summary>
        /// 用户编号
        /// </summary>
        public long SysNo
        {
            get; set;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get; set;
        }

        /// <summary>
        /// 真实名称
        /// </summary>
        public string RealName
        {
            get; set;
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd
        {
            get; set;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public UserType UserType
        {
            get; set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public UserStatus Status
        {
            get; set;
        }

        /// <summary>
        /// 联系方式
        /// </summary>
        public Contact Contact
        {
            get; set;
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateDate
        {
            get; set;
        }

        /// <summary>
        /// 最近登录时间
        /// </summary>
        public DateTime LastLoginDate
        {
            get; set;
        }

        /// <summary>
        /// 超级管理员
        /// </summary>
        public bool SuperUser
        {
            get;
            set;
        }

        #endregion
    }
}
