FROM node:10 as base
RUN npm install -g http-server
WORKDIR /opt
EXPOSE 8080

FROM node:10 as build
WORKDIR /src
COPY . .

RUN npm install -g yarn
RUN yarn install && yarn build

FROM base
COPY --from=build /src/build .

ENTRYPOINT [ "http-server", "." ]
