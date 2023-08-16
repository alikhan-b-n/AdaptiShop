FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AdaptiShop/AdaptiShop.csproj", "AdaptiShop/"]
RUN dotnet restore "AdaptiShop/AdaptiShop.csproj"
COPY . .
WORKDIR "/src/AdaptiShop"
RUN dotnet build "AdaptiShop.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdaptiShop.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdaptiShop.dll"]
