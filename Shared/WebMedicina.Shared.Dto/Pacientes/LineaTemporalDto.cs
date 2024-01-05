namespace WebMedicina.Shared.Dto.Pacientes
{

    // DTO ETAPAS
    public record EtapasDto(string? Descripcion = null, string Titulo = null!, string Label = null!);

    // DTO EvolucionEtapas
    public record EvolucionEtapasDto(int Id, string Nombre = null!, string Titulo = null!, string Label = null!);



}

