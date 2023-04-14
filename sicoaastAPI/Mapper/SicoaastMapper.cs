using AutoMapper;
using sicoaastAPI.Models;
using sicoaastAPI.Models.Dtos.Ccosto;
using sicoaastAPI.Models.Dtos.Departamento;
using sicoaastAPI.Models.Dtos.Empresa;
using sicoaastAPI.Models.Dtos.MovEmp;
using sicoaastAPI.Models.Dtos.Organismo;
using sicoaastAPI.Models.Dtos.Usuario;

namespace sicoaastAPI.Mapper
{
    public class SicoaastMapper : Profile
    {
        public SicoaastMapper()
        {
            CreateMap<Empresa, EmpresaDto>().ReverseMap();
            CreateMap<Empresa, CrearEmpresaDto>().ReverseMap();
            CreateMap<Organismo, OrganismoDto>().ReverseMap();
            CreateMap<Organismo, CrearOrganismoDto>().ReverseMap();
            CreateMap<Ccosto, CcostoDto>().ReverseMap();
            CreateMap<Ccosto, CrearCcostoDto>().ReverseMap();
            CreateMap<Departamento, DepartamentoDto>().ReverseMap();
            CreateMap<Departamento, CrearDepartamentoDto>().ReverseMap();
            CreateMap<MovEmp, MovEmpDto>().ReverseMap();
            CreateMap<MovEmp, CrearMovEmpDto>().ReverseMap();
            CreateMap<MovEmp, PatchMovEmpDto>().ReverseMap();
            CreateMap<Usuario, CrearUsuarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioLoginDto>().ReverseMap();
            CreateMap<Usuario, UsuarioLoginRespuestaDto>().ReverseMap();
        }
    }
}
