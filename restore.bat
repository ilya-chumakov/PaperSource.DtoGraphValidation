rmdir /s /q ".vs"
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S obj') DO RMDIR /S /Q "%%G"
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S bin') DO RMDIR /S /Q "%%G"
dotnet clean
dotnet restore

echo "press any key to close the window"
PAUSE