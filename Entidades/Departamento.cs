using System.ComponentModel.DataAnnotations;

namespace _3_Examen.Entidades
{
    public class Departamento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(maximumLength: 20, ErrorMessage = "El campo {0} solo puede tener hasta 20 caracteres.")]
        public string Numero { get; set; }

        public string Disponibilidad { get; set; }
        public List<DepartamentoInquilino> DepartamentoInquilino { get; set; }
    }
}
