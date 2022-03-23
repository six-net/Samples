# Samples

示例应用

# 快速开始

1. 下载安装 [.NetCore5.0 SDK](https://dotnet.microsoft.com/download/dotnet-core/5.0)
2. 下载安装 [Visual Studio 2019](https://visualstudio.microsoft.com/zh-hans/downloads/)
3. EZNEW备用Nuget包地址：<b>http://nuget.eznew.net/v3/index.json</b>
4. 下载项目:git clone https://github.com/eznew-net/Samples.git
5. 默认使用MySQL8+数据库，在<b>/src/api/Application/Api.Console/appsettings.json</b>中配置数据库连接
6. 创建数据库
	* [使用EF Migration创建数据库](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)
	
		1. 打开命令行工具cmd.exe
		
		2. 安装ef工具：<b>dotnet tool install --global dotnet-ef</b>
		
		3. 导航到路径 <b>/src/api/Application/App.EntityMigration</b> 并运行命令: <b>dotnet ef migrations add init</b>
		
		4. 运行命令：<b>dotnet ef database update</b>
	
7. 编译运行 <b>Api.Console</b> 
8. 导航到路径 <b>src/web</b>,运行命令 <b>npm install cnpm -g --registry=https://registry.nlark.com</b>
9. 运行命令 <b>cnpm install</b>
10. 运行命令  <b>npm run serve</b>
11. 使用默认用户名密码登录：<b>admin</b> and default Password：<b>admin</b>
