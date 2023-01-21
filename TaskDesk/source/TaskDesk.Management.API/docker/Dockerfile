FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["source/TaskDesk.Management.API/TaskDesk.Management.API.csproj", "source/TaskDesk.Management.API/"]
COPY ["source/TaskDesk.Management/TaskDesk.Management.csproj", "source/TaskDesk.Management/"]
COPY ["source/TaskDesk.Shared/TaskDesk.Shared.csproj", "source/TaskDesk.Shared/"]
COPY ["source/TaskDesk.SharedModel/TaskDesk.SharedModel.csproj", "source/TaskDesk.SharedModel/"]
COPY ["source/TaskDesk.Domain/TaskDesk.Domain.csproj", "source/TaskDesk.Domain/"]
COPY ["source/TaskDesk.Migrations/TaskDesk.Migrations.csproj", "source/TaskDesk.Migrations/"]
RUN dotnet restore "source/TaskDesk.Management.API/TaskDesk.Management.API.csproj"

COPY . .
WORKDIR "/src/source/TaskDesk.Management.API"
RUN dotnet publish "TaskDesk.Management.API.csproj" -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .

EXPOSE 80

ENTRYPOINT ["dotnet", "TaskDesk.Management.API.dll"]