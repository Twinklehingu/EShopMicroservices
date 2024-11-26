using BuildingBlocks.CQRS;
using CatalogAPI.Models;
using MediatR;
using System.Windows.Input;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description,
        string ImageFile, decimal Price) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

            //create Product entity from command object
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            //save to db

            //return result
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}

/*
It suppose to receive the request, it will forward it to appropriate endpoint

client -> send the request -> marten -> to command handler -> response
what command handler is doing?
marten has to trigger command query  (IRequest) interface  to handle this command handler needs to have IRequestHandler
marten -> command query -> execute the appropriate code in our repository -> there will be a -> database -> return as a result


Flow Diagram
Client makes a request (e.g., create a product).
Request is routed to the command/query handler (via Mediator pattern).
The handler processes the command/query and calls the repository.
The repository uses Marten to interact with the database.
Database performs the operation, and the result is returned to the handler.
The handler formats the result and sends it back to the client.





 */