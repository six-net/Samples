using EZNEW.Develop.DataAccess;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Framework.Extension;

namespace EZNEW.DataAccess.Sys
{
    /// <summary>
    /// 用户授权数据访问
    /// </summary>
    public class UserAuthorizeDataAccess : RdbDataAccess<UserAuthorizeEntity>, IUserAuthorizeDataAccess
    {
    }
}
