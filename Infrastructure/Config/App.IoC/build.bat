set iocPath=%cd%\App.IoC.csproj
cd ../../../
set topPath=%cd%

cd Modules
set modulePath=%cd%
for /f "delims=" %%i in ('dir /ad /b "%modulePath%"') do (
cd %%i\Libraries
for /f "delims=" %%c in ('dir /ad /b "*.ModuleConfig.*"') do (
dotnet add %iocPath% reference "%modulePath%\%%i\Libraries\%%c\%%c.csproj"
)
cd %modulePath%
)