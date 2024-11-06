using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint {
    public static void Map(IEndpointRouteBuilder app) => app.MapPut("/{id}", HandleAsync)
    .WithName("Transactions: Update")
    .WithDescription("Atualizar uma transação.")
    .WithSummary("Atualizar uma transação.")
    .WithOrder(2)
    .Produces<Response<Transaction?>>();


    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        UpdateTransactionRequest request,
        Guid id,
        ClaimsPrincipal user
    ){
        request.Id = id;
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.UpdateAsync(request);

        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }
}