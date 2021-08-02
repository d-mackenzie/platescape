del bin\Release\net5.0\osx-x64\*.* /S /Q
dotnet publish -c Release -r osx-x64 -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true -p:PublishTrimmed=true
ren bin\Release\net5.0\osx-x64\publish\Chantry Chantry.app
xcopy bin\Release\net5.0\osx-x64\publish\ ..\publish\ /Y