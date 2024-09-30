
namespace Catalog.Api.Products.GetProductById;

//public record GetProductByIdRequest(Guid ProductId);
public record GetProductByIdResponse(Product Product);
public class GetProductByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Products/{id}", async (Guid id, ISender sender) =>
        {
            var result= await sender.Send(new GetProductByIdQuery(id));

            var response = result.Adapt<GetProductByIdResponse>();

            return Results.Ok(response);
        }).WithName("GetProductById")
        .WithSummary("Get Product by Id")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
