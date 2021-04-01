FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY ["src/1 - Services/Api/Api.csproj", "src/1 - Services/Api/"]
COPY ["src/2 - Domain/Domain/Domain.csproj", "src/2 - Domain/Domain/"]
COPY ["src/3 - Infra/IoC/IoC.csproj", "src/3 - Infra/IoC/"]
COPY ["src/3 - Infra/Data/Data.csproj", "src/3 - Infra/Data/"]
RUN dotnet restore "src/1 - Services/Api/Api.csproj"

COPY . ./
RUN dotnet publish -c Release -o dist

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/dist .
ENTRYPOINT ["dotnet", "Api.dll"]