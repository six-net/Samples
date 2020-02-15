using EZNEW.Develop.CQuery;
using EZNEW.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Query.Sys
{
    /// <summary>
    /// 权限&操作关联查询对象
    /// </summary>
    [QueryEntity(typeof(AuthorityBindOperationEntity))]
    public class AuthorityBindOperationQuery : QueryModel<AuthorityBindOperationQuery>
    {
        #region	属性

        /// <summary>
        /// 授权操作
        /// </summary>
        public long AuthorityOperationSysNo
        {
            get;
            set;
        }

        /// <summary>
        /// 权限
        /// </summary>
        public long AuthoritySysNo
        {
            get;
            set;
        }

        #endregion
    }
}
