using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 权限分组编辑信息
    /// </summary>
    public class SaveAuthorityGroupCmdDto
    {
        #region 属性

        /// <summary>
        /// 分组信息
        /// </summary>
        public AuthorityGroupCmdDto AuthorityGroup
        {
            get;set;
        }

        #endregion
    }
}
