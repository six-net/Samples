using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 修改密码信息
    /// </summary>
    public class ModifyPasswordCmdDto
    {
        #region 属性

        /// <summary>
        /// 用户编号
        /// </summary>
        public long SysNo
        {
            get;
            set;
        }

        /// <summary>
        /// 当前密码
        /// </summary>
        public string NowPassword
        {
            get; set;
        }

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword
        {
            get; set;
        }

        /// <summary>
        /// 是否验证现有密码
        /// </summary>
        public bool CheckOldPassword
        {
            get;set;
        }

        #endregion
    }
}
