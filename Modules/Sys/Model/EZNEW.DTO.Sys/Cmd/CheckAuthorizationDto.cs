using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 授权认证
    /// </summary>
    public class CheckAuthorizationDto
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 授权操作
        /// </summary>
        public OperationDto Operation { get; set; }
    }
}
