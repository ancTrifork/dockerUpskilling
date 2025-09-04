#Stage 1: Construction site. We build the project in a huge container with all the dependencies.
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["MyWebApi/MyWebApi.csproj", "MyWebApi/"]
RUN dotnet restore "MyWebApi/MyWebApi.csproj"
COPY . .
WORKDIR "/src/MyWebApi"
RUN dotnet build "MyWebApi.csproj" -c Release -o /app/build

#Stage 2: Publish the application. We prepare the application for deployment.
FROM build AS publish
#runs the dotnet publish command with release configuration optimized and no debug info, then outputs the published files to app/publish
RUN dotnet publish "MyWebApi.csproj" -c Release -o /app/publish

#Stage 3: Runtime image creation
# Create completely new container. aspnet is runtime only, without all development tools found in sdk
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
#Copies the published files from the publish stage to the current working directory in the final stage
COPY --from=publish /app/publish .
#Documents that the docker container will listen on port 8080. Doesn't open the port, just tells Docker: "Hey, this app uses port 8080".
EXPOSE 8080
# When Docker starts this container, it will run the following command: "dotnet MyWebApi.dll"
ENTRYPOINT ["dotnet", "MyWebApi.dll"]
