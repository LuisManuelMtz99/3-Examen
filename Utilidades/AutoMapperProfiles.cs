using AutoMapper;
using _3_Examen.DTOs;
using _3_Examen.Entidades;

namespace _3_Examen.Utilidades { 
 public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<DepartamentoDTO, Departamento>();
        CreateMap<Departamento, GetDepartamentoDTO>();
        CreateMap<Departamento, DepartamentoDTOConInquilinos>()
            .ForMember(departamentoDTO => departamentoDTO.Inquilino, opciones => opciones.MapFrom(MapDepartamentoDTOInquilinos));
        CreateMap<InquilinoCreacionDTO, Inquilino>()
            .ForMember(inquilino => inquilino.DepartamentoInquilino, opciones => opciones.MapFrom(MapDepartamentoInquilino));
        CreateMap<Inquilino, InquilinoDTO>();
        CreateMap<Inquilino, InquilinoDTOConDepartamento>()
            .ForMember(inquilinoDTO => inquilinoDTO.Departamentos, opciones => opciones.MapFrom(MapInquilinoDTODepartamento));
        CreateMap<InquilinoPatchDTO, Inquilino>().ReverseMap();

    }
    private List<InquilinoDTO> MapDepartamentoDTOInquilinos(Departamento departamento, GetDepartamentoDTO getDepartamentoDTO)
    {
        var result = new List<InquilinoDTO>();

        if (departamento.DepartamentoInquilino == null) { return result; }

        return result;
    }

    private List<GetDepartamentoDTO> MapInquilinoDTODepartamento(Inquilino dato, InquilinoDTO DatoDTO)
    {
        var result = new List<GetDepartamentoDTO>();

        if (dato.DepartamentoInquilino == null)
        {
            return result;
        }

        foreach (var juegodato in dato.DepartamentoInquilino)
        {
            result.Add(new GetDepartamentoDTO()
            {
                Id = juegodato.DepartamentoId,
                
            });
        }

        return result;
    }

    private List<DepartamentoInquilino> MapDepartamentoInquilino(InquilinoCreacionDTO datoCreacionDTO, Inquilino dato)
    {
        var resultado = new List<DepartamentoInquilino>();

        if (datoCreacionDTO.DepartamentoIds == null) { return resultado; }
        foreach (var juegoId in datoCreacionDTO.DepartamentoIds)
        {
            resultado.Add(new DepartamentoInquilino() { DepartamentoId = juegoId });
        }
        return resultado;
    }
}
}