# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApi.csproj", "WebApi/"]
RUN dotnet restore "WebApi/WebApi.csproj"
WORKDIR "/src/WebApi"
COPY . .
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

#wait for sql server
COPY wait-for-it.sh /wait-for-it.sh
RUN chmod +x /wait-for-it.sh

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["./wait-for-it.sh", "sqlserver", "--","dotnet", "WebApi.dll"]
