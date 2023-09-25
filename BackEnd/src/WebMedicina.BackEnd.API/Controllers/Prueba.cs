using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace WebMedicina.BackEnd.API.Controllers {
	[Route ("api/[controller]")]
	[ApiController]
	public class Prueba {
		// Para usar automap
		private readonly IMapper _mapper;
		public Prueba(IMapper mapper) {
			_mapper = mapper;

			// Para transoformar la clase2 uno en la clase1 lo hacemos en una funcion
			//_mapper.Map<Clase1>(Clase2)
		}


		[HttpGet]
		public string Get(string nombre) {
			return $"Hola {nombre}";
		}



    }
}
