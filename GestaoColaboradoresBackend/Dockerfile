# Est�gio 1: Compilar e Publicar a Aplica��o
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar o arquivo de projeto e restaurar as depend�ncias
COPY ["GestaoColaboradoresBackend.csproj", "./"]
RUN dotnet restore "./GestaoColaboradoresBackend.csproj"

# Copiar todo o c�digo fonte e compilar a aplica��o
COPY . .
RUN dotnet build "GestaoColaboradoresBackend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicar a aplica��o
FROM build AS publish
RUN dotnet publish "GestaoColaboradoresBackend.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Est�gio 2: Configura��o da Imagem Final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copiar os arquivos publicados para a imagem final
COPY --from=publish /app/publish .

# Configurar as portas expostas para HTTP
EXPOSE 8080

# Configurar a aplica��o para usar apenas HTTP no ambiente de desenvolvimento
ENV ASPNETCORE_URLS="http://+:8080"
ENV ASPNETCORE_ENVIRONMENT="Development"

# Comando para iniciar a aplica��o
ENTRYPOINT ["dotnet", "GestaoColaboradoresBackend.dll"]
