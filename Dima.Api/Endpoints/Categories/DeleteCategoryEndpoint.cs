using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint {
    public static void Map(IEndpointRouteBuilder app) => app.MapDelete("/{id}", HandleAsync)
    .WithName("Categoria: Excluir")
    .WithDescription("Excluir uma categoria.")
    .WithSummary("Excluir uma categoria")
    .WithOrder(3)
    .Produces<Response<Category?>>();

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        Guid id,
        ClaimsPrincipal user) {

            var request = new DeleteCategoryRequest {
                Id = id,
                UserId = user.Identity?.Name ?? string.Empty
            };

            var result = await handler.DeleteAsync(request);

            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
        }
}