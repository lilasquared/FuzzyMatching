FROM microsoft/dotnet:2.1-sdk AS server
WORKDIR /src
COPY server/ .
RUN dotnet restore
RUN dotnet publish --output /build/ --configuration Release

FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /opt

COPY --from=server /build .

ENTRYPOINT ["dotnet", "FuzzyMatch.Worker.dll"]