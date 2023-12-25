using System.Security.Claims;
using WebMedicina.BackEnd.Dto;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public static class Comun  {
        public static CrearPacienteDto? MapearPacienteModdel(InfoPacienteDto? infoPaciente) {
            try {
                if (infoPaciente is null) return null;

                return new() {
                    IdPaciente = infoPaciente.Paciente.IdPaciente,
                    NumHistoria = infoPaciente.Paciente.NumHistoria,
                    FechaNac = infoPaciente.Paciente.FechaNac,
                    Sexo = infoPaciente.Paciente.Sexo,
                    Talla = infoPaciente.Paciente.Talla,
                    FechaDiagnostico = infoPaciente.Paciente.FechaDiagnostico,
                    FechaFractalidad = infoPaciente.Paciente.FechaFractalidad,
                    Farmaco = infoPaciente.Paciente.Farmaco,
                    Epilepsia = infoPaciente.Paciente.IdEpilepsia.ToString(),
                    NombreEpilepsia = infoPaciente.NombreEpilepsia,
                    NombreMutacion = infoPaciente.NombreMutacion,
                    IdMutacion = infoPaciente.Paciente.IdMutacion.ToString(),
                    EnfermRaras = (infoPaciente.Paciente.EnfermRaras == "S" ? true : false),
                    DescripEnferRaras = (infoPaciente.Paciente.EnfermRaras == "S" ? infoPaciente.Paciente.DescripEnferRaras : string.Empty),
                    FechaCreac = infoPaciente.Paciente.FechaCreac,
                    FechaUltMod = infoPaciente.Paciente.FechaUltMod,
                    NombreMedicoCreador = infoPaciente.Paciente.MedicoCreadorNavigation?.UserLogin ?? string.Empty,
                    NombreMedicoUltMod = infoPaciente.Paciente.MedicoUltModNavigation?.UserLogin ?? string.Empty,
                    MedicoCreador = infoPaciente.Paciente.MedicoCreador,
                    MedicoUltMod = infoPaciente.Paciente.MedicoUltMod,
                    MedicosPacientes = infoPaciente.MedicosPacientes.ToDictionary(x => x.IdMedico, x => x.UserLogin)
                };
            } catch (Exception) {
                throw;
            }
        }

        public static bool ObtenerIdUsuario(ClaimsPrincipal? user, out int idMedico) {
            try {
                if (int.TryParse(user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out idMedico)) {
                    return true;
                }
                return false;
            } catch (Exception) {
                throw;
            }
        }
    }
}
