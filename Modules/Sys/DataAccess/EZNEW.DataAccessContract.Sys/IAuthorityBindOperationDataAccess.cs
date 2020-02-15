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
    /// 权限&授权操作关联数据访问接口
    /// </summary>
    public interface IAuthorityBindOperationDataAccess : IDataAccess<AuthorityBindOperationEntity>
    {
    }
}
