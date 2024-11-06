namespace Dima.Core.Requests.Transactions;

public class GetTransactionByIdRequest : Requests {
    public Guid Id { get; set; }
}