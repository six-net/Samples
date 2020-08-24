using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Entity.Sys;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Develop.Domain.Repository;

namespace EZNEW.Repository.Sys
{
    /// <summary>
    /// 授权操作组存储
    /// </summary>
    public class OperationGroupRepository : DefaultAggregationRepository<OperationGroup, OperationGroupEntity, IOperationGroupDataAccess>, IOperationGroupRepository
    {
    }
}
