using System.ComponentModel.DataAnnotations;

namespace _3_Examen.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
