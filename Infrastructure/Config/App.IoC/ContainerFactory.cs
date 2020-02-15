using EZNEW.Framework.Drawing;
using EZNEW.Framework.IoC;
using EZNEW.Framework.Serialize;
using EZNEW.Web.Utility;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Web.DI;
using EZNEW.VerificationCode.SkiaSharp;
namespace App.IoC
{
    public class ContainerFactory : IServiceProviderFactory<IDIContainer>
    {
        /// <summary>
        /// 自定义服务注入
        /// </summary>
        /// <param name="container"></param>
        static void RegisterServices(IDIContainer container)
        {
            WebDependencyInjectionManager.RegisterDefaultService();
            container.Register(typeof(VerificationCodeBase), typeof(SkiaSharpVerificationCode));
        }

        public IDIContainer CreateBuilder(IServiceCollection services)
        {
            ContainerManager.Init(services, serviceRegisterAction: RegisterServices);
            return ContainerManager.Container;
        }

        public IServiceProvider CreateServiceProvider(IDIContainer containerBuilder)
        {
            return containerBuilder.BuildServiceProvider();
        }
    }
}
