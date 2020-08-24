using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.DTO.Sys
{
    /// <summary>
    /// 权限授权操作
    /// </summary>
    public class PermissionOperationDto
    {
        /// <summary>
        /// 权限编号
        /// </summary>
        public long PermissionId { get; set; }

        /// <summary>
        /// 操作功能编号
        /// </summary>
        public long OperationId { get; set; }
    }
}
