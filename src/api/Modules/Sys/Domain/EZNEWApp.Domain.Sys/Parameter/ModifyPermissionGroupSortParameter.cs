using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 修改权限分组排序
    /// </summary>
    public class ModifyPermissionGroupSortParameter
    {
        /// <summary>
        /// 分组编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 新的排序
        /// </summary>
        public int NewSort { get; set; }
    }
}
