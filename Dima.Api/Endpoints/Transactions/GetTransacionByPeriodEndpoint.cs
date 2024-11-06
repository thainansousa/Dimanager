using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Transactions;

public class GetTransactionByPeriodEndpoint : IEndpoint {
    public static void Map(IEndpointRouteBuilder app) => app.MapGet("/", HandleAsync)
    .WithName("Transactions: Get By Period")
    .WithDescription("Buscar todas as transações de um periodo.")
    .WithSummary("Buscar todas as transação de um periodo.")
    .WithOrder(5)
    .Produces<PagedResponse<List<Transaction>?>>();

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        ClaimsPrincipal user,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int pageNumber = DefaultConfigurations.DefaultPageNumber,
        [FromQuery] int PageSize = DefaultConfigurations.DefaultPageSize
    ){
        var request = new GetTransactionByPeriodRequest {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = PageSize,
            StartDate = startDate,
            EndDate = endDate
        };

        var result = await handler.GetByPeriodAsync(request);

        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }
}