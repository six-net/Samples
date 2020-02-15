using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.ViewModel.Sys.Request
{
    /// <summary>
    /// 修改密码
    /// </summary>
    public class ModifyPasswordViewModel
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
        /// 确认新密码
        /// </summary>
        public string ConfirmNewPassword
        {
            get; set;
        }

        #endregion
    }
}
