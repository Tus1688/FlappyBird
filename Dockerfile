FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /code

COPY superbird.csproj .

RUN dotnet restore

COPY . .

CMD ["dotnet", "run"]
