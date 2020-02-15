using EZNEW.DataAccessContract.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Entity.Sys;
using System.Data;
using EZNEW.Develop.DataAccess;
using EZNEW.Develop.CQuery;

namespace EZNEW.DataAccess.Sys
{
    /// <summary>
    /// 用户角色数据访问
    /// </summary>
    public class UserRoleDataAccess : RdbDataAccess<UserRoleEntity>, IUserRoleDbAccess
    {
    }
}
