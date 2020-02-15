using EZNEW.Application.Identity.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 修改授权操作状态信息
    /// </summary>
    public class ModifyAuthorityOperationStatusCmdDto
    {
        #region 属性

        /// <summary>
        /// 状态信息
        /// </summary>
        public Dictionary<long, AuthorityOperationStatus> StatusInfo
        {
            get;set;
        }

        #endregion
    }
}
