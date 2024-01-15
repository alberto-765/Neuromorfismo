using System.Runtime.CompilerServices;
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto.LineaTemporal;

namespace WebMedicina.BackEnd.ServicesDependencies.Mappers
{
    public static class LineaTemporalMap {
        public static EtapaLTDto ToDto(this EtapaLTModel modelo) =>
            new (modelo.Id, modelo.Titulo, modelo.Label, modelo.Descripcion, modelo.IdMedicoCreador, modelo.IdMedicoUltModif);

        public static EtapaLTModel ToModel(this EtapaLTDto dto) =>
         new() {
             Id = dto.Id,
             Descripcion = dto.Descripcion,
             Titulo = dto.Titulo,
             Label = dto.Label
         };

        public static EvolucionLTDto ToDto(this EvolucionLTModel modelo) =>
           new (modelo.Id, modelo.Confirmado, modelo.Fecha, modelo.IdMedicoUltModif, modelo.IdEtapa, modelo.IdPaciente);

        public static EvolucionLTModel ToModel(this EvolucionLTDto dto) =>
         new() {
             Id = dto.Id,
             Confirmado = dto.Confirmado,
             Fecha = dto.Fecha,
             IdMedicoUltModif = dto.IdMedicoUltModif,
             Etapa = new EtapaLTModel { Id = dto.Id }
         };
    }
}
