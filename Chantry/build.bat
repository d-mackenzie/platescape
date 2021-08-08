@echo off

del bin\Release\net5.0\osx-x64\*.* /S /Q
dotnet publish -c Release -r osx-x64 -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true -p:PublishTrimmed=true
copy bin\Release\net5.0\osx-x64\publish\Chantry ..\publish\Chantry.app /Y

del bin\Release\net5.0\win-x64\*.* /S /Q
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true -p:PublishTrimmed=true
copy bin\Release\net5.0\win-x64\publish\Chantry.exe ..\publish\Chantry.exe /Y
