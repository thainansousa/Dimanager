using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories;

public class CreateCategoryRequest : Requests {
    
    [Required(ErrorMessage = "O título não pode ser vázio.")]
    [MaxLength(80, ErrorMessage = "O título deve conter no máximo 80 caracteres.")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "A descrição não pode ser vázia.")]
    public string Description { get; set; } = string.Empty;
}