using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Domain.Sys.Parameter.Filter;

namespace EZNEW.DTO.Sys.Filter
{
    /// <summary>
    /// 角色用户
    /// </summary>
    public class RolePermissionFilterDto : PermissionFilterDto
    {
        /// <summary>
        /// 角色筛选
        /// </summary>
        public RoleFilterDto RoleFilter { get; set; }

        #region 筛选条件转换

        /// <summary>
        /// 筛选条件转换
        /// </summary>
        /// <returns></returns>
        public override PermissionFilter ConvertToFilter()
        {
            return this.MapTo<RolePermissionFilter>();
        }

        #endregion
    }
}
