using System.ComponentModel.DataAnnotations;
using Dima.Core.Enums;

namespace Dima.Core.Requests.Transactions;

public class CreateTransactionRequest : Requests {
    
    [Required(ErrorMessage = "O título não pode ser vázio.")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "O tipo não pode ser vázio.")]
    public ETransactionType Type { get; set; }
    
    [Required(ErrorMessage = "O valor não pode ser vázio.")]
    public decimal Amount { get; set; }
    
    [Required(ErrorMessage = "A categoria não pode ser vázia.")]
    public Guid CategoryId { get; set; }
    
    [Required(ErrorMessage = "Data inválida.")]
    public DateTime? PaidOrReceivedAt { get; set; }
}