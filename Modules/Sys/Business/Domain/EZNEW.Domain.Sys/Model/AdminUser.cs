using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Framework.Extension;
using EZNEW.Domain.Sys.Service;
using EZNEW.Develop.CQuery;
using EZNEW.Query.Sys;
using EZNEW.Framework.ValueType;
using EZNEW.Framework;
using System.Linq.Expressions;
using EZNEW.Framework.ExpressionUtil;
using EZNEW.Application.Identity.User;
using EZNEW.Framework.IoC;

namespace EZNEW.Domain.Sys.Model
{
    /// <summary>
    /// 管理用户
    /// </summary>
    public class AdminUser : User
    {
        #region 构造方法

        /// <summary>
        /// 初始化管理账户
        /// </summary>
        internal AdminUser() : base()
        {
            userType = UserType.管理账户;
        }

        #endregion

        #region 功能方法

        #region 公共方法

        #endregion

        #region 内部方法

        #endregion

        #region 静态方法

        #region 创建管理用户

        /// <summary>
        /// 创建一个管理账号对象
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="realName">真实名称</param>
        /// <param name="superUser">是否为超级用户</param>
        /// <returns>管理账号对象</returns>
        public static AdminUser CreateNewAdminUser(long userId, string userName = "", string pwd = "", string realName = "", bool superUser = false)
        {
            var user = new AdminUser()
            {
                SysNo = userId,
                UserName = userName,
                RealName = realName,
                SuperUser = superUser
            };
            user.ModifyPassword(pwd);
            return user;
        }

        #endregion

        #endregion

        #endregion
    }
}
