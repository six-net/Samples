using EZNEW.Domain.Sys.Parameter.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Filter
{
    /// <summary>
    /// 授权操作绑定权限筛选
    /// </summary>
    public class OperationPermissionFilterDto : PermissionFilterDto
    {
        /// <summary>
        /// 授权操作筛选
        /// </summary>
        public OperationFilterDto OperationFilter { get; set; }

        #region 筛选条件转换

        /// <summary>
        /// 筛选条件转换
        /// </summary>
        /// <returns></returns>
        public override PermissionFilter ConvertToFilter()
        {
            return this.MapTo<OperationPermissionFilter>();
        }

        #endregion
    }
}
