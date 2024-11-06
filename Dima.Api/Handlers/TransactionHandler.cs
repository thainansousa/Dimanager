using Dima.Api.Data;
using Dima.Core;
using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class TransactionHandler(AppDbContext context) : ITransactionHandler {
    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request){
        try
        {
            var transaction = new Transaction {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Type = request.Type,
                Amount = request.Amount,
                CreatedAt = DateTime.Now,
                CategoryId = request.CategoryId,
                PaidOrReceiveAt = request.PaidOrReceivedAt,
                UserId = request.UserId
            };

            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 201, "Nova transação criada com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new Response<Transaction?>(null, 500, "Erro interno no servidor. Contate o Desenvolvedor");
        }
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request){
        try
        {
            var transaction = await context.Transactions
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if(transaction is null) return new Response<Transaction?>(null, 404, "Transação não encontrada.");

            transaction.Title = request.Title;
            transaction.Amount = request.Amount;
            transaction.Type = request.Type;
            transaction.PaidOrReceiveAt = request.PaidOrReceivedAt;
            transaction.CategoryId = request.CategoryId;

            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 200, "Transação atualizada com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new Response<Transaction?>(null, 500, "Erro interno no servidor. Contate o Desenvolvedor");
        }
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request){
        try
        {
            var transaction = await context.Transactions
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if(transaction is null) return new Response<Transaction?>(null, 404, "Transação não encontrada.");

            transaction.Id = request.Id;

            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();

            return new Response<Transaction?>(transaction, 200, "Transação excluida com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new Response<Transaction?>(null, 500, "Erro interno no servidor. Contate o Desenvolvedor");
        }
    }
    
    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request){
        
        var transaction = await context.Transactions
        .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

        return transaction is null 
        ? new Response<Transaction?>(null, 404, "Transação não encontrada.")
        : new Response<Transaction?>(transaction, 200, "");
    }

    public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
    {
        try
        {
            request.StartDate ??= DateTime.Now.GetFirstDay();
            request.EndDate ??= DateTime.Now.GetLastDay();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new PagedResponse<List<Transaction>?>
            (null, 500, "Erro interno no servidor ao tratar periodo. Contate o Desenvolvedor.");
        }

        try
        {
            var query = context.Transactions
            .AsNoTracking()
            .Where(
            x => x.CreatedAt >= request.StartDate 
            && x.CreatedAt <= request.EndDate 
            && x.UserId == request.UserId)
            .OrderBy(x => x.CreatedAt);

            var transactions = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(DefaultConfigurations.DefaultPageSize)
            .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Transaction>?>
            (transactions, count, 1, DefaultConfigurations.DefaultPageSize, 200, "");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new PagedResponse<List<Transaction>?>
            (null, 500, "Erro interno no servidor. Contate o Desenvolvedor");
        }
    }
}