using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace _3_Examen.Entidades
{
    public class Inquilino
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 20, ErrorMessage = "El campo {0} solo puede tener hasta 20 caracteres.")]
        public string Name { get; set; }

        public int Renta { get; set; }

        public string Estado { get; set; }

        public string UsuarioId { get; set; }

        public IdentityUser Usuario { get; set; }

        public List<DepartamentoInquilino> DepartamentoInquilino { get; set; }

    }
}
