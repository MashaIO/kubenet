FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
EXPOSE 5000
WORKDIR /app
COPY deploy/. .
ENTRYPOINT ["dotnet", "KubeNet.Api.dll"]