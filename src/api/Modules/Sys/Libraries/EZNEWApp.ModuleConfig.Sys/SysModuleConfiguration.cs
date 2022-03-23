using EZNEW.Module;
using EZNEWApp.Module.Sys;

namespace EZNEWApp.ModuleConfig.Sys
{
    public class SysModuleConfiguration : IModuleConfiguration
    {
        public void Configure()
        {
            //服务注册配置
            DependencyInjectionConfiguration.Configure();
            //对象映射配置
            ObjectMappingConfiguration.Configure();
            //领域业务配置
            DomainConfiguration.Configure();
            //仓储配置
            RepositoryConfiguration.Configure();
            //数据配置
            DataConfiguration.Configure();
            //查询配置
            QueryConfiguration.Configure();
        }
    }
}
