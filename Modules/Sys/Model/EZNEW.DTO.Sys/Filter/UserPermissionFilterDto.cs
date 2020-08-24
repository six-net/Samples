using EZNEW.Domain.Sys.Parameter.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Filter
{
    /// <summary>
    /// 用户权限筛选
    /// </summary>
    public class UserPermissionFilterDto : PermissionFilterDto
    {
        /// <summary>
        /// 用户筛选信息
        /// </summary>
        public UserFilterDto UserFilter { get; set; }

        #region 筛选条件转换

        /// <summary>
        /// 筛选条件转换
        /// </summary>
        /// <returns></returns>
        public override PermissionFilter ConvertToFilter()
        {
            return this.MapTo<UserPermissionFilter>();
        }

        #endregion
    }
}
