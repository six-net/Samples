using System.Collections.Generic;
using EZNEW.Develop.Command;
using Microsoft.Extensions.Configuration;
using EZNEW.Framework.IoC;
using EZNEW.Data.SqlServer;
using EZNEW.Data;

namespace App.DBConfig
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DbConfig
    {
        public static void Init()
        {
            DataBaseEngineConfig();//数据库执行器
            DataManager.GetDBServer = GetServerInfo;//获取数据连接信息方法
        }

        /// <summary>
        /// 数据库执行器配置
        /// </summary>
        static void DataBaseEngineConfig()
        {
            DataManager.RegisterDBEngine(ServerType.SQLServer, new SqlServerEngine());
        }

        /// <summary>
        /// 获取数据库服务器
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        static List<ServerInfo> GetServerInfo(ICommand command)
        {
            List<ServerInfo> servers = new List<ServerInfo>();
            servers.Add(new ServerInfo()
            {
                ServerType = ServerType.SQLServer,
                ConnectionString = ContainerManager.Resolve<IConfiguration>().GetConnectionString("DefaultConnection")
            });
            return servers;
        }
    }
}
