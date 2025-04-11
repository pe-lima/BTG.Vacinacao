FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["BTG.Vacinacao.sln", "."]
COPY ["src/BTG.Vacinacao.Application/BTG.Vacinacao.Application.csproj", "src/BTG.Vacinacao.Application/"]
COPY ["src/BTG.Vacinacao.Core/BTG.Vacinacao.Core.csproj", "src/BTG.Vacinacao.Core/"]
COPY ["src/BTG.Vacinacao.CrossCutting/BTG.Vacinacao.CrossCutting.csproj", "src/BTG.Vacinacao.CrossCutting/"]
COPY ["src/BTG.Vacinacao.Infra/BTG.Vacinacao.Infra.csproj", "src/BTG.Vacinacao.Infra/"]
COPY ["src/BTG.Vacinacao.IoC/BTG.Vacinacao.IoC.csproj", "src/BTG.Vacinacao.IoC/"]
COPY ["src/BTG.Vacinacao.Presentation/BTG.Vacinacao.Presentation.csproj", "src/BTG.Vacinacao.Presentation/"]

RUN dotnet restore

COPY . .
WORKDIR "/src/src/BTG.Vacinacao.Presentation"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BTG.Vacinacao.Presentation.dll"]