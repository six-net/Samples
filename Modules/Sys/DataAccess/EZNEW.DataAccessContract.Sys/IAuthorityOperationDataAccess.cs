using EZNEW.Develop.DataAccess;
using EZNEW.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DataAccessContract.Sys
{
    /// <summary>
    /// 授权操作数据访问接口
    /// </summary>
    public interface IAuthorityOperationDataAccess : IDataAccess<AuthorityOperationEntity>
    {
    }
}
