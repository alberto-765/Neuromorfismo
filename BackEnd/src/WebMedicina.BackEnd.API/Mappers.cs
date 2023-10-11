using AutoMapper;
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
            CreateMap<UserRegistroDto, UserInfoDto>();
            CreateMap<UserLoginDto, UserInfoDto>()
                .ForMember(dest => dest.NumHistoria, co => co.MapFrom(src => src.UserName));

            // Mapeo modelo medico
            CreateMap<MedicosModel, UserRegistroDto>().ReverseMap();
            CreateMap<MedicosModel, UserInfoDto>();



            // Mapeamos listas
            //CreateMap<ListaOrigen, ListaDestino>()
            //	.ReverseMap(); // esto es para que pueda ser en ambos sentidos y no solo origen-destino
        }
    }
}
