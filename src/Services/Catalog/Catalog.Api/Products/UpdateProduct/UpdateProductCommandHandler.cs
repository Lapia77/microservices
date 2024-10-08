 namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductCommand (Guid Id, string Name,
                                   List<string> Categories,
                                   string Description,
                                   string ImageFile,
                                   decimal Price): ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSucces);

public class UpdateProductCommandValidator: AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x=>x.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Categories).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}
internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) :
    ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommandHandler.Handle called with {@command}", command);

        var productExist = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (productExist is null) { 
            throw new ProductNotFoundException(command.Id);
        }

        productExist.Name = command.Name;
        productExist.Description = command.Description; 
        productExist.ImageFile = command.ImageFile;
        productExist.Price = command.Price;
        productExist.Categories = command.Categories;

        session.Update<Product>(productExist);

        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}