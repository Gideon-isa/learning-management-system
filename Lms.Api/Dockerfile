# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Lms.Api/Lms.Api.csproj", "Lms.Api/"]
COPY ["Lms.Assessment.Application/Lms.Assessment.Application.csproj", "Lms.Assessment.Application/"]
COPY ["Lms.Shared.Application/Lms.Shared.Application.csproj", "Lms.Shared.Application/"]
COPY ["Lms.Shared.Abstractions/Lms.Shared.Abstractions.csproj", "Lms.Shared.Abstractions/"]
COPY ["Lms.Shared.IntegrationEvents/Lms.Shared.IntegrationEvents.csproj", "Lms.Shared.IntegrationEvents/"]
COPY ["Lms.SharedKernel/Lms.SharedKernel.csproj", "Lms.SharedKernel/"]
COPY ["Lms.ContentDelivery.Application/Lms.ContentDelivery.Application.csproj", "Lms.ContentDelivery.Application/"]
COPY ["Lms.ContentDelivery.Domain/Lms.ContentDelivery.Domain.csproj", "Lms.ContentDelivery.Domain/"]
COPY ["Lms.ContentDelivery.Infrastructure/Lms.ContentDelivery.Infrastructure.csproj", "Lms.ContentDelivery.Infrastructure/"]
COPY ["Lms.CourseManagement.Application/Lms.CourseManagement.Application.csproj", "Lms.CourseManagement.Application/"]
COPY ["Lms.CourseManagement.Domain/Lms.CourseManagement.Domain.csproj", "Lms.CourseManagement.Domain/"]
COPY ["Lms.CourseManagement.Infrastructure/Lms.CourseManagement.Infrastructure.csproj", "Lms.CourseManagement.Infrastructure/"]
COPY ["Lms.Enrollment.Application/Lms.Enrollment.Application.csproj", "Lms.Enrollment.Application/"]
COPY ["Lms.Enrollment.Domain/Lms.Enrollment.Domain.csproj", "Lms.Enrollment.Domain/"]
COPY ["Lms.Enrollment.Infrastructure/Lms.Enrollment.Infrastructure.csproj", "Lms.Enrollment.Infrastructure/"]
COPY ["Lms.Identity.Application/Lms.Identity.Application.csproj", "Lms.Identity.Application/"]
COPY ["Lms.Identity.Infrastructure/Lms.Identity.Infrastructure.csproj", "Lms.Identity.Infrastructure/"]
RUN dotnet restore "./Lms.Api/Lms.Api.csproj"
COPY . .
WORKDIR "/src/Lms.Api"
RUN dotnet build "./Lms.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Lms.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lms.Api.dll"]