
namespace Catalog.Api.Products.CreateProduct;

public record CreateProductCommand(string Name,
                                   List<string> Categories,
                                   string Description,
                                   string ImageFile,
                                   decimal Price) : ICommand<CreateProductResult>;
public record CreateProductResult( Guid ProductId );

public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Categories).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}
internal class CreateProductHandler(IDocumentSession _session) : 
    ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //Business logic to create product

        var product = new Product()
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
