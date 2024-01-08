using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto.LineaTemporal;

namespace WebMedicina.BackEnd.ServicesDependencies.Mappers
{
    public static class LineaTemporalMap {
        public static EtapaLTDto ToDto(this EtapaLTModel modelo) =>
            new() {
                Id = modelo.Id,
                Descripcion = modelo.Descripcion,
                Titulo = modelo.Titulo,
                Label = modelo.Label
            };

        public static EtapaLTModel ToModel(this EtapaLTDto dto) =>
         new() {
             Id = dto.Id,
             Descripcion = dto.Descripcion,
             Titulo = dto.Titulo,
             Label = dto.Label
         };

        public static EvolucionLTDto ToDto(this EvolucionLTModel modelo) =>
           new() {
              Id = modelo.Id,
              Confirmado = modelo.Confirmado,
              Fecha = modelo.Fecha,
              MedicoUltModif = modelo.MedicoUltModif.ToUserInfoDto(),
              IdEtapa = modelo.Etapa.Id
           };

        public static EvolucionLTModel ToModel(this EvolucionLTDto dto) =>
         new() {
             Id = dto.Id,
             Confirmado = dto.Confirmado,
             Fecha = dto.Fecha,
             MedicoUltModif = dto.MedicoUltModif.ToModel(),
             Etapa = new EtapaLTModel { Id = dto.Id }
         };
    }
}
