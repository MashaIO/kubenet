FROM mcr.microsoft.com/dotnet/core/runtime:3.0 AS runtime
EXPOSE 8081
WORKDIR /app
COPY deploy/. .
ENTRYPOINT ["dotnet", "KubeNet.Gen.dll"]