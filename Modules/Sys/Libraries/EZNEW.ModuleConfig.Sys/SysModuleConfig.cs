using EZNEW.Configuration;
using EZNEW.Module.Sys;

namespace EZNEW.ModuleConfig.Sys
{
    public class SysModuleConfig : IModuleConfiguration
    {
        public void Init()
        {
            //数据库配置
            DataAccessConfig.Configure();
            //标识符生成配置
            SysManager.ConfigureIdentityKey();
            //对象映射转换配置
            ObjectMapConfig.Configure();
            //领域事件配置
            DomainEventConfig.Configure();
            //仓储事件配置
            RepositoryEventConfig.Configure();
        }
    }
}
