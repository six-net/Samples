using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 保存用户信息
    /// </summary>
    public class SaveUserCmdDto
    {
        #region 属性

        /// <summary>
        /// 保存用户
        /// </summary>
        public UserCmdDto User
        {
            get;set;
        }
        
        #endregion
    }
}
