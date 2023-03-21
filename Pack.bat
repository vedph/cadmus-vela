@echo off
echo BUILD Cadmus Vela Packages
del .\Cadmus.Vela.Parts\bin\Debug\*.snupkg
del .\Cadmus.Vela.Parts\bin\Debug\*.nupkg

del .\Cadmus.Seed.Vela.Parts\bin\Debug\*.snupkg
del .\Cadmus.Seed.Vela.Parts\bin\Debug\*.nupkg

del .\Cadmus.Vela.Services\bin\Debug\*.snupkg
del .\Cadmus.Vela.Services\bin\Debug\*.nupkg

cd .\Cadmus.Vela.Parts
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Seed.Vela.Parts
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Vela.Services
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

pause
