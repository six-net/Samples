using EZNEW.Domain.Sys.Parameter.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.DTO.Sys.Filter
{
    /// <summary>
    /// 用户角色筛选
    /// </summary>
    public class UserRoleFilterDto : RoleFilterDto
    {
        /// <summary>
        /// 用户筛选
        /// </summary>
        public UserFilterDto UserFilter { get; set; }

        #region 筛选条件转换

        /// <summary>
        /// 筛选条件转换
        /// </summary>
        /// <returns></returns>
        public override RoleFilter ConvertToFilter()
        {
            return this.MapTo<UserRoleFilter>();
        } 

        #endregion
    }
}
