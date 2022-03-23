set currentPath=%cd%
set entityMigrationPath=%cd%\App.EntityMigration.csproj
cd ../
set appRootPath=%cd%
cd ../
set topPath=%cd%

cd Modules
set modulePath=%cd%
for /f "delims=" %%i in ('dir /ad /b "%modulePath%"') do (
cd %%i\Libraries
for /f "delims=" %%c in ('dir /ad /b "*.ModuleConfig.*"') do (
dotnet add %entityMigrationPath% reference "%modulePath%\%%i\Libraries\%%c\%%c.csproj"
)
cd %modulePath%
)
cd %currentPath%
copy ..\Api.Console\appsettings.json .\appsettings.json