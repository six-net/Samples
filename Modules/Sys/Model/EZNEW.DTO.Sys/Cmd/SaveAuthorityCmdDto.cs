using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 保存权限信息
    /// </summary>
    public class SaveAuthorityCmdDto
    {
        /// <summary>
        /// 权限信息
        /// </summary>
        public AuthorityCmdDto Authority
        {
            get;set;
        }
    }
}
