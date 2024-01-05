using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.BackEnd.ServicesDependencies.Mappers {
    public static class LineaTemporalMap {
        public static EtapasDto ToDto(this EtapaLTModel modelo) =>
            new() {
               Descripcion = modelo.Descripcion,
               Titulo = modelo.Titulo,
               Label = modelo.Label
            };

        public static EtapaLTModel ToModel(this EtapasDto dto) =>
         new() {
             Descripcion = dto.Descripcion,
             Titulo = dto.Titulo,
             Label = dto.Label
         };
    }
}
