FROM microsoft/dotnet:2.1-sdk AS server
WORKDIR /src
COPY server/ .
RUN dotnet restore FuzzyMatch.Api/FuzzyMatch.Api.csproj
RUN dotnet publish FuzzyMatch.Api/FuzzyMatch.Api.csproj --output /build/ --configuration Release

FROM mhart/alpine-node AS client
WORKDIR /src
COPY client/ .
RUN npm install -g yarn
RUN yarn && yarn build

FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /opt
EXPOSE 80

COPY --from=server /build .
COPY --from=client /src/build /opt/wwwroot

ENTRYPOINT ["dotnet", "FuzzyMatch.Api.dll"]