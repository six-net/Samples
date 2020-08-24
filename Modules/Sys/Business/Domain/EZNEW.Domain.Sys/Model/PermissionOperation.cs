using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.Domain.Sys.Model
{
    /// <summary>
    /// 权限授权操作
    /// </summary>
    public class PermissionOperation
    {
        /// <summary>
        /// 权限编号
        /// </summary>
        public long PermissionId { get; set; }

        /// <summary>
        /// 操作编号
        /// </summary>
        public long OperationId { get; set; }
    }
}
