
using Marten;

namespace Catalog.Api.Products.GetProducts;

public record GetProductsQuery():IQuery<GetProductsResults>;

public record GetProductsResults(IEnumerable<Product> Products);
internal class GetProductQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery,GetProductsResults>
{
    public async Task<GetProductsResults> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products= await session.Query<Product>().ToListAsync<Product>(cancellationToken);

        return new GetProductsResults(products);
    }
}
