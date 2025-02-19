@echo off
echo PRESS ANY KEY TO INSTALL TO LOCAL NUGET FEED
c:\exe\nuget add .\Cadmus.Vela.Parts\bin\Debug\Cadmus.Vela.Parts.3.0.4.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Seed.Vela.Parts\bin\Debug\Cadmus.Seed.Vela.Parts.3.0.4.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Vela.Services\bin\Debug\Cadmus.Vela.Services.3.0.4.nupkg -source C:\Projects\_NuGet
pause
