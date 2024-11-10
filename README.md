# WorkdayCalendar

## Used Technologies/Tools
- IDE: Visual Studio 2022 version 17.0
- Platform: .NET Core 8.0
- Programming Language: C#
- Database: Microsoft EntityFrameworkCore Sqlite
- ORM: Microsoft EntityFrameworkCore
- Unit Test: MSTest, NSubstitute (For mocking)
- Object Mapping: AutoMapper

## Architecture

The project uses a Domain-Driven Design (DDD) architecture that is divided into several layers.

**1. WorkdayCalendar.DomainLayer**
   
      Purpose: Represents the core business logic and rules of workday calendar application.
   
      Components: Includes Entities, Repository/Service Intefaces, Services and Exceptions.

      Dependencies(NuGet packages):
        - Microsoft.Extensions.Options (8.0.2)

**2. WorkdayCalendar.ServiceLayer**
   
       Purpose: Manages application tasks and coordinates operations between the WorkdayCalendar.DomainLayer and WorkdayCalendar.API (Presentation Layer).

       Components: Interfaces, Services, Middlewares and Startup (Registers services used in the application in the Dependency Injection container).

       Dependencies(NuGet packages):
         - Microsoft.AspNetCore.Http (2.2.2)
         - Microsoft.Extensions.DependencyInjection (8.0.1)

       Dependencies(Projects):
          - WorkdayCalendar.DomainLayer
          - WorkdayCalendar.Infrastructure

**3. WorkdayCalendar.InfrastructureLayer**
   
      Purpose: Manages operations with the data storage.
   
      Components: Repositories, Migrations.
   
      Description: This layer manages implementations related to the data storage as defined in repository interfaces of the WorkdayCalendar.DomainLayer.

       Dependencies(NuGet packages):
         - Microsoft.EntityFrameworkCore.Sqlite (8.0.10)
         - Microsoft.EntityFrameworkCore.Tools (8.0.10)

**4. WorkdayCalendar.API (Presentation Layer)**

       Purpose: Manages user interactions and presentation logic communicating with the WorkdayCalendar.ServiceLayer.

       Components: Controllers, Dtos.

       Dependencies(NuGet packages):
         - AutoMapper (13.0.1)
         - Swashbuckle.AspNetCore (6.4.0)
         - Swashbuckle.AspNetCore.Annotations (6.9.0)

       Dependencies(Projects):
          - WorkdayCalendar.ServiceLayer

## Unit Tests

<img width="339" alt="image" src="https://github.com/user-attachments/assets/475970dd-1280-4ba4-abce-7a2e4282f6a8">

  Dependencies(NuGet packages):

  - MSTest.TestFramework (3.0.4)
  - MSTest.TestAdapter (3.0.4)
  - Microsoft.NET.Test.Sdk (17.6.0)
  - coverlet.collector (6.0.0)
  - NSubstitute (5.3.0)
  - AutoMapper (13.0.1)

**1. WorkdayCalendar.DomainLayer.Tests**
      
      Handles unit test coverage of the WorkdayCalendar.DomainLayer.

      Dependencies(Projects):
      - WorkdayCalendar.DomainLayer
      
**2. WorkdayCalendar.ServiceLayer.Tests**

      Handles unit test coverage of the WorkdayCalendar.ServiceLayer.

      Dependencies(Projects):
      - WorkdayCalendar.ServiceLayer

**3. WorkdayCalendar.InfrastructureLayer.Tests**

      Handles unit test coverage of the WorkdayCalendar.InfrastructureLayer.

      Dependencies(Projects):
      - WorkdayCalendar.InfrastructureLayer

**4. WorkdayCalendar.API.Tests**

      Handles unit test coverage of the WorkdayCalendar.API.

      Dependencies(Projects):
      - WorkdayCalendar.API

   ## How to run unit tests
      - Click on the "Test" menu in Visual Studio.
      - Select "Run All Tests".
