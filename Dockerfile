FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7889

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY test-push-notification-fcm.sln ./

COPY */*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done

RUN dotnet restore -v n
COPY . ./
RUN dotnet build -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "test-push-notification-fcm/test-push-notification-fcm.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://*:7889
ENTRYPOINT ["dotnet", "test-push-notification-fcm.dll"]