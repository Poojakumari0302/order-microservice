FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
ARG AspnetEnvironment
ENV ASPNETCORE_ENVIRONMENT=$AspnetEnvironment

WORKDIR /app
COPY /published .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Api.dll"]