using AutoMapper;

namespace WebMedicina.BackEnd.API {
	public class Mappers :Profile {

		public Mappers() {
			//// Mapeamos una clase a otra
			//CreateMap<ClaseOrigen, ClaseDestino>()

			//// Mapeamos clases con diferentes propiedades
			//CreateMap<ClaseOrigen, ClaseDestino>()
			//.ForMember(cd => cd.Propiedad, co => co.MapFrom(co2 => co2.PropiedadDiferente));

			// Mapeamos listas
			//CreateMap<ListaOrigen, ListaDestino>()
			//	.ReverseMap(); // esto es para que pueda ser en ambos sentidos y no solo origen-destino
		}
	}
}
