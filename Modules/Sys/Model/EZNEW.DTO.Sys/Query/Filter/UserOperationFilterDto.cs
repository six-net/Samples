using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.DTO.Sys.Query.Filter
{
    /// <summary>
    /// 用户操作功能筛选
    /// </summary>
    public class UserOperationFilterDto : AuthorityOperationFilterDto
    {
        /// <summary>
        /// 用户筛选条件
        /// </summary>
        public UserFilterDto UserFilter
        {
            get; set;
        }
    }
}
