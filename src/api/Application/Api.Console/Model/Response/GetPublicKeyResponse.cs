using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Console.Model.Response
{
    /// <summary>
    /// 获取公钥响应信息
    /// </summary>
    public class GetPublicKeyResponse
    {
        /// <summary>
        /// 获取或设置公钥值
        /// </summary>
        public string PublicKey { get; set; }
    }
}
