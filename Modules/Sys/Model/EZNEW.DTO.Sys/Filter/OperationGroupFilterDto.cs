using System;
using System.Collections.Generic;
using EZNEW.Domain.Sys.Parameter.Filter;
using EZNEW.Paging;

namespace EZNEW.DTO.Sys.Filter
{
    /// <summary>
    /// 授权操作分组筛选信息
    /// </summary>
    public class OperationGroupFilterDto : PagingFilter
    {
        /// <summary>
        /// 编号
        /// </summary>
        public List<long> Ids { get; set; }

        /// <summary>
        /// 排除数据
        /// </summary>
        public List<long> ExcludeIds { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        public long? Parent { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int? Level { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }

        #region 筛选条件转换

        /// <summary>
        /// 筛选条件转换
        /// </summary>
        /// <returns></returns>
        public virtual OperationGroupFilter ConvertToFilter()
        {
            return this.MapTo<OperationGroupFilter>();
        }

        #endregion
    }
}
