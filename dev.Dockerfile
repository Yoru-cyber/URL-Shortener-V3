FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files and restore dependencies
COPY ["URL-Shortener.csproj", "./"]
RUN dotnet restore "URL-Shortener.csproj"
# Copy the remaining project files
COPY . .

# Start the application in watch mode
CMD ["dotnet", "watch", "run", "--project" , "URL-Shortener.csproj", "--urls=http://0.0.0.0:8080"]