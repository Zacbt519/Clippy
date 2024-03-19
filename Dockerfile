FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TT6 V.2.csproj", "./"]
RUN dotnet restore "TT6 V.2.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "TT6 V.2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TT6 V.2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TT6 V.2.dll"]
