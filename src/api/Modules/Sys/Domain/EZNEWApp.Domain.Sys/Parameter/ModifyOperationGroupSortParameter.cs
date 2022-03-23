using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 修改分组排序
    /// </summary>
    public class ModifyOperationGroupSortParameter
    {
        /// <summary>
        /// 分组编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int NewSort { get; set; }
    }
}
