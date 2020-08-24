using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Entity.Sys;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Develop.Domain.Repository;

namespace EZNEW.Repository.Sys
{
    /// <summary>
    /// 权限分组存储
    /// </summary>
    public class PermissionGroupRepository : DefaultAggregationRepository<PermissionGroup, PermissionGroupEntity, IPermissionGroupDataAccess>, IPermissionGroupRepository
    {
    }
}
