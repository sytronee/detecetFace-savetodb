# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# DetectionEventAPI ve CommonModels'in olduğu seviyede build yapıyoruz
COPY . .
RUN dotnet publish "DetectionEventAPI/DetectionEventAPI.csproj" -c Release -o /app/publish

# Final aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DetectionEventAPI.dll"]