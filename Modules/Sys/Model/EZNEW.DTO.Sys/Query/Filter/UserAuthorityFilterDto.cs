using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Query.Filter
{
    /// <summary>
    /// 用户权限筛选
    /// </summary>
    public class UserAuthorityFilterDto : AuthorityFilterDto
    {
        /// <summary>
        /// 用户筛选信息
        /// </summary>
        public UserFilterDto UserFilter
        {
            get; set;
        }
    }
}
