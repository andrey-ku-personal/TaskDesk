FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY *.sln "./"
COPY ["source/TaskDesk.Google.Auth.API/TaskDesk.Google.Auth.API.csproj", "source/TaskDesk.Google.Auth.API/"]
COPY ["source/TaskDesk.Identity/TaskDesk.Identity.csproj", "source/TaskDesk.Identity/"]
COPY ["source/TaskDesk.Shared/TaskDesk.Shared.csproj", "source/TaskDesk.Shared/"]
COPY ["source/TaskDesk.Domain/TaskDesk.Domain.csproj", "source/TaskDesk.Domain/"]
COPY ["source/TaskDesk.Migrations/TaskDesk.Migrations.csproj", "source/TaskDesk.Migrations/"]
RUN dotnet restore "source/TaskDesk.Google.Auth.API/TaskDesk.Google.Auth.API.csproj"

COPY . .
WORKDIR "/src/source/TaskDesk.Google.Auth.API"
RUN dotnet publish "TaskDesk.Google.Auth.API.csproj" -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .

EXPOSE 80

ENTRYPOINT ["dotnet", "TaskDesk.Google.Auth.API.dll"]