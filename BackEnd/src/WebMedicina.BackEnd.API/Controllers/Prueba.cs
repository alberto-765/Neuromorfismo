using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebMedicina.BackEnd.API.Controllers {
	[Route("api")]
	[ApiController]
	public class Prueba : ControllerBase {

		

		[HttpGet("prueba/{parametro}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult<string>> Get(String dato) {
			return Ok(dato);
		}



	}
}
