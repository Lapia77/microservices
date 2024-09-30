
using Marten;

namespace Catalog.Api.Products.GetProducts;

public record GetProductsQuery():IQuery<GetProductsResults>;

public record GetProductsResults(IEnumerable<Product> Products);
internal class GetProductQueryHandler(IDocumentSession session, ILogger<GetProductQueryHandler> logger) : IQueryHandler<GetProductsQuery,GetProductsResults>
{
    public async Task<GetProductsResults> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductQuery.Handler called with {@request}",request);
        var products= await session.Query<Product>().ToListAsync<Product>(cancellationToken);

        return new GetProductsResults(products);
    }
}
