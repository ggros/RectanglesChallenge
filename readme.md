# Rectangles intersections challenge

## introduction
project created with dot net core 3.1 (`dotnet new console`)  
To quickly test using docker and image on docker hub  
    `docker run -it --rm ggros/intersecting-rectangles-image`

## install/prerequisites for coding
go to https://dotnet.microsoft.com/download/dotnet-core/3.1 to install dotnet.
to edit the code, you can use vscode or any IDE that supports C# and dot net core.

for ubuntu 18.04
https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu#1804-

## run or publish
- to run the project `dotnet run --project .\App sample1.json`
- to publish the project `dotnet publish` to produce an .exe or linux ELF binary

## run the unit tests
`dotnet test .\Tests\RectangleIntersectionsTest.csproj`

## docker: Build a new image
publish the app running `dotnet publish -c Release`

Build the image `docker build -t intersecting-rectangles-image -f Dockerfile .`

## docker: push the new image to a registry (e.g. docker hub)
`docker tag intersecting-rectangles-image ggros/intersecting-rectangles-image`

`docker push ggros/intersecting-rectangles-image`

# docker run
You can run the image interactively against sample1.json
- `docker run -it --rm ggros/intersecting-rectangles-image`

If you want to test another file, run a bash, copy files and test them.

- step 1: run  
    `docker run -it --rm --name rectangles-test --entrypoint bash ggros/intersecting-rectangles-image`

- step 2: copy a json from your local system  
    `docker cp .\App\sample2.json rectangles-test:/App/data`

- step 3: now in the container's bash shell execute  
    `dotnet intersecting_rectangles.dll data/sample2.json`
