# 👇 MUDOU AQUI DE 8.0 PARA 10.0!
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .

# CAMINHO DUPLO AQUI TAMBÉM PARA ACHAR O PROJETO!
RUN dotnet publish "ComunicacaoVisual.Client/ComunicacaoVisual.Client/ComunicacaoVisual.Client.csproj" -c Release -o /app/publish

# 2. Pega o Nginx para rodar a tela no navegador
FROM nginx:alpine
WORKDIR /usr/share/nginx/html
# Copia os arquivos compilados do passo 1 para a pasta do Nginx
COPY --from=build /app/publish/wwwroot .
# Expõe a porta 80
EXPOSE 80