using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Domain.Sys.Parameter.Filter;

namespace EZNEW.DTO.Sys.Filter
{
    /// <summary>
    /// 权限绑定的操作筛选信息
    /// </summary>
    public class PermissionOperationFilterDto : OperationFilterDto
    {
        /// <summary>
        /// 权限筛选信息
        /// </summary>
        public PermissionFilterDto PermissionFilter { get; set; }

        #region 筛选条件转换

        /// <summary>
        /// 筛选条件转换
        /// </summary>
        /// <returns></returns>
        public override OperationFilter ConvertToFilter()
        {
            return this.MapTo<PermissionOperationFilter>();
        }

        #endregion
    }
}
