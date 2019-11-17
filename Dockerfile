FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ["./Dashboard.csproj", "./"]
RUN dotnet restore "./Dashboard.csproj"

# copy everything else and build app
COPY ./ ./DEV_dashboard_2019/
WORKDIR /app/DEV_dashboard_2019
RUN dotnet publish "./Dashboard.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
EXPOSE 8080
WORKDIR /app
COPY --from=build /app/DEV_dashboard_2019/out ./
ENTRYPOINT ["dotnet", "Dashboard.dll"]