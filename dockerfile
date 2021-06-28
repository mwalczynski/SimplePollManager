FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /work

COPY src/*/*.csproj ./
RUN for projectFile in $(ls *.csproj); \
do \
  mkdir -p ${projectFile%.*}/ && mv $projectFile ${projectFile%.*}/; \
done

RUN dotnet restore /work/SimplePollManager.Api/SimplePollManager.Api.csproj

COPY src .

FROM build AS publish
WORKDIR /work/SimplePollManager.Api
RUN dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
COPY --from=publish /app .

HEALTHCHECK --interval=5m --timeout=3s --start-period=10s --retries=1 \
  CMD curl --fail http://localhost:80/health || exit 1

ENTRYPOINT ["dotnet", "SimplePollManager.Api.dll"]
