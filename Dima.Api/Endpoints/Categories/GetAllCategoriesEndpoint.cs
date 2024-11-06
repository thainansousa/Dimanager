using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Categories;

public class GetAllCategoriesEndpoint : IEndpoint {
    public static void Map(IEndpointRouteBuilder app) => app.MapGet("/", HandleAsync)
    .WithName("Categorias: Buscar")
    .WithDescription("Buscar todas as categorias")
    .WithSummary("Buscar todas as categorias")
    .WithOrder(5)
    .Produces<PagedResponse<List<Category>?>>();

    private static async Task<IResult> HandleAsync(
    ICategoryHandler handler,
    ClaimsPrincipal user,
    [FromQuery] int pageNumber = DefaultConfigurations.DefaultPageNumber, 
    [FromQuery] int pageSize = DefaultConfigurations.DefaultPageSize){

        var request = new GetAllCategoriesRequest{
                UserId = user.Identity?.Name ?? string.Empty,
                PageNumber = pageNumber,
                PageSize = pageSize
        };

        var result = await handler.GetAllAsync(request);

        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);

    }
}