@Echo Packing files
"..\.nuget\nuget.exe" pack "FormVisitorGroups.nuspec"

@Echo Moving package
move /Y *.nupkg c:\project\nuget.local\