using EZNEW.Data.Cache;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Entity.Sys;

namespace EZNEW.CacheDataAccess.Sys
{
    /// <summary>
    /// 用户缓存数据访问
    /// </summary>
    public class UserCacheDataAccess : DefaultCacheDataAccess<IUserDbAccess, UserEntity>, IUserDataAccess
    {
    }
}
