call "C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\Tools\VsDevCmd.bat" x86_amd64

echo off

msbuild GrosvnerMenu\GrosvnerMenu.sln /t:Rebuild /p:Configuration=Release

mstest /testcontainer:.\GrosvnerMenu\GrosvnerMenu.Test\bin\Release\GrosvnerMenu.Test.dll

mkdir build

xcopy /e /y GrosvnerMenu\GrosvnerMenu\bin\Release\*.* .\build

pause