set projectRootName=EZNEWApp
set moduleName=Sys
set configPath=%cd%\%projectRootName%.ModuleConfig.%moduleName%.csproj
cd ../../
set topPath=%cd%

cd Business
for /f "delims=" %%i in ('dir /ad /b "%cd%"') do (
	if exist %cd%\%%i\%%i.csproj (
		dotnet add %configPath% reference %cd%\%%i\%%i.csproj
	)
)

cd %topPath%
cd Domain
for /f "delims=" %%i in ('dir /ad /b "%cd%"') do (
	if exist %cd%\%%i\%%i.csproj (
		dotnet add %configPath% reference %cd%\%%i\%%i.csproj
	)
)

cd %topPath%
cd DataAccess
for /f "delims=" %%i in ('dir /ad /b "%cd%"') do (
	if exist %cd%\%%i\%%i.csproj (
		dotnet add %configPath% reference %cd%\%%i\%%i.csproj
	)
)

cd %topPath%
cd Model
for /f "delims=" %%i in ('dir /ad /b "%cd%"') do (
	if exist %cd%\%%i\%%i.csproj (
		dotnet add %configPath% reference %cd%\%%i\%%i.csproj
	)
)

cd %topPath%
cd AppService
for /f "delims=" %%i in ('dir /ad /b "%cd%"') do (
	if exist %cd%\%%i\%%i.csproj (
		dotnet add %configPath% reference %cd%\%%i\%%i.csproj
	)
)