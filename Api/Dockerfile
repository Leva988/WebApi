ARG REGISTRY=mcr.microsoft.com/
FROM ${REGISTRY}dotnet/aspnet:5.0.2 AS base
WORKDIR /app
EXPOSE 80

FROM node:latest as node-build
WORKDIR /web
COPY ClientApp/ .
RUN npm install && npm run build  --prod

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . .
ARG NUGET=https://api.nuget.org/v3/index.json
RUN dotnet restore  WebApi.csproj -s ${NUGET}
RUN dotnet build WebApi.csproj --no-restore -c Release -o /app
 
FROM build AS publish
RUN dotnet publish WebApi.csproj -c Release -o /app

# final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app ./
COPY --from=node-build /web/dist/UserApp /app/ClientApp/dist
ENTRYPOINT ["dotnet", "WebApi.dll"]