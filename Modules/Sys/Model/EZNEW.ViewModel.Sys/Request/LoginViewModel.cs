using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.ViewModel.Sys.Request
{
    public class LoginViewModel
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName
        {
            get;
            set;
        }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Pwd
        {
            get;
            set;
        }

        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificationCode
        {
            get;
            set;
        }
    }
}
