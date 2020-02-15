using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 验证权限编码是否存在
    /// </summary>
    public class ExistAuthorityCodeCmdDto
    {
        /// <summary>
        /// 权限系统编号
        /// </summary>
        public long SysNo
        {
            get;set;
        }

        /// <summary>
        /// 权限编码
        /// </summary>
        public string AuthCode
        {
            get;set;
        }
    }
}
