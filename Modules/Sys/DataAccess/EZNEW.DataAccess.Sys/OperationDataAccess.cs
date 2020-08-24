using EZNEW.Develop.DataAccess;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Entity.Sys;

namespace EZNEW.DataAccess.Sys
{
    /// <summary>
    /// 授权操作数据访问
    /// </summary>
    public class OperationDataAccess : RdbDataAccess<OperationEntity>, IOperationDataAccess
    {
    }
}
