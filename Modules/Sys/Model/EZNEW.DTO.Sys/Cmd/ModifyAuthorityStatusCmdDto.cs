using EZNEW.Application.Identity.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 修改权限状态信息
    /// </summary>
    public class ModifyAuthorityStatusCmdDto
    {
        #region 属性

        /// <summary>
        /// 要修改的权限状态信息
        /// </summary>
        public Dictionary<string, AuthorityStatus> AuthorityStatusInfo
        {
            get;set;
        }

        #endregion
    }
}
