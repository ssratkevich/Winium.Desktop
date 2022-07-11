@echo off

setlocal EnableExtensions
setlocal EnableDelayedExpansion

set "Proj=Winium.Desktop.Driver"
set "Ver=2.1.1"
set "CurrentDir=%~dp0"
if "%CurrentDir:~-1%" == "\" (
    set "CurrentDir=%CurrentDir:~0,-1%"
)
pushd "%CurrentDir%"

set "BuildPath=%CurrentDir%\publish"

echo ^> Cleaning previous build
rd /S /Q "%BuildPath%">nul 2>&1

rem cd "Winium.Desktop.Driver"
rem dotnet publish -c Release -r win-x64 --sc true -o bin\publish\x64
rem dotnet publish -c Release -r win-x86 --sc true -o bin\publish\x86

echo ^> Building...
call :Publish "%Proj%" x64 "%BuildPath%"
call :Publish "%Proj%" x86 "%BuildPath%"

echo ^> Arichving...
call :Archive "%BuildPath%" "%BuildPath%\%Proj%.%Ver%"

echo ^> Done!
popd

pause

:Publish "proj" "arch" "path"
echo dotnet publish %1 -c Release -r "win-%2" --sc true -o "%3\%2"
dotnet publish %1 -c Release -r "win-%2" --sc true -o "%3\%2"
goto :EOF

:Archive "path" "name"
set "zipPath=%ProgramFiles%\7-Zip\7z.exe"
if not exist "%zipPath%" (
    if exist "%ProgramFiles(x86)%\7-Zip\7z.exe" (
        set "zipPath=%ProgramFiles(x86)%\7-Zip\7z.exe"
    ) else (
        echo 7-Zip not found!
        goto :EOF
    )
)

"%zipPath%" a -t7z -ad -slp -saa "%~2" "%~1\*">nul -mx=9
goto :EOF