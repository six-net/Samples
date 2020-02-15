using EZNEW.Develop.DataAccess;
using EZNEW.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DataAccessContract.Sys
{
    /// <summary>
    /// 用户角色数据访问
    /// </summary>
    public interface IUserRoleDataAccess : IDataAccess<UserRoleEntity>
    {
    }

    public interface IUserRoleDbAccess : IUserRoleDataAccess
    { }
}
