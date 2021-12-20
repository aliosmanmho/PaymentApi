#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Application/Payment.Api/Payment.Api.csproj", "src/Application/Payment.Api/"]
COPY ["src/Db/Payment.Data/Payment.Data.csproj", "src/Db/Payment.Data/"]
COPY ["src/Core/Payment.Core/Payment.Core.csproj", "src/Core/Payment.Core/"]
COPY ["src/Business/Payment.Bussinies/Payment.Bussinies.csproj", "src/Business/Payment.Bussinies/"]
COPY ["src/Business/Payment.Bussinies.StaticData/Payment.Bussinies.StaticData.csproj", "src/Business/Payment.Bussinies.StaticData/"]
COPY ["src/Providers/Payment.Providers/Payment.Providers.csproj", "src/Providers/Payment.Providers/"]
RUN dotnet restore "src/Application/Payment.Api/Payment.Api.csproj"
COPY . .
WORKDIR "/src/src/Application/Payment.Api"
RUN dotnet build "Payment.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Payment.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Payment.Api.dll
#ENTRYPOINT ["dotnet", "Payment.Api.dll"]