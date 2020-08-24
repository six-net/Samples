using System;
using System.Collections.Generic;
using EZNEW.Code;

namespace EZNEW.Module.Sys
{
    public static class SysManager
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public const string ModuleName = "Sys";

        #region 对象标识值

        /// <summary>
        /// 配置对象标识值
        /// </summary>
        public static void ConfigureIdentityKey()
        {
            List<string> groupCodes = new List<string>();

            Array values = Enum.GetValues(SysModuleObject.Operation.GetType());
            foreach (SysModuleObject group in values)
            {
                groupCodes.Add(GetIdGroup(group));
            }
            SerialNumber.RegisterGenerator(groupCodes, 1, 1);
        }

        /// <summary>
        /// 获取模块对象Id分组名称
        /// </summary>
        /// <param name="moduleObject">模块对象类型</param>
        public static string GetIdGroup(SysModuleObject moduleObject)
        {
            return $"{ModuleName}_{(int)moduleObject}";
        }

        /// <summary>
        /// 根据指定的模块对象生成获取一个Id
        /// </summary>
        /// <param name="moduleObject">模块对象类型</param>
        /// <returns></returns>
        public static long GetId(SysModuleObject moduleObject)
        {
            var idGroup = GetIdGroup(moduleObject);
            return SerialNumber.GenerateSerialNumber(idGroup);
        }

        #endregion
    }
}
