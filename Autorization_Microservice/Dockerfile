#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Autorization_Microservice/Autorization_Microservice.csproj", "Autorization_Microservice/"]

COPY ["Domain/Domain.Entities/Domain.Entities.csproj", "Domain/Domain.Entities/"]

COPY ["Infrastructure/Infrastructure.EntityFramework/Infrastructure.EntityFramework.csproj", "Infrastructure/Infrastructure.EntityFramework/"]
COPY ["Infrastructure/Infrastructure.Repositories.Implementations/Infrastructure.Repositories.Implementations.csproj", "Infrastructure/Infrastructure.Repositories.Implementations/"]

COPY ["Services/Services.Abstractions/Services.Abstractions.csproj", "Services/Services.Abstractions/"]
COPY ["Services/Services.Contracts/Services.Contracts.csproj", "Services/Services.Contracts/"]
COPY ["Services/Services.Implementations/Services.Implementations.csproj", "Services/Services.Implementations/"]
COPY ["Services/Services.Repositories.Abstractions/Services.Repositories.Abstractions.csproj", "Services/Services.Repositories.Abstractions/"]

COPY ["Test_Microservice/Test_Microservice.csproj", "Test_Microservice/"]

COPY ["WebClient/WebClient.csproj", "WebClient/"]

RUN dotnet restore "Autorization_Microservice/Autorization_Microservice.csproj"

COPY . .
WORKDIR "/src/Autorization_Microservice"
RUN dotnet build "Autorization_Microservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Autorization_Microservice.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Autorization_Microservice.dll"]

# docker build -t auth_img -f Autorization_Microservice/Dockerfile .
# docker build -f ".\Autorization_Microservice\Dockerfile" --force-rm -t my_authorizationmcs  --label "com.microsoft.created-by=Eig" --label "com.microsoft.visual-studio.project-name=AuthorizationMcs" .


# docker run -it --rm -p 80:54205 -p 443:54206 -e ASPNETCORE_HTTPS_PORT=https://+:54206 -e ASPNETCORE_URLS=http://+:54205 --name author_container auth_img
# docker run -t -d -p 54205:80 --name authorizemcs_container my_authorizationmcs


#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish
#WORKDIR /src
#COPY . .
#
#RUN dotnet restore /ignoreprojectextensions:.dcproj
#
#WORKDIR /src/Autorization_Microservice
#RUN dotnet publish "Autorization_Microservice.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Autorization_Microservice.dll"]