using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 修改分组排序
    /// </summary>
    public class ModifyAuthorityOperationGroupSortCmdDto
    {
        #region 属性

        /// <summary>
        /// 分组编号
        /// </summary>
        public long AuthorityOperationGroupSysNo
        {
            get; set;
        }

        /// <summary>
        /// 排序号
        /// </summary>
        public int NewSort
        {
            get;set;
        }

        #endregion
    }
}
