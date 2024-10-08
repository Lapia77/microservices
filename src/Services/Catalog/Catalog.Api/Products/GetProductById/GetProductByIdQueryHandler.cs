
namespace Catalog.Api.Products.GetProductById;

public record GetProductByIdQuery(Guid ProductId): IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);
internal class GetProductByIdQueryHandler(IDocumentSession session) :
    IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken) 
    {

        var produit = await session.LoadAsync<Product>(query.ProductId,cancellationToken);

        return produit is null ? throw new ProductNotFoundException(query.ProductId) : new GetProductByIdResult(produit);
    }
}
