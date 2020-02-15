using EZNEW.Application.Identity.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Domain.Sys.Service.Param
{
    /// <summary>
    /// 修改权限状态信息
    /// </summary>
    public class ModifyAuthorityStatus
    {
        /// <summary>
        /// 权限码
        /// </summary>
        public string Code
        {
            get;set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public AuthorityStatus Status
        {
            get;set;
        }
    }
}
