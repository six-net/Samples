using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Query.Filter
{
    /// <summary>
    /// 权限绑定的操作筛选信息
    /// </summary>
    public class AuthorityBindOperationFilterDto : AuthorityOperationFilterDto
    {
        /// <summary>
        /// 权限筛选信息
        /// </summary>
        public AuthorityFilterDto AuthorityFilter
        {
            get; set;
        }
    }
}
