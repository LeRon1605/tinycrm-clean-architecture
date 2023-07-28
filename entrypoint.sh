#!/bin/bash

# Create network
docker network create tiny-crm

# Create volume to store db data
docker volume create tiny-crm

# Run Sql Server
docker run --user root \
           -e "ACCEPT_EULA=Y" \
           -e "MSSQL_SA_PASSWORD=Leron@1605" \
           -e "MSSQL_PID=Express" \
           -v tiny-crm:/var/opt/mssql/data \
           -p 14433:1433 \
           -d \
           --network tiny-crm --network-alias db \
           mcr.microsoft.com/mssql/server:2019-latest

# Build api image
docker build -t tiny_crm -f Lab2.API/Dockerfile .

# Migrate db
cd Lab2.API
dotnet ef database update -- --environment Docker

# Run api 
docker run -e "ConnectionStrings__Default=Server=db;Database=TinyCRM;UID=sa;PWD=Leron@1605;TrustServerCertificate=True" \
           -e "ASPNETCORE_ENVIRONMENT=Development" \
           -p 5000:443 \
           -p 5001:80 \
           --network tiny-crm --network-alias api \
           tiny_crm