FROM mcr.microsoft.com/dotnet/sdk AS publish
WORKDIR /src

COPY . .
RUN dotnet publish "src/nomeprojetoAPI" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/sdk

WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5000
EXPOSE 5001
ENTRYPOINT ["dotnet", "nomeprojetoAPI.dll"]