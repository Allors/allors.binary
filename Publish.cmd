@SET PATH=%PATH%;C:\Program Files\MSBuild\12.0\Bin;C:\Program Files (x86)\MSBuild\12.0\Bin;C:\Windows\Microsoft.NET\Framework\v4.0.30319

@Call Package.cmd

@if NOT ["%errorlevel%"]==["0"] (
    exit /b %errorlevel%
)

@.nuget\Nuget.exe Push publish\*.nupkg

@if NOT ["%errorlevel%"]==["0"] (
    exit /b %errorlevel%
)
