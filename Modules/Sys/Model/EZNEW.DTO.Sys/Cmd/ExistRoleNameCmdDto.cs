using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 验证角色名是否存在
    /// </summary>
    public class ExistRoleNameCmdDto
    {
        #region 属性
        
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        {
            get;set;
        }

        /// <summary>
        /// 排除验证的角色
        /// </summary>
        public long ExcludeRoleId
        {
            get;set;
        }

        #endregion
    }
}
