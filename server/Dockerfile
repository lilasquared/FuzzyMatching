FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS publish

WORKDIR /src

COPY . .
RUN dotnet restore
RUN dotnet publish --output /app/ --configuration Release

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FuzzyMatch.Api.dll"]