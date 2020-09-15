FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY App/bin/Release/netcoreapp3.1/publish/ App/
COPY App/sample1.json App/data/
WORKDIR /App
ENTRYPOINT ["dotnet", "intersecting_rectangles.dll","./data/sample1.json"]
