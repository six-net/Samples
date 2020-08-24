using EZNEW.Develop.Domain.Repository;
using EZNEW.Domain.Sys.Model;

namespace EZNEW.Domain.Sys.Repository
{
    /// <summary>
    /// 用户存储
    /// </summary>
    public interface IUserRepository : IAggregationRepository<User>
    {
    }
}
