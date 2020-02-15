using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.ViewModel.Sys.Filter
{
    /// <summary>
    /// 权限绑定操作筛选信息
    /// </summary>
    public class AuthorityBindOperationFilterViewModel:AuthorityOperationFilterViewModel
    {
        /// <summary>
        /// 权限筛选信息
        /// </summary>
        public AuthorityFilterViewModel AuthorityFilter
        {
            get;set;
        }
    }
}
