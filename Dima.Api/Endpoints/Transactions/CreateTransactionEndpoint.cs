using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Transactions;

public class CreateTransactionEndpoint : IEndpoint {
    public static void Map(IEndpointRouteBuilder app) => app.MapPost("/", HandleAsync)
    .WithName("Transactions: Create")
    .WithDescription("Criar uma nova transação.")
    .WithSummary("Criar uma nova transação.")
    .WithOrder(1)
    .Produces<Response<Transaction?>>();


    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler, CreateTransactionRequest request, ClaimsPrincipal user)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);

        return result.IsSuccess 
        ? Results.Created($"/{result.Data?.Id}", result)
        : Results.BadRequest(result);
    }   
}