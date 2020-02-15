using EZNEW.Domain.Sys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Domain.Sys.Service.Param
{
    /// <summary>
    /// 修改操作绑定权限
    /// </summary>
    public class ModifyAuthorityOperationBindAuthority
    {
        #region 属性

        /// <summary>
        /// 授权操作编号
        /// </summary>
        public long AuthorityOperationId
        {
            get; set;
        }

        /// <summary>
        /// 新加的权限
        /// </summary>
        public IEnumerable<Authority> NewAuthoritys
        {
            get; set;
        }

        /// <summary>
        /// 要移除的权限
        /// </summary>
        public IEnumerable<Authority> RemoveAuthoritys
        {
            get; set;
        }

        #endregion
    }
}
