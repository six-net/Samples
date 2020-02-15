using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 授权验证
    /// </summary>
    public class AuthenticationCmdDto
    {
        /// <summary>
        /// 用户
        /// </summary>
        public UserCmdDto User
        {
            get; set;
        }

        /// <summary>
        /// 授权操作
        /// </summary>
        public AuthorityOperationCmdDto Operation
        {
            get; set;
        }
    }
}
