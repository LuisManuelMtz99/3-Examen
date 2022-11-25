using System.ComponentModel.DataAnnotations;

namespace _3_Examen.DTOs
{
    public class DepartamentoDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")] //
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} solo puede tener hasta 150 caracteres")]
        public string Numero { get; set; }

    }
}
