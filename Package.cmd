@SET PATH=%PATH%;C:\Program Files\MSBuild\12.0\Bin;C:\Program Files (x86)\MSBuild\12.0\Bin;C:\Windows\Microsoft.NET\Framework\v4.0.30319

@msbuild Allors.Binary\Allors.Binary.csproj /TARGET:REBUILD /PROPERTY:Configuration=Release

@if NOT ["%errorlevel%"]==["0"] (
    exit /b %errorlevel%
)

@rmdir publish /s /q

@if NOT ["%errorlevel%"]==["0"] (
    exit /b %errorlevel%
)

@mkdir publish

@if NOT ["%errorlevel%"]==["0"] (
    exit /b %errorlevel%
)

@.nuget\Nuget.exe Pack Allors.Binary\Allors.Binary.csproj -Build -Symbols -Properties Configuration=Release -OutputDirectory publish

@if NOT ["%errorlevel%"]==["0"] (
    exit /b %errorlevel%
)