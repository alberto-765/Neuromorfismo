using AutoMapper;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebMedicina.BackEnd.Dto;
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.API.Mappers
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            //// Mapeamos una clase a otra
            //CreateMap<ClaseOrigen, ClaseDestino>()

            //// Mapeamos clases con diferentes propiedades
            //CreateMap<ClaseOrigen, ClaseDestino>()
            //      .ForMember(dest => dest.PropiedadDestino, co => co.MapFrom(src => src.NombrePropiedadOrigen));

            // Mapper user info
            CreateMap<UserRegistroDto, UserInfoDto>().ReverseMap();
            CreateMap<UserLoginDto, UserInfoDto>();

            // Mapeo modelo medico
            CreateMap<MedicosModel, UserRegistroDto>().ReverseMap();
            CreateMap<MedicosModel, UserInfoDto>();

            // Mapeo token de User a UserInfoDto
            CreateMap<ClaimsPrincipal, UserInfoDto>()
                .ForMember(dest => dest.IdMedico, co => co.MapFrom(src => int.Parse(src.FindFirstValue(ClaimTypes.NameIdentifier))))
                .ForMember(dest => dest.UserLogin, co => co.MapFrom(src => src.FindFirstValue("UserName")))
                .ForMember(dest => dest.Nombre, co => co.MapFrom(src => src.FindFirstValue(ClaimsIdentity.DefaultNameClaimType)))
                .ForMember(dest => dest.Apellidos, co => co.MapFrom(src => src.FindFirstValue(ClaimTypes.Surname)))
                .ForMember(dest => dest.Rol, co => co.MapFrom(src => src.FindFirstValue(ClaimTypes.Role)));


            // Mapeo modelo medico en dto upload
            CreateMap<MedicosModel, UserUploadDto>().ReverseMap();

            // Mapeo Epilepsias
            CreateMap<EpilepsiaModel, EpilepsiasDto>().ReverseMap();

            // Mapeo Mutaciones
            CreateMap<MutacionesModel, MutacionesDto>().ReverseMap();


            // Mapeo Farmacos
            //CreateMap<List<FarmacosModel>, List<FarmacosDto>>()
            //    .AfterMap((origen, destino) => {

            //        // recorremos la lista y añadimos el indice
            //        destino.Indice = indice;
            //        indice++;
            //    })
            //    .ReverseMap();

            // Mapeo crear pacientes
            CreateMap<PacientesModel, CrearPacienteDto>()
                .ForMember(dest => dest.EnfermRaras, co => co.MapFrom(src => src.EnfermRaras == "S"))
                .ForMember(dest => dest.MedicosPacientes, co => co.Ignore()); ;

            // Mapeo crear pacientes
            CreateMap<CrearPacienteDto, PacientesModel>()
                .ForMember(dest => dest.EnfermRaras, co => co.MapFrom(src => src.EnfermRaras ? "S" : "N"))
                .ForMember(dest => dest.DescripEnferRaras, co => co.MapFrom(src => src.EnfermRaras ? src.DescripEnferRaras : string.Empty))
                .ForMember(dest => dest.Medicospacientes, co => co.Ignore());

            // Mapeamos listas
            //CreateMap<ListaOrigen, ListaDestino>()
            //	.ReverseMap(); // esto es para que pueda ser en ambos sentidos y no solo origen-destino
        }
    }
}
