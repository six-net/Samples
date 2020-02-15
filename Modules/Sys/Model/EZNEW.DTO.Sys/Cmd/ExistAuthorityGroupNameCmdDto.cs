using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 验证权限分组名称是否存在
    /// </summary>
    public class ExistAuthorityGroupNameCmdDto
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName
        {
            get;set;
        }

        /// <summary>
        /// 排除验证分组编号
        /// </summary>
        public long ExcludeGroupId
        {
            get;set;
        }
    }
}
