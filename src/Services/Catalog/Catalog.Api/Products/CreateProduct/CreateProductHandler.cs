
namespace Catalog.Api.Products.CreateProduct;

public record CreateProductCommand(string Name,
                                   List<string> Categories,
                                   string Description,
                                   string ImageFile,
                                   decimal Price) : ICommand<CreateProductResult>;
public record CreateProductResult( Guid ProductId );
internal class CreateProductHandler(IDocumentSession _session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //Business logic to create product
        Product product = new Product()
        {
            Name = command.Name,
            Categories = command.Categories,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        _session.Store( product );
        await _session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }

}
