using EZNEW.Develop.DataAccess;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Entity.Sys;

namespace EZNEW.DataAccess.Sys
{
    /// <summary>
    /// 用户数据访问
    /// </summary>
    public class UserDataAccess : RdbDataAccess<UserEntity>, IUserDbAccess
    {
    }
}
