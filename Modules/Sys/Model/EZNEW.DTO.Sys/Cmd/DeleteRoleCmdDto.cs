using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 角色删除命令对象
    /// </summary>
    public class DeleteRoleCmdDto
    {
        #region 属性

        /// <summary>
        /// 要删除的角色编号
        /// </summary>
        public IEnumerable<long> RoleIds
        {
            get;set;
        }

        #endregion
    }
}
