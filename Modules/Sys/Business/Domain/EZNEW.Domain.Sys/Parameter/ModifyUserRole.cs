using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Develop.Domain;
using EZNEW.Domain.Sys.Model;

namespace EZNEW.Domain.Sys.Parameter
{
    /// <summary>
    /// 修改用户&角色绑定关系
    /// </summary>
    public class ModifyUserRole : IDomainParameter
    {
        /// <summary>
        /// 绑定信息
        /// </summary>
        public IEnumerable<UserRole> Bindings { get; set; }

        /// <summary>
        /// 解绑信息
        /// </summary>
        public IEnumerable<UserRole> Unbindings { get; set; }
    }
}
