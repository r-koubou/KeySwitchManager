@echo off

dotnet msbuild /nologo /t:Archive /p:Configuration=Release publish.msbuild
dotnet msbuild /nologo /t:Archive /p:Configuration=Release /p:RID=osx-x64 publish.msbuild
dotnet msbuild /nologo /t:Archive /p:Configuration=Release /p:RID=win-x64 publish.msbuild
