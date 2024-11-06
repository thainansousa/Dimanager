using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint {
    public static void Map(IEndpointRouteBuilder app) => app.MapPut("/{id}", HandleAsync)
    .WithName("Categoria: Atualizar")
    .WithDescription("Atualizar uma categoria")
    .WithSummary("Atualizar uma categoria")
    .WithOrder(2)
    .Produces<Response<Category?>>();


    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler, 
        UpdateCategoryRequest request,
        Guid id,
        ClaimsPrincipal user){
        
        request.Id = id;
        request.UserId = user.Identity?.Name ?? string.Empty;
        
        var result = await handler.UpdateAsync(request);

        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}