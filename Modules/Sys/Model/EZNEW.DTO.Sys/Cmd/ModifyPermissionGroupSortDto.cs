using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 修改权限分组排序
    /// </summary>
    public class ModifyPermissionGroupSortDto
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
