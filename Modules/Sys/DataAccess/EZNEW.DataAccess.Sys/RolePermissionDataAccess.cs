using EZNEW.Develop.DataAccess;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Entity.Sys;

namespace EZNEW.DataAccess.Sys
{
    /// <summary>
    /// 角色权限数据访问
    /// </summary>
    public class RolePermissionDataAccess : RdbDataAccess<RolePermissionEntity>, IRoleAuthorizeDbAccess
    {
    }
}
