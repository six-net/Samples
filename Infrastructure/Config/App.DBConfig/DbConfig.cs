using System.Collections.Generic;
using EZNEW.Develop.Command;
using Microsoft.Extensions.Configuration;
using EZNEW.Data.SqlServer;
using EZNEW.Data;
using EZNEW.DependencyInjection;

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
            DataManager.ConfigureDatabaseServer(GetServerInfo);//获取数据连接信息方法
        }

        /// <summary>
        /// 数据库执行器配置
        /// </summary>
        static void DataBaseEngineConfig()
        {
            DataManager.ConfigureDatabaseEngine(DatabaseServerType.SQLServer, new SqlServerEngine());
        }

        /// <summary>
        /// 获取数据库服务器
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        static List<DatabaseServer> GetServerInfo(ICommand command)
        {
            List<DatabaseServer> servers = new List<DatabaseServer>();
            servers.Add(new DatabaseServer()
            {
                ServerType = DatabaseServerType.SQLServer,
                ConnectionString = ContainerManager.Resolve<IConfiguration>().GetConnectionString("DefaultConnection")
            });
            return servers;
        }
    }
}
