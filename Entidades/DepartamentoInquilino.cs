namespace _3_Examen.Entidades
{
    public class DepartamentoInquilino
    {
        public int DepartamentoId { get; set; }
        public int InquilinoId { get; set; }
        public Departamento Departamento { get; set; }
        public Inquilino Inquilino { get; set; }
    }
}
