using Neuromorfismo.BackEnd.Model;
using Neuromorfismo.Shared.Dto.Pacientes;

namespace Neuromorfismo.BackEnd.ServicesDependencies.Mappers
{
    public static class PacientesMapers
    {
        // CrearPacienteDto a PacienteModel
        public static PacientesModel ToModel(this CrearPacienteDto model) =>
            new() {
                IdPaciente = model.IdPaciente,
                NumHistoria = model.NumHistoria,
                FechaNac = model.FechaNac ?? DateTime.MinValue,
                Sexo = model.Sexo,
                Talla = model.Talla,
                FechaDiagnostico = model.FechaDiagnostico ?? DateTime.MinValue,
                FechaFractalidad = model.FechaFractalidad ?? DateTime.MinValue,
                Farmaco = model.Farmaco,
                IdEpilepsia = model.Epilepsia?.IdEpilepsia,
                IdMutacion = model.Mutacion?.IdMutacion,
                EnfermRaras = model.EnfermRaras ? "S" : "N",
                DescripEnferRaras = model.EnfermRaras ? model.DescripEnferRaras : "",
                FechaCreac = model.FechaCreac,
                MedicoUltMod = model.MedicoUltMod,
                MedicoCreador = model.MedicoCreador
            };        

        // ModelPaciente to CrearPacienteDto
        public static CrearPacienteDto ToDto(this PacientesModel modelo) =>
            new() {
                IdPaciente = modelo.IdPaciente,
                NumHistoria = modelo.NumHistoria,
                FechaNac = modelo.FechaNac,
                Sexo = modelo.Sexo,
                Talla = modelo.Talla,
                FechaDiagnostico = modelo.FechaDiagnostico,
                FechaFractalidad = modelo.FechaFractalidad,
                Farmaco = modelo.Farmaco,
                Epilepsia = modelo.IdEpilepsiaNavigation?.ToDto(),
                Mutacion = modelo.IdMutacionNavigation?.ToDto(),
                EnfermRaras = modelo.EnfermRaras == "S",
                DescripEnferRaras = (modelo.EnfermRaras == "S" ? modelo.DescripEnferRaras : string.Empty),
                FechaCreac = modelo.FechaCreac,
                FechaUltMod = modelo.FechaUltMod,
                NombreMedicoCreador = modelo.MedicoCreadorNavigation?.Nombre ?? string.Empty,
                NombreMedicoUltMod = modelo.MedicoUltModNavigation?.Nombre ?? string.Empty,
                MedicoCreador = modelo.MedicoCreador,
                MedicoUltMod = modelo.MedicoUltMod,
                MedicosPacientes = modelo.Medicospacientes.ToDictionary(x => x.IdMedico, x => x.IdMedico.ToString())
            };
    }
}
