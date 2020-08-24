using EZNEW.Develop.Domain.Repository;
using EZNEW.Domain.Sys.Model;

namespace EZNEW.Domain.Sys.Repository
{
    /// <summary>
    /// 用户授权存储
    /// </summary>
    public interface IUserPermissionRepository : IAggregationRelationRepository<UserPermission, User, Permission>
    {
    }
}
