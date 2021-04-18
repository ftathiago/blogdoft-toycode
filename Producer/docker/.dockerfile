FROM mcr.microsoft.com/dotnet/nightly/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/nightly/sdk:6.0-alpine AS restore
WORKDIR /src
COPY "Producer.sln" "Producer.sln"
COPY "__tests__/Producer.Api.Tests/Producer.Api.Tests.csproj" "__tests__/Producer.Api.Tests/Producer.Api.Tests.csproj"
COPY "__tests__/Producer.Business.Tests/Producer.Business.Tests.csproj" "__tests__/Producer.Business.Tests/Producer.Business.Tests.csproj"
COPY "__tests__/Producer.InfraData.Tests/Producer.InfraData.Tests.csproj" "__tests__/Producer.InfraData.Tests/Producer.InfraData.Tests.csproj"
COPY "__tests__/Producer.InfraKafka.Tests/Producer.InfraKafka.Tests.csproj" "__tests__/Producer.InfraKafka.Tests/Producer.InfraKafka.Tests.csproj"
COPY "__tests__/Producer.Shared.Tests/Producer.Shared.Tests.csproj" "__tests__/Producer.Shared.Tests/Producer.Shared.Tests.csproj"
COPY "src/Producer.Api/Producer.Api.csproj" "src/Producer.Api/Producer.Api.csproj"
COPY "src/Producer.Business/Producer.Business.csproj" "src/Producer.Business/Producer.Business.csproj"
COPY "src/Producer.InfraData/Producer.InfraData.csproj" "src/Producer.InfraData/Producer.InfraData.csproj"
COPY "src/Producer.InfraKafka/Producer.InfraKafka.csproj" "src/Producer.InfraKafka/Producer.InfraKafka.csproj"
COPY "src/Producer.IoC/Producer.IoC.csproj" "src/Producer.IoC/Producer.IoC.csproj"
COPY "src/Producer.Shared/Producer.Shared.csproj" "src/Producer.Shared/Producer.Shared.csproj"
COPY "src/Producer.WarmUp/Producer.WarmUp.csproj" "src/Producer.WarmUp/Producer.WarmUp.csproj"
RUN dotnet restore

FROM restore AS build
WORKDIR /src
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Producer.Api.dll"]