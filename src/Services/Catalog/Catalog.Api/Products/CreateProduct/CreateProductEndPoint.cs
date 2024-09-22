namespace Catalog.Api.Products.CreateProduct;

public record CreateProductRequest(string name,
                                   List<string> categories,
                                   string description,
                                   string imageFile,
                                   decimal price);

public record CreateProductResponse(Guid ProductId);
public class CreateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app) 
    {
        app.MapPost("/products",async(CreateProductRequest request, ISender sender) =>
        {
            var command= request.Adapt<CreateProductCommand>();

            var result = await sender.Send(command);

            var response= result.Adapt<CreateProductResponse>();

            return Results.Created($"/Products/{response.ProductId}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product"); 
    }
}
