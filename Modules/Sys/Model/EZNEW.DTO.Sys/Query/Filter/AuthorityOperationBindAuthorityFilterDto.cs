using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Query.Filter
{
    /// <summary>
    /// 授权操作绑定权限筛选
    /// </summary>
    public class AuthorityOperationBindAuthorityFilterDto : AuthorityFilterDto
    {
        #region 数据筛选

        /// <summary>
        /// 授权操作筛选
        /// </summary>
        public AuthorityOperationFilterDto AuthorityOperationFilter
        {
            get; set;
        }

        #endregion
    }
}
