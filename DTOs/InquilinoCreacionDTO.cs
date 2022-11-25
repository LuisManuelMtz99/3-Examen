using System.ComponentModel.DataAnnotations;
namespace _3_Examen.DTOs
{
    public class InquilinoCreacionDTO
    { 
    [Required]
    [StringLength(maximumLength: 50, ErrorMessage = "El campo {0} solo puede tener hasta 50 caracteres")]
    public string Name { get; set; }

    public DateTime FechaCreacion { get; set; }

    public List<int> DepartamentoIds { get; set; }
}
}
