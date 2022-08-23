FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://*:80
ENV ASPNETCORE_ENVIRONMENT=Development
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Test.csproj", "./"]
RUN dotnet restore "Test.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Test.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Test.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Test.dll","--environment=Development"]
