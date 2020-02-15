using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Framework.Extension;
using EZNEW.Application.Identity.User;

namespace EZNEW.Repository.Sys
{
    /// <summary>
    /// 管理用户存储
    /// </summary>
    public class AdminUserRepository : IAdminUserRepository
    {
        #region 加载管理账户信息

        /// <summary>
        /// 加载管理账户信息
        /// </summary>
        /// <param name="users">用户信息</param>
        public List<User> LoadAdminUser(IEnumerable<User> users)
        {
            if (users.IsNullOrEmpty())
            {
                return new List<User>(0);
            }
            List<User> newUserList = new List<User>();
            foreach (var user in users)
            {
                if (user == null)
                {
                    continue;
                }
                if (user.UserType != UserType.管理账户)
                {
                    newUserList.Add(user);
                }
                else
                {
                    newUserList.Add(user.MapTo<AdminUser>());
                }
            }
            return newUserList;
        }

        #endregion
    }
}
