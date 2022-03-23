using System;
using System.Collections.Generic;
using EZNEW.Code;

namespace EZNEWApp.Module.Sys
{
    public static class SysManager
    {
        #region 对象标识值

        /// <summary>
        /// 根据指定的模块对象生成获取一个Id
        /// </summary>
        /// <param name="moduleObject">模块对象类型</param>
        /// <returns></returns>
        public static long GetId<T>()
        {
            return SerialNumber.GenerateSerialNumber<T>();
        }

        #endregion
    }
}
