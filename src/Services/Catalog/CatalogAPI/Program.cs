var builder = WebApplication.CreateBuilder(args);

//added services to the container



var app = builder.Build();

//configure the HTTP request pipeline


app.MapGet("/", () => "Hello World!");

app.Run();
/*
Analysis(Domain+ technical-patterns and principals)
Dev + test
deploy (containers + orchestrate)

Domain analysis
design domain models, applications use cases (like flow),   Rest API Endpoints , Underlpying Data Structures( Q-NoSQL, W(PostGre) ) + marten(DocumentDB)
table catalog - json doc
What is Marten?
Marten is a data access library for .NET applications that simplifies working with PostgreSQL databases. It acts as a bridge between your application and the database, allowing you to store, query, and manipulate both relational data and JSON-based documents in PostgreSQL.

By combining the capabilities of a document database and a relational database, Marten enables developers to use PostgreSQL as a hybrid database, catering to various modern application requirements.

Key Features of Marten
Document Database Support:

Store and query JSON documents directly in PostgreSQL.
Avoid the need for a separate NoSQL database like MongoDB.
Ideal for event sourcing, schema-less scenarios, or microservices.
Event Sourcing:

Out-of-the-box support for event-sourced systems.
Store events in PostgreSQL with efficient retrieval and projection mechanisms.
Transaction Management:

Provides Unit of Work (UoW) pattern for managing transactions across commands and queries.
Strongly-Typed Models:

Store and retrieve documents as strongly-typed .NET classes, improving code safety and readability.
LINQ Query Support:

Use LINQ to query documents, making queries expressive and type-safe.
PostgreSQL Integration:

Leverages PostgreSQL’s strengths, such as high performance, transactional consistency, and SQL features.
Multi-Tenancy:

Supports multiple tenants with isolated storage within a single database instance.
Simple Setup:

Built with simplicity in mind, making it easy to configure and start using.
Why Use Marten?
Simplifies Data Access:

Allows using PostgreSQL as a flexible document store and a relational database, reducing complexity.
Reduces Infrastructure Needs:

Eliminates the need for a separate NoSQL database, consolidating your database technology stack.
Supports Modern Architectures:

Perfect for applications using microservices, CQRS, or event sourcing.
Performance and Scalability:

Leverages PostgreSQL's performance capabilities while simplifying development with JSON storage.


Disadvantages of Marten
Tied to PostgreSQL:

Only works with PostgreSQL, limiting database choice.
Learning Curve:

Developers unfamiliar with document databases or event sourcing may need time to adapt.
Limited Community:

Smaller community compared to larger ORMs like Entity Framework.



Use Cases for Marten
Event Sourcing:

Systems that require capturing and replaying events, such as financial systems or audit logs.
Microservices:

Storing data for domain-driven microservices with flexible schemas.
Hybrid Workloads:

Applications needing both relational (SQL) and document-based (NoSQL) storage.
Catalog Management:

Managing e-commerce product catalogs with diverse attributes stored as JSON.







Technical Anylysis
//1. Application Architecture Style
//2. Patterns + Principles
//3. Nuget Packages
//4. Project Folder Structure

//Marten - DocumentDB
//Carter - API Endpoints
//Mapstar - Object Mapping
//MediatR - CQRS pattern
//FluentValidation - Input Validation


//CQRS pattern
//Mediator pattern
//DI
//Minimal APIs with Routing









Flow Explanation: Building a Modern Microservice Application
This flow provides a structured process for designing, developing, and deploying a scalable microservice application, incorporating both domain analysis and technical patterns to ensure best practices.

1. Analysis
a. Domain Analysis
This phase involves understanding the business needs and translating them into a technical solution.

Domain Models:

Identify the key entities relevant to the application domain.
Example: In an e-commerce system:
Entities: Product, Order, Customer, Cart.
Models:

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
Application Use Cases:

Define user or system actions the application should support.
Examples:
View product catalog.
Place an order.
Retrieve order history.
REST API Endpoints:

Map use cases to HTTP endpoints.
Example:
GET /products - Retrieve product list.
POST /orders - Create a new order.
GET /orders/{id} - Fetch order details.
Underlying Data Structures:

Select suitable databases for persistence.
Relational: Use PostgreSQL for structured, transactional data.
NoSQL: Use Marten for flexible JSON document storage.
Example Data Structure:
json

{
  "OrderId": "1234",
  "Customer": "John Doe",
  "Items": [
    { "ProductId": "5678", "Quantity": 2 }
  ],
  "Total": 199.98
}
b. Technical Analysis
Translate domain insights into architectural and implementation strategies.

Application Architecture Style:

Opt for microservices for modularity, scalability, and isolation.
Utilize Minimal APIs for lightweight services with low overhead.
Patterns and Principles:

CQRS: Separate query and command operations for scalability.
Mediator Pattern: Use MediatR to decouple request handling and business logic.
Dependency Injection (DI): Integrate ASP.NET Core's built-in DI for better testability and modular design.
NuGet Packages:

Marten: DocumentDB support using PostgreSQL.
Carter: Simplified endpoint routing and grouping.
Mapster: Lightweight object mapping for transforming DTOs to domain models.
FluentValidation: Streamlined input validation.
Project Folder Structure:

Modularize the application for maintainability:
css

src/
  Application/
    UseCases/
    Interfaces/
    Validators/
  Domain/
    Models/
  Infrastructure/
    Persistence/
    Repositories/
  WebAPI/
    Program.cs
    Endpoints/
2. Development + Testing
Development Steps:
Set up Minimal APIs in ASP.NET Core for REST endpoints.
Implement domain logic using CQRS with MediatR for clear separation of responsibilities.
Integrate Marten for data persistence, enabling JSON-based document storage.
Use FluentValidation for input validation to enforce rules at the API boundary.
Testing:
Unit Tests:
Test individual use cases and business logic.
Mock dependencies for isolated testing.
Integration Tests:
Validate the entire flow of API interactions with a database.
Use an in-memory database for quick tests.
Contract Testing:
Ensure APIs adhere to client contracts using tools like Pact.
3. Deployment
a. Containers:
Package the application into Docker containers for isolated and consistent deployment environments.
Example Dockerfile:
dockerfile

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "WebAPI.dll"]
b. Orchestration:
Use Kubernetes for managing containerized applications:
Scale microservices independently.
Monitor and manage service health.
c. CI/CD Pipeline:
Automate builds, tests, and deployments using tools like Azure DevOps or GitHub Actions.
Deploy to cloud platforms like Azure, AWS, or GCP.
4. Patterns and Tools in Action
CQRS Pattern:
Separation of commands and queries enables scalability and clear logic.
Example:

public record GetProductQuery(Guid Id) : IRequest<Product>;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
{
    public Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        // Logic to fetch the product from the database
    }
}
Mediator Pattern:
MediatR acts as a mediator between requests and handlers.
Decouples the API from specific implementations.
Minimal APIs with Routing:
Simple route definition:

app.MapGet("/products", (IProductService productService) =>
    productService.GetAllProducts());
Example Flow in a Real Project
Domain:

Build a system for order management in an e-commerce app.
Design entities: Order, Product, Customer.
Use JSON documents to store orders.
API:

Minimal API with endpoints like GET /orders/{id} and POST /orders.
Challenges:

State Management: Handling eventual consistency in CQRS.
Validation: Complex validation logic for nested JSON objects using FluentValidation.
Resolution:

Use MediatR for decoupled command and query handling.
Employ FluentValidation for input rules.
Store read models in a separate database to optimize queries
*/