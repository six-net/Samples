using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 用户授权信息
    /// </summary>
    public class UserAuthorizeCmdDto
    {
        #region	属性

        /// <summary>
        /// 用户
        /// </summary>
        public UserCmdDto User
        {
            get;
            set;
        }

        /// <summary>
        /// 权限
        /// </summary>
        public AuthorityCmdDto Authority
        {
            get;
            set;
        }

        /// <summary>
        /// 禁用
        /// </summary>
        public bool Disable
        {
            get;
            set;
        }

        #endregion
    }
}
