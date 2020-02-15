using EZNEW.DataAccessContract.Sys;
using EZNEW.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Framework.Extension;
using EZNEW.Develop.DataAccess;

namespace EZNEW.DataAccess.Sys
{
    /// <summary>
    /// 权限数据访问
    /// </summary>
    public class AuthorityDataAccess : RdbDataAccess<AuthorityEntity>, IAuthorityDataAccess
    {
    }
}
