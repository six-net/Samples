@echo off
set "str1=eznewui"
set "str2=eznew"
Setlocal Enabledelayedexpansion
for /f "delims=" %%i in ('dir /b *.*') do (
set "var=%%i" & ren "%%i" "!var:%str1%=%str2%!")