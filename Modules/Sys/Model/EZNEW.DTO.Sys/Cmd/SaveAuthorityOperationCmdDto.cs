using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 保存授权操作信息
    /// </summary>
    public class SaveAuthorityOperationCmdDto
    {
        /// <summary>
        /// 授权操作信息
        /// </summary>
        public AuthorityOperationCmdDto AuthorityOperation
        {
            get;set;
        }
    }
}
