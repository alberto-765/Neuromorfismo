using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
            CreateMap<UserLoginDto, UserInfoDto>()
                .ForMember(dest => dest.NumHistoria, co => co.MapFrom(src => src.UserName));

            // Mapeo modelo medico
            CreateMap<MedicosModel, UserRegistroDto>().ReverseMap();
            CreateMap<MedicosModel, UserInfoDto>();

            // Mapeo token de User a UserInfoDto
            CreateMap<ClaimsPrincipal, UserInfoDto>()
                .ForMember(dest => dest.NumHistoria, co => co.MapFrom(src => src.FindFirst(JwtRegisteredClaimNames.Sub).Value))
                .ForMember(dest => dest.Nombre, co => co.MapFrom(src => src.FindFirst("nombre").Value))
                .ForMember(dest => dest.Apellidos, co => co.MapFrom(src => src.FindFirst("apellidos").Value))
                .ForMember(dest => dest.Rol, co => co.MapFrom(src => src.FindFirst(ClaimTypes.Role).Value));


            // Mapeo modelo medico en dto upload
            CreateMap<MedicosModel, UserUploadDto>().ReverseMap();

            // Mapeo Epilepsias
            CreateMap<EpilepsiaModel, EpilepsiasDto>().ReverseMap();

            // Mapeamos listas
            //CreateMap<ListaOrigen, ListaDestino>()
            //	.ReverseMap(); // esto es para que pueda ser en ambos sentidos y no solo origen-destino
        }
    }
}
