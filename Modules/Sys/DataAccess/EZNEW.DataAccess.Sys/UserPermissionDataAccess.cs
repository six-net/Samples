using EZNEW.Develop.DataAccess;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Entity.Sys;

namespace EZNEW.DataAccess.Sys
{
    /// <summary>
    /// 用户授权数据访问
    /// </summary>
    public class UserPermissionDataAccess : RdbDataAccess<UserPermissionEntity>, IUserPermissionDataAccess
    {
    }
}
