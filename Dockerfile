FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

ARG APP_ENVIRONMENT=
ENV ASPNETCORE_ENVIRONMENT=${APP_ENVIRONMENT}

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

RUN apt-get update && apt-get install -y curl

COPY "WebAppExamn/WebAppExamn.sln" "WebAppExamn/WebAppExamn.sln"
COPY "WebAppExamn/WebAppExamn.csproj" "WebAppExamn/WebAppExamn.csproj"
COPY "Infrastructure/Infrastructure.csproj" "Infrastructure/Infrastructure.csproj"
COPY "Domain/Domain.csproj" "Domain/Domain.csproj"
COPY "Application/Application.csproj" "Application/Application.csproj"

RUN dotnet restore "WebAppExamn/WebAppExamn.sln"
COPY . .
WORKDIR "/app/WebAppExamn"
RUN dotnet build "WebAppExamn.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAppExamn.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish/ .
ENTRYPOINT ["dotnet", "WebAppExamn.dll", "--urls", "http://*:5000"]