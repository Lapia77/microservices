
namespace Catalog.Api.Products.GetProductById;

public record GetProductByIdQuery(Guid ProductId): IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);
internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) :
    IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken) 
    {
        logger.LogInformation("GetProductById.Handle  called with {@query}", query);

        var produit = await session.LoadAsync<Product>(query.ProductId,cancellationToken);

        if (produit is null) {
             throw new ProductNotFoundException();
        }

        return new GetProductByIdResult(produit);
    }
}
