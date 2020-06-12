set projectRootName=EZNEW
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
cd Business\Domain
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
cd Service
for /f "delims=" %%i in ('dir /ad /b "%cd%"') do (
	if exist %cd%\%%i\%%i.csproj (
		dotnet add %configPath% reference %cd%\%%i\%%i.csproj
	)
)