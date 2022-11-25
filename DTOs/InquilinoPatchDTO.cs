using System.ComponentModel.DataAnnotations;
namespace _3_Examen.DTOs
{
    public class InquilinoPatchDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "El campo {0} solo puede tener hasta 50 caracteres")]
        public string Name { get; set; }
        public int Renta { get; set; }

        public string Estado { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
