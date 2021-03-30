FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base

WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/1 - Services/Api/Api.csproj", "src/1 - Services/Api/"]
COPY ["src/2 - Domain/Domain/Domain.csproj", "src/2 - Domain/Domain/"]
COPY ["src/3 - Infra/IoC/IoC.csproj", "src/3 - Infra/IoC/"]
COPY ["src/3 - Infra/Data/Data.csproj", "src/3 - Infra/Data/"]
RUN dotnet restore "src/1 - Services/Api/Api.csproj"

COPY . .
WORKDIR "/src/src/1 - Services/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS="http://localhost:5000"
ENTRYPOINT ["dotnet", "Api.dll"]