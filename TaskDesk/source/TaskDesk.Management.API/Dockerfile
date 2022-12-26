#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["source/TaskDesk.Management.API/TaskDesk.Management.API.csproj", "source/TaskDesk.Management.API/"]
COPY ["source/TaskDesk.Identity/TaskDesk.Identity.csproj", "source/TaskDesk.Identity/"]
COPY ["source/TaskDesk.Domain/TaskDesk.Domain.csproj", "source/TaskDesk.Domain/"]
COPY ["source/TaskDesk.Shared/TaskDesk.Shared.csproj", "source/TaskDesk.Shared/"]
COPY ["source/TaskDesk.Migrations/TaskDesk.Migrations.csproj", "source/TaskDesk.Migrations/"]
RUN dotnet restore "source/TaskDesk.Management.API/TaskDesk.Management.API.csproj"
COPY . .
WORKDIR "/src/source/TaskDesk.Management.API"
RUN dotnet build "TaskDesk.Management.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskDesk.Management.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskDesk.Management.API.dll"]