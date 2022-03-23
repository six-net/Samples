using System;
using EZNEW.CodeBuilder;

namespace App.CodeBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            //解决方案名称
            CodeBuilderManager.SolutionName = "EZNEWApp";
            //根命名空间
            CodeBuilderManager.RootNamespace = "EZNEWApp";
            //.NET版本
            CodeBuilderManager.NetVersion = "net5.0";
            //项目结构
            CodeBuilderManager.DefaultProjectStructure = CodeBuild.ProjectStructure.简化模式;
            CodeBuilderManager.Run();
        }
    }
}
