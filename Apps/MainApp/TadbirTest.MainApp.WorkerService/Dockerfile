#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Apps/MainApp/TadbirTest.MainApp.WorkerService/TadbirTest.MainApp.WorkerService.csproj", "Apps/MainApp/TadbirTest.MainApp.WorkerService/"]
RUN dotnet restore "Apps/MainApp/TadbirTest.MainApp.WorkerService/TadbirTest.MainApp.WorkerService.csproj"
COPY . .
WORKDIR "/src/Apps/MainApp/TadbirTest.MainApp.WorkerService"
RUN dotnet build "TadbirTest.MainApp.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TadbirTest.MainApp.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TadbirTest.MainApp.WorkerService.dll"]
