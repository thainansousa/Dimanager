using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Transactions;

public class DeleteTransactionEndpoint : IEndpoint {
    public static void Map(IEndpointRouteBuilder app) => app.MapDelete("/{id}", HandleAsync)
    .WithName("Transactions: Delete")
    .WithDescription("Excluir uma transação")
    .WithSummary("Excluir uma transação")
    .WithOrder(3)
    .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandleAsync(ITransactionHandler handler, Guid id, ClaimsPrincipal user){
        var request = new DeleteTransactionRequest {
            Id = id,
            UserId = user.Identity?.Name ?? string.Empty
        };

        var result = await handler.DeleteAsync(request);

        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }
}