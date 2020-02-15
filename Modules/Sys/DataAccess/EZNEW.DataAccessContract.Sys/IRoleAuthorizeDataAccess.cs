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
    /// 角色权限数据访问接口
    /// </summary>
    public interface IRoleAuthorizeDataAccess : IDataAccess<RoleAuthorizeEntity>
    {
    }

    /// <summary>
    /// 角色权限数据库接口
    /// </summary>
    public interface IRoleAuthorizeDbAccess : IRoleAuthorizeDataAccess
    {
    }
}
