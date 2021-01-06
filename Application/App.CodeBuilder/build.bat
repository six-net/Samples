set iocPath=%cd%\App.CodeBuilder.csproj
cd ../../
set topPath=%cd%

cd Modules
set modulePath=%cd%
for /f "delims=" %%i in ('dir /ad /b "%modulePath%"') do (
cd %%i\Model
for /f "delims=" %%c in ('dir /ad /b "*.Entity.*"') do (
dotnet add %iocPath% reference "%modulePath%\%%i\Model\%%c\%%c.csproj"
)
cd %modulePath%
)