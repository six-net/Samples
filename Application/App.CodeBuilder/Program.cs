using EZNEW.CodeBuilder;
using System;

namespace App.CodeBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            //解决方案名称
            CodeBuilderManager.SolutionName = "Demo.Core3.0";
            //根命名空间
            CodeBuilderManager.RootNamespace = "EZNEW";
            //.NET版本
            CodeBuilderManager.NetVersion = CodeBuild.Net.NetVersion.netcore3_0;
            CodeBuilderManager.Run();
        }
    }
}
