@echo off

setlocal EnableDelayedExpansion

for %%i in (*.yaml) do (
    set LISTFILES=!LISTFILES! "%%i"
)

call ..\simple_codegen.bat !LISTFILES!

endlocal
