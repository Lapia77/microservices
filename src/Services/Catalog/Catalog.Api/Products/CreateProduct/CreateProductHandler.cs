
namespace Catalog.Api.Products.CreateProduct;

public record CreateProductCommand(string name,
                                   List<string> categories, 
                                   string description,
                                   string imageFile,
                                   decimal price ) : ICommand<CreateProductResult>;
public record CreateProductResult( Guid productId );
internal class CreateProductHandler(IDocumentSession _session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //Business logic to create product
        Product product = new Product()
        {
            Name = command.name,
            Categories = command.categories,
            Description = command.description,
            ImageFile = command.imageFile,
            Price = command.price
        };

        _session.Store( product );
        await _session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }

}
