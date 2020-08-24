using EZNEW.Drawing;
using EZNEW.DataValidation;
using EZNEW.Web.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using EZNEW.Drawing.VerificationCode;

namespace Site.Console.Util
{
    /// <summary>
    /// 验证码管理
    /// </summary>
    public class VerificationCodeHelper
    {
        /// <summary>
        /// 登陆验证码Cookie键值
        /// </summary>
        static readonly string LoginVerificationCodeKey = HttpClientHelper.Host + "_eznew.net".MD5();

        #region 登陆

        /// <summary>
        /// 刷新登陆验证码
        /// </summary>
        /// <returns>登陆验证码图片数据</returns>
        public static byte[] RefreshLoginCode()
        {
            var codeObj = VerificationCodeFactory.GetVerificationCodeProvider();
            codeObj.CodeType = VerificationCodeType.Number;
            var verificationValue = codeObj.CreateCode();
            CookieHelper.SetCookieValue(LoginVerificationCodeKey, verificationValue.Code);
            return verificationValue.FileBytes;
        }

        /// <summary>
        /// 移除登陆验证码
        /// </summary>
        public static void RemoveLoginCode()
        {
            CookieHelper.RemoveCookie(LoginVerificationCodeKey);
        }

        /// <summary>
        /// 验证登陆验证码
        /// </summary>
        /// <param name="code">验证码</param>
        /// <param name="caseSensitive">区分大小写</param>
        /// <returns></returns>
        public static bool CheckLoginCode(string code, bool caseSensitive = false)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return false;
            }
            string vcodeValue = CookieHelper.GetCookieValue(LoginVerificationCodeKey);
            RemoveLoginCode();
            if (caseSensitive)
            {
                return code == vcodeValue;
            }
            return code.ToLower() == vcodeValue.ToLower();
        }

        #endregion
    }
}
