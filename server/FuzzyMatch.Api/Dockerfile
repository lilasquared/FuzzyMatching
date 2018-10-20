FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["FuzzyMatch.Api/FuzzyMatch.Api.csproj", "FuzzyMatch.Api/"]
RUN dotnet restore "FuzzyMatch.Api/FuzzyMatch.Api.csproj"
COPY . .
WORKDIR "/src/FuzzyMatch.Api"
RUN dotnet build "FuzzyMatch.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FuzzyMatch.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FuzzyMatch.Api.dll"]