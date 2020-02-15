set iocPath=%cd%\App.IoC.csproj
cd ../../../
set topPath=%cd%

cd Infrastructure\Contract\BusinessContract
for /f "delims=" %%i in ('dir /ad /b "%cd%"') do (
dotnet add %iocPath% reference %cd%\%%i\%%i.csproj
)

cd %topPath%
cd Infrastructure\Contract\DataAccessContract
for /f "delims=" %%i in ('dir /ad /b "%cd%"') do (
dotnet add %iocPath% reference %cd%\%%i\%%i.csproj
)

cd %topPath%
cd Infrastructure\DataAccess
for /f "delims=" %%i in ('dir /ad /b "%cd%"') do (
dotnet add %iocPath% reference %cd%\%%i\%%i.csproj
)

cd %topPath%
cd Infrastructure\Repository
for /f "delims=" %%i in ('dir /ad /b "%cd%"') do (
dotnet add %iocPath% reference %cd%\%%i\%%i.csproj
)

cd %topPath%
cd Business\Implement
for /f "delims=" %%i in ('dir /ad /b "%cd%"') do (
dotnet add %iocPath% reference %cd%\%%i\%%i.csproj
)

cd %topPath%
cd Business\Domain
for /f "delims=" %%i in ('dir /ad /b "%cd%"') do (
dotnet add %iocPath% reference %cd%\%%i\%%i.csproj
)

cd %topPath%
cd Service
for /f "delims=" %%i in ('dir /ad /b "%cd%"') do (
dotnet add %iocPath% reference %cd%\%%i\%%i.csproj
)