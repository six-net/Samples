using EZNEW.Application.Identity.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Domain.Sys.Service.Param
{
    /// <summary>
    /// 修改授权操作状态信息
    /// </summary>
    public class ModifyAuthorityOperationStatus
    {
        /// <summary>
        /// 操作编号
        /// </summary>
        public long OperationId
        {
            get;set;
        }

        /// <summary>
        /// 操作状态
        /// </summary>
        public AuthorityOperationStatus Status
        {
            get;set;
        }
    }
}
