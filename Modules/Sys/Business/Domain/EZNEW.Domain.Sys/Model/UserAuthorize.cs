using System;
using EZNEW.Develop.Domain.Aggregation;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Framework.Extension;
using EZNEW.Framework.ValueType;
using EZNEW.Develop.CQuery;
using EZNEW.Query.Sys;
using System.Threading.Tasks;

namespace EZNEW.Domain.Sys.Model
{
    /// <summary>
    /// 用户授权
    /// </summary>
    public class UserAuthorize : AggregationRoot<UserAuthorize>
    {
        #region	字段

        /// <summary>
        /// 用户
        /// </summary>
        protected LazyMember<User> user;

        /// <summary>
        /// 权限
        /// </summary>
        protected LazyMember<Authority> authority;

        /// <summary>
        /// 禁用
        /// </summary>
        protected bool disable;

        #endregion

        #region 构造方法

        /// <summary>
        /// 实例化用户授权对象
        /// </summary>
        internal UserAuthorize()
        {
            user = new LazyMember<User>(LoadUser);
            authority = new LazyMember<Authority>(LoadAuthority);
            repository = this.Instance<IUserAuthorizeRepository>();
        }

        #endregion

        #region	属性

        /// <summary>
        /// 用户
        /// </summary>
        public User User
        {
            get
            {
                return user.Value;
            }
            protected set
            {
                user.SetValue(value, false);
            }
        }

        /// <summary>
        /// 权限
        /// </summary>
        public Authority Authority
        {
            get
            {
                return authority.Value;
            }
            protected set
            {
                authority.SetValue(value, false);
            }
        }

        /// <summary>
        /// 禁用
        /// </summary>
        public bool Disable
        {
            get
            {
                return disable;
            }
            protected set
            {
                disable = value;
            }
        }

        #endregion

        #region 方法

        #region 功能方法

        #endregion

        #region 内部方法

        #region 加载用户

        /// <summary>
        /// 加载用户
        /// </summary>
        /// <returns></returns>
        User LoadUser()
        {
            if (!AllowLazyLoad(r => r.User))
            {
                return user.CurrentValue;
            }
            if (user.CurrentValue == null)
            {
                return null;
            }
            return this.Instance<IUserRepository>().Get(QueryFactory.Create<UserQuery>(r => r.SysNo == user.CurrentValue.SysNo));
        }

        #endregion

        #region 加载权限

        /// <summary>
        /// 加载权限
        /// </summary>
        /// <returns></returns>
        Authority LoadAuthority()
        {
            if (!AllowLazyLoad(r => r.Authority))
            {
                return authority.CurrentValue;
            }
            if (authority.CurrentValue == null)
            {
                return null;
            }
            return this.Instance<IAuthorityRepository>().Get(QueryFactory.Create<AuthorityQuery>(r => r.Code == authority.CurrentValue.Code));
        }

        #endregion

        #region 主标识值是否为空

        /// <summary>
        /// 主标识值是否为空
        /// </summary>
        /// <returns></returns>
        public override bool IdentityValueIsNone()
        {
            return authority.Value == null || user.Value == null;
        }

        #endregion

        #endregion

        #region 静态方法

        #region 创建用户授权

        /// <summary>
        /// 创建一个用户授权对象
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="authority">权限</param>
        /// <param name="disable">禁用权限</param>
        /// <returns></returns>
        public static UserAuthorize CreateUserAuthority(User user, Authority authority, bool disable = false)
        {
            return new UserAuthorize()
            {
                User = user,
                Authority = authority,
                Disable = disable
            };
        }

        protected override string GetIdentityValue()
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #endregion
    }
}