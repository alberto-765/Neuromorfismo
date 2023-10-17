using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.API.Controllers {
    [Route("/api/gestionUsers")]
    [ApiController]
    [Authorize(Roles = "superAdmin, admin")]
    public class GestionUsersController : Controller {
        private readonly IMapper _mapper;
        private readonly IAdminsService _adminService;
        private readonly IIdentityService _identityService;

        public GestionUsersController(IMapper mapper, IAdminsService adminsService, IIdentityService identity) {
            _mapper = mapper;
            _adminService = adminsService;
            _identityService = identity;
        }

        [HttpPost("obtenerUsuariosFiltrados")]
        public async Task<IActionResult> ObtenerUsuariosFiltrados([FromBody] filtros){

        }

    }
