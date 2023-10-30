using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IAdminsService {
        bool CrearMedico(UserRegistroDto nuevoMedico, String idUsuario);
        Task<List<UserUploadDto>> ObtenerFiltradoUsuarios(Dictionary<string, string> filtros, ClaimsPrincipal user);
        List<UserUploadDto> FiltrarUsuarios(List<UserUploadDto> listaUsuarios, ClaimsPrincipal user);

        Task<bool> ActualizarMedico(UserUploadDto medicoActualizado);
        List<EpilepsiasDto> ObtenerEpilepsias();
    }
}
