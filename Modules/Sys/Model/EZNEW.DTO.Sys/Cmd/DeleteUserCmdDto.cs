using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 删除管理账户
    /// </summary>
    public class DeleteUserCmdDto
    {
        #region 属性

        /// <summary>
        /// 要删除的用户编号
        /// </summary>
        public IEnumerable<long> UserIds
        {
            get; set;
        }

        #endregion
    }
}
