using EZNEW.Develop.Domain.Repository;
using EZNEW.Domain.Sys.Model;

namespace EZNEW.Domain.Sys.Repository
{
    /// <summary>
    /// 用户角色绑定存储管理
    /// </summary>
    public interface IUserRoleRepository : IRelationRepository<User, Role>
    {
    }
}
