using EZNEW.Framework.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.ViewModel.Sys.Filter
{
    /// <summary>
    /// 授权操作绑定的权限筛选信息
    /// </summary>
    public class AuthorityOperationBindAuthorityFilterViewModel : AuthorityFilterViewModel
    {
        /// <summary>
        /// 授权操作筛选
        /// </summary>
        public AuthorityOperationFilterViewModel AuthorityOperationFilter
        {
            get; set;
        }
    }
}
