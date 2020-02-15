using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Query.Filter
{
    /// <summary>
    /// 角色用户
    /// </summary>
    public class RoleAuthorityFilterDto : AuthorityFilterDto
    {
        /// <summary>
        /// 角色筛选
        /// </summary>
        public RoleFilterDto RoleFilter
        {
            get; set;
        }
    }
}
