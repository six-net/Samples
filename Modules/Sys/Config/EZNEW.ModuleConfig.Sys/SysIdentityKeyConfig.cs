using EZNEW.Application.Identity;
using EZNEW.Framework.Code;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.ModuleConfig.Sys
{
    /// <summary>
    /// sys identity key config
    /// </summary>
    public static class SysIdentityKeyConfig
    {
        public static void Config()
        {
            List<string> groupCodes = new List<string>();

            Array values = Enum.GetValues(IdentityGroup.授权操作.GetType());
            foreach (IdentityGroup group in values)
            {
                groupCodes.Add(IdentityApplicationHelper.GetIdGroupCode(group));
            }
            SerialNumber.RegisterGenerator(groupCodes, 1, 1);
        }
    }
}
