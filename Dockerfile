FROM microsoft/dotnet:2.1-sdk AS server
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish --output /build/ --configuration Release

FROM mhart/alpine-node
WORKDIR /src
COPY . .
RUN npm install -g yarn
RUN cd client/ && yarn install && yarn build

FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /opt
EXPOSE 80

COPY --from=server /src/build .
COPY --from=client /src/client/build /opt/wwwroot

ENTRYPOINT ["dotnet", "FuzzyMatch.Api.dll"]