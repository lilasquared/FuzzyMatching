FROM microsoft/dotnet:2.1-sdk AS builder

WORKDIR /src

COPY . .
RUN dotnet restore
RUN dotnet publish --output /app/ --configuration Release
RUN npm install -g yarn
RUN cd client/ && yarn install && yarn build

FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /opt
EXPOSE 80

COPY --from=builder /app .
COPY --from=builder /src/client/build /opt/wwwroot

ENTRYPOINT ["dotnet", "FuzzyMatch.Api.dll"]