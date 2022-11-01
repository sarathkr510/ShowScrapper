# TvMaze Shows Scapper

TvMaze Show Scrapper is an application that ingests Shows and Cast Information in to internal database. 

pThis application also exposes an API that provides cast of the tv shows in TvMaze Database, which can be enriched further in other systems. 

## Architecture Patterns and Design Patterns

* Clean Architecture
* Separation of Concerns
* SOLID Principles
* DDD (Domain-Driven Design)
* CQRS design pattern
* Repository design pattern


## Technologies
* [C# 10](https://docs.microsoft.com/en-us/dotnet/csharp)
* [.NET 6](https://dotnet.microsoft.com/download)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core)
* [MediatR](https://github.com/jbogard/MediatR)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq)
* [Polly](https://github.com/App-vNext/Polly)
* [Swagger](https://swagger.io/)
* [ErrorOr](https://github.com/amantinband/error-or)
* [Docker Desktop with Kubernetes Enabled](https://www.docker.com/products/docker-desktop/)


## Layers

**API:** Frontend (Presentation)

**Application:** This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.


**Domain:** This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web Apis, smtp, and so on. These classes should be based on interfaces.

### Configuration



Verify that the **AppSettings.json** has all the below mentioned settings.
```json
"ConnectionStrings": {
    "TvMazeDbContext": "DataSource=tvmaze.db"
  },
  "CacheProfiles": {
    "Cache2Mins": {
      "Duration": 120,
      "Location": "Any"
    }
  },
  "PaginationSettings": {
    "DefaultPage": 1,
    "PageSize": 10
  },
  "ScraperSettings": {
    "ApiBaseUrl": "https://api.tvmaze.com",
    "ShowsRoute": "shows?page={page}",
    "CastRoute": "shows/{id}/cast",
    "PageSize": 25
  },
  "RateLimitSettings": {
    "CallsPerSecond": 2
  }
```


## Business Requirement

* Scrapes the TVMAZE API for the show and cast information.
* Persists the data in storage.
* Provides the scraped data using a REST API.

## Rules for the Exposed Restful Web API
* It should provide a paginated list of all tv shows containing the id of the TV show and a list of all the cast that are playing in that TV Show.
* The list of the cast must be ordered by birthday descending.

## Challenges
* TvMaze API is rate limited, so I have used Polly and created custom policy provider for building the http requests and for resiliency.

Here it goes:
```csharp
  var bulkheadPolicy = Policy.BulkheadAsync<HttpResponseMessage>(10, Int32.MaxValue);
  var timeoutPolicy = Policy.TimeoutAsync(TimeSpan.FromSeconds(180));
  var policy = Policy
              .Handle<HttpRequestException>()
              .OrResult<HttpResponseMessage>
              (r => r.StatusCode ==  HttpStatusCode.TooManyRequests)
              .WaitAndRetryAsync(5, retryAttempt =>
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
              .WrapAsync(bulkheadPolicy);

  return timeoutPolicy.WrapAsync(policy);
