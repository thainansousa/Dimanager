using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Transactions;

public class GetTransactionByIdEndpoint : IEndpoint {
    public static void Map(IEndpointRouteBuilder app) => app.MapGet("/{id}", HandleAsync)
    .WithName("Transactions: Get By Id")
    .WithDescription("Buscar uma transação por ID")
    .WithSummary("Buscar uma transação por ID")
    .WithOrder(4)
    .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        Guid id,
        ClaimsPrincipal user
    ){
        var request = new GetTransactionByIdRequest {
            Id = id,
            UserId = user.Identity?.Name ?? string.Empty
        };

        var result = await handler.GetByIdAsync(request);

        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }
}