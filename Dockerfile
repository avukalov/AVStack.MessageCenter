

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
COPY ./Templates /Templates

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ./LocalNugets /tmp/LocalNugets

COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done

RUN dotnet restore "AVStack.MessageCenter.sln" -s https://api.nuget.org/v3/index.json -s /tmp/LocalNugets

COPY . .

WORKDIR "/src"
RUN dotnet build "AVStack.MessageCenter.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AVStack.MessageCenter.sln" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AVStack.MessageCenter.dll"]
