namespace _3_Examen.DTOs
{
    public class DepartamentoDTOConInquilinos: GetDepartamentoDTO
    {
        public List<InquilinoDTO> Inquilino { get; set; }
    }
}
