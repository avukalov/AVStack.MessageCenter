FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AVStack.MessageCenter.WebApi/AVStack.MessageCenter.WebApi.csproj", "AVStack.MessageCenter.WebApi/"]
RUN dotnet restore "AVStack.MessageCenter.WebApi/AVStack.MessageCenter.WebApi.csproj"
COPY . .
WORKDIR "/src/AVStack.MessageCenter.WebApi"
RUN dotnet build "AVStack.MessageCenter.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AVStack.MessageCenter.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AVStack.MessageCenter.WebApi.dll"]
