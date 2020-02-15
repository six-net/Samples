using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.DTO.Sys.Query.Filter
{
    /// <summary>
    /// 用户角色筛选
    /// </summary>
    public class UserRoleFilterDto : RoleFilterDto
    {
        /// <summary>
        /// 用户筛选
        /// </summary>
        public UserFilterDto UserFilter
        {
            get; set;
        }
    }
}
