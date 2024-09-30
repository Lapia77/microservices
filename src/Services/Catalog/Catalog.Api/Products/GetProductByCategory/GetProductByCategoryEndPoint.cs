
namespace Catalog.Api.Products.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var results= await sender.Send(new GetProductByCategoryQuery(category));

            var responses= results.Adapt<GetProductByCategoryResponse>();

            return Results.Ok(responses);
        });
    }
}
