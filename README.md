# Comparis


What has been done:
 Simple API which has 2 endpoints:
  GET - To get processed payments by Id
  POST - run-payment: Process payments filling AccountFrom, AccountTo and Amount.
  
  How to run:
  In the solution folder using a console, run docker-compose command: 
  docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
  
  Containers created:
   - comparis.api -> Web API http://localhost:8020/swagger/index.html
   - comparisdb -> SQL Server -> Server=localhost;Database=ComparisDb;User Id=sa;Password=admin1234;
   - jagerservice -> Jaeger tracing tool: http://localhost:16686/
  
  Obs -> Jaeger is saving tracings only running API from Visual Studio, it is not saving by container to container, it is an issue that I will fix soon.
  

Technical details:

Layers:
 -= API =-
   -> Swagger
   -> BaseController with MessageManager Operation Validation
   -> Extensions:
      => GlobalExceptionHandlerMiddlewareExtensions - in order to register GlobalExceptionHandlerMiddleware
      => HostExtension - Run migration and initializa MSSQL Database in docker container
   -> GlobalExceptionHandlerMiddleware - Handle all unexpected exceptions, log it and return an error message by context.Response.
 
 -= APPLICATION =-
    Simple layer to orchestrate services, using MediatR and CQRS to segregate the command and query responsability.
   -> Behaviours - Has MediatR behaviors for Exception and Fluent Validation
   - UseCases -> Has Commands and Queries using Command/Query, Handler and Validator
   
 -= DOMAIN =-
   -> Entities
   -> Services - Domain Services for Business decision
   -> Interfaces -> Repository/Command, Queries and Domain Services
   
 -= PERSISTENCE =-
    Simple layer using EF CORE, Migration and Repository
   -> Commands using repository pattern
   -> Query -> To make it simple, it is using dbcontext to read data and the same database write and read.
   - Seed -> To add default data
 
 -= CROSSCUTTING =-
   - Notification -> MessageManager in this case responsible for carry just validation messages.
   
 -= BOOTSTRAP =-
   Responsible for configurations and services registration.
