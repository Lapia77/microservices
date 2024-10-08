using JasperFx.Core;
using Marten.Linq.QueryHandlers;

namespace Catalog.Api.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category): IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);
internal class GetProductByCategoryQueryHandler(IDocumentSession session) :
    IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {

        var results = await session.Query<Product>()
                                   .Where(prod=>prod.Categories.Contains(query.Category))
                                   .ToListAsync();
        return new GetProductByCategoryResult(results);
    }
}
