using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 保存授权操作分组信息
    /// </summary>
    public class SaveAuthorityOperationGroupCmdDto
    {
        /// <summary>
        /// 授权操作分组信息
        /// </summary>
        public AuthorityOperationGroupCmdDto AuthorityOperationGroup
        {
            get;set;
        }
    }
}
