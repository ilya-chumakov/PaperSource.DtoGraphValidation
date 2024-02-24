::start dotnet run -c Release -- --job short --runtimes net8.0 --filter *
start dotnet run -c Release -- --job short --runtimes net8.0 --filter *SizeDriven*
::start dotnet run -c Release -- --job short --runtimes net8.0 --filter *Validatable*

::start dotnet run -c Release --list tree