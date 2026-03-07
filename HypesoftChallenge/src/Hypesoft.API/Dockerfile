FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia apenas os projetos para restaurar as dependências
COPY ["src/Hypesoft.API/Hypesoft.API.csproj", "Hypesoft.API/"]
COPY ["src/Hypesoft.Application/Hypesoft.Application.csproj", "Hypesoft.Application/"]
COPY ["src/Hypesoft.Domain/Hypesoft.Domain.csproj", "Hypesoft.Domain/"]
COPY ["src/Hypesoft.Infrastructure/Hypesoft.Infrastructure.csproj", "Hypesoft.Infrastructure/"]

RUN dotnet restore "Hypesoft.API/Hypesoft.API.csproj"

COPY src ./ 

WORKDIR "/src/Hypesoft.API"

RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "Hypesoft.API.dll"]