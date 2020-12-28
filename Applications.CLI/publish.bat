@echo off

rem dotnet msbuild /nologo /t:Archive /p:Configuration=Release /p:RID=osx-x64 publish.msbuild
rem dotnet msbuild /nologo /t:Archive /p:Configuration=Release /p:RID=win-x64 publish.msbuild
dotnet msbuild /nologo /t:Archive /p:Configuration=Release publish.msbuild
