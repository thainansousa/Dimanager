using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories;

public class GetCategoryByIdEndpoint : IEndpoint {
    public static void Map(IEndpointRouteBuilder app) => app.MapGet("/{id}", HandleAsync)
    .WithName("Categoria: Buscar")
    .WithDescription("Buscar categoira por Id")
    .WithSummary("Buscar categoria por Id")
    .WithOrder(4)
    .Produces<Response<Category?>>();


    private static async Task<IResult> HandleAsync(ICategoryHandler handler, Guid id, ClaimsPrincipal user){
        var request = new GetCategoryByIdRequest {
            Id = id,
            UserId = user.Identity?.Name ?? string.Empty
        };

        var result = await handler.GetByIdAsync(request);

        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }
}