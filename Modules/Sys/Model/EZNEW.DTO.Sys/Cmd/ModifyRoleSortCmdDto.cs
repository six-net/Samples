using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 修改角色排序
    /// </summary>
    public class ModifyRoleSortCmdDto
    {
        #region 属性

        /// <summary>
        /// 角色编号
        /// </summary>
        public long RoleSysNo
        {
            get;set;
        }

        /// <summary>
        /// 新的排序号
        /// </summary>
        public int NewSort
        {
            get;set;
        }

        #endregion
    }
}
