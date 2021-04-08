@echo off

dotnet msbuild /nologo /t:Archive /p:Configuration=Release /p:PublishDir=.\publish publish.msbuild
