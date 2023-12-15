using AutoMapper;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebMedicina.BackEnd.Dto;
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.API {
	public class Mappers :Profile {

		public Mappers() {
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
            CreateMap<EpilepsiaModel, EpilepsiasDto>()
                .AfterMap((origen, destino) => {
                    int indice = 1;
                    
                    // recorremos la lista y añadimos el indice
                    destino.IdEpilepsia = indice;
                    indice++;
                })
                .ReverseMap();

            // Mapeo Mutaciones
            CreateMap<MutacionesModel, MutacionesDto>().ReverseMap();

            // Mapeo Farmacos
            CreateMap<FarmacosModel, FarmacosDto>().ReverseMap();

            // Mapeo tabla medicos pacientes
            CreateMap<MedicospacienteModel, MedicosPacientesDto>();

            // Mapeo crear pacientes
            CreateMap<PacientesModel, CrearPacienteDto>().ReverseMap()
                .ForMember(dest => dest.EnfermRaras, co => co.MapFrom(src => (src.EnfermRaras ? 'S' : 'N')))
                .ForMember(dest => dest.DescripEnferRaras, co => co.MapFrom(src => (src.EnfermRaras ? src.DescripEnferRaras : "")));

            // Mapeo pacientes
            CreateMap<PacientesModel, PacienteDto>()
                .ForMember(dest => dest.EnfermRaras, co => co.MapFrom(src => (src.EnfermRaras == "S" ? "Sí" : "No")))
                .ForMember(dest => dest.DescripEnferRaras, co => co.MapFrom(src => (src.EnfermRaras == "S" ? src.DescripEnferRaras : "")))
                .ReverseMap();

            // Mapeamos listas
            //CreateMap<ListaOrigen, ListaDestino>()
            //	.ReverseMap(); // esto es para que pueda ser en ambos sentidos y no solo origen-destino
        }
    }
}
