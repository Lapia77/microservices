
namespace Catalog.Api.Products.DeleteProduct;


public record DeleteProductResponse(bool IsSucces);

public class DeleteProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}",async(Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(id));

            var response = result.Adapt<DeleteProductResponse>();

            return Results.Ok(response);
        })
            .WithName("DeleteProduct")
            .WithDescription("Delete Product")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
