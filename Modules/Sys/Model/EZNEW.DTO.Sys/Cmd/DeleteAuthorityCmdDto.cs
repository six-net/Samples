using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 权限删除命令信息
    /// </summary>
    public class DeleteAuthorityCmdDto
    {
        #region 属性

        /// <summary>
        /// 权限编号
        /// </summary>
        public IEnumerable<long> SysNos
        {
            get;set;
        }

        #endregion
    }
}
