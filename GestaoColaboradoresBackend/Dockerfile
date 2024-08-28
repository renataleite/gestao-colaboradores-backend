# Estágio 1: Compilar e Publicar a Aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar o arquivo de projeto e restaurar as dependências
COPY ["GestaoColaboradoresBackend.csproj", "./"]
RUN dotnet restore "./GestaoColaboradoresBackend.csproj"

# Copiar todo o código fonte e compilar a aplicação
COPY . .
RUN dotnet build "GestaoColaboradoresBackend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicar a aplicação
FROM build AS publish
RUN dotnet publish "GestaoColaboradoresBackend.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Estágio 2: Configuração da Imagem Final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copiar os arquivos publicados para a imagem final
COPY --from=publish /app/publish .

# Configurar as portas expostas para HTTP
EXPOSE 8080

# Configurar a aplicação para usar apenas HTTP no ambiente de desenvolvimento
ENV ASPNETCORE_URLS="http://+:8080"
ENV ASPNETCORE_ENVIRONMENT="Development"

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "GestaoColaboradoresBackend.dll"]
