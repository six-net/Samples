using EZNEW.Framework.Drawing;
using EZNEW.Framework.Extension;
using EZNEW.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        static readonly string LoginVerificationCodeKey = Client.Host + "_eznew.net".MD5();

        #region 登陆

        /// <summary>
        /// 刷新登陆验证码
        /// </summary>
        /// <returns>登陆验证码图片数据</returns>
        public static byte[] RefreshLoginCode()
        {
            var codeObj = VerificationCodeFactory.GetVerificationCode();
            codeObj.CodeType = VerificationCodeType.Number;
            var byteValues = codeObj.CreateCode();
            CookieHelper.SetCookieValue(LoginVerificationCodeKey, codeObj.Code);
            return byteValues;
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
            if (code.IsNullOrEmpty())
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
