# Krok 1: Budowanie aplikacji
# Użyj SDK do kompilacji
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src

# Kopiuj pliki projektu aplikacji konsolowej
# Zakładamy, że pliki .csproj aplikacji konsolowej są w podkatalogu MyConsoleApp
COPY CatalogOfVegetablesFruitsHerb/VFHCatalog.csproj ./VFHCatalog/
# Przywróć zależności dla projektu konsolowego
RUN dotnet restore ./VFHCatalog/

# Kopiuj resztę plików projektu
COPY . ./

# Opublikuj aplikację konsolową w trybie Release
# Zastąp "MyConsoleApp" nazwą swojego projektu konsolowego
RUN dotnet publish ./CatalogOfVegetablesFruitsHerb/VFHCatalog.csproj -c Release -o /app/publish

# Krok 2: Obraz środowiska uruchomieniowego
# Użyj środowiska uruchomieniowego .NET (runtime) dla aplikacji konsolowej
FROM mcr.microsoft.com/dotnet/runtime:3.1 AS final
WORKDIR /app

# Kopiuj opublikowaną aplikację
COPY --from=build /app/publish .

# Uruchom aplikację konsolową
# Zastąp "MyConsoleApp.dll" nazwą skompilowanej biblioteki DLL Twojej aplikacji konsolowej
ENTRYPOINT ["dotnet", "VFHCatalog.dll"]
