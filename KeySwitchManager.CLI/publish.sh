#!/bin/bash

# dotnet msbuild /nologo /t:Archive /p:Configuration=Release /p:RID=osx-x64 /p:PublishDir=./publish publish.msbuild
# dotnet msbuild /nologo /t:Archive /p:Configuration=Release /p:RID=win-x64 /p:PublishDir=./publish publish.msbuild
dotnet msbuild /nologo /t:Archive /p:Configuration=Release /p:PublishDir=./publish publish.msbuild
