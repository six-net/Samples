using EZNEW.Domain.Sys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Domain.Sys.Repository
{
    /// <summary>
    /// 管理用户存储接口
    /// </summary>
    public interface IAdminUserRepository
    {
        #region 加载管理账户信息

        /// <summary>
        /// 加载管理账户信息
        /// </summary>
        /// <param name="users">用户信息</param>
        List<User> LoadAdminUser(IEnumerable<User> users);

        #endregion
    }
}
