using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto;
using Microsoft.AspNetCore.Mvc;


namespace WebMedicina.BackEnd.API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class Prueba : ControllerBase {

		private readonly UserManager<Aspnetuser> _userManager;


		// Para usar automap
		private readonly IMapper _mapper;
		public Prueba(IMapper mapper) {
			_mapper = mapper;

			// Para transoformar la clase2 uno en la clase1 lo hacemos en una funcion
			//_mapper.Map<Clase1>(Clase2)
		}


		//[HttpGet]
		//public string Get(string nombre) {
		//	return $"Hola {nombre}";
		//}

		[HttpGet]
		public IActionResult Get(UsuarioDto datos) {
			if (ModelState.IsValid) {
				return Unauthorized();
			}
			return BadRequest();
		}



	}
}
