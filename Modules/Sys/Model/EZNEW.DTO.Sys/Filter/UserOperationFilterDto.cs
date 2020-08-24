using EZNEW.Domain.Sys.Parameter.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.DTO.Sys.Filter
{
    /// <summary>
    /// 用户操作功能筛选
    /// </summary>
    public class UserOperationFilterDto : OperationFilterDto
    {
        /// <summary>
        /// 用户筛选条件
        /// </summary>
        public UserFilterDto UserFilter { get; set; }

        #region 筛选条件转换

        /// <summary>
        /// 筛选条件转换
        /// </summary>
        /// <returns></returns>
        public override OperationFilter ConvertToFilter()
        {
            return this.MapTo<UserOperationFilter>();
        }

        #endregion
    }
}
