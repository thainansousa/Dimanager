using System.Security.Claims;
using Azure;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;

namespace Dima.Api.Endpoints.Categories;

public class CreateCategoryEndpoint : IEndpoint {
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
    .WithName("Categoria: Criar")
    .WithSummary("Criar uma nova categoria")
    .WithDescription("Criar uma nova categoria")
    .WithOrder(1)
    .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler, 
        CreateCategoryRequest request,
        ClaimsPrincipal user
        ){
        
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);

        return result.IsSuccess 
        ? TypedResults.Created($"/{result.Data?.Id}", result) 
        : TypedResults.BadRequest(result);
    }
}