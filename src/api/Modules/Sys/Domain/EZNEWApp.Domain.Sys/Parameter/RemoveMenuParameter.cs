using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Development.Domain;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 删除菜单
    /// </summary>
    public class RemoveMenuParameter : IDomainParameter
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        public List<long> Ids { get; set; }
    }
}
