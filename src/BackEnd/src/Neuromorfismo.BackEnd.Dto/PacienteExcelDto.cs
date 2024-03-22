using ClosedXML.Attributes; 

namespace Neuromorfismo.Shared.Dto.Pacientes {
    public record PacienteExcelDto {
        public PacienteExcelDto(CrearPacienteDto paciente)
        {
            this.NumHistoria = paciente.NumHistoria;
            this.FechaNac = DateOnly.FromDateTime(paciente.FechaNac ?? DateTime.MinValue);
            this.Sexo = paciente.Sexo == "H" ? "Hombre" : paciente.Sexo == "M" ? "Mujer" : string.Empty;
            this.Talla = paciente.Talla;
            this.FechaDiagnostico = paciente.FechaDiagnostico;
            this.FechaFractalidad = paciente.FechaFractalidad;
            this.Farmaco = paciente.Farmaco;
            this.EnfermRaras = paciente.EnfermRaras ? "Sí" : "No";
            this.DescripEnferRaras = paciente.DescripEnferRaras;
            this.FechaCreac = paciente.FechaCreac;
            this.FechaUltMod = paciente.FechaUltMod;
            this.NombreMedicoUltMod = paciente.NombreMedicoUltMod;
            this.NombreMedicoCreador = paciente.NombreMedicoCreador;
            this.Epilepsia = paciente.Epilepsia?.Nombre;
            this.Mutacion = paciente.Mutacion?.Nombre;
        }

        [XLColumn(Header = "   Número de Historia   ", Order = 1)]
        public string NumHistoria { get; init; }

        [XLColumn(Header = "    Fecha Nacimiento    ", Order = 2)]
        public DateOnly FechaNac { get; init; }

        [XLColumn(Header = "    Género    ", Order = 3)]
        public string Sexo { get; init; }

        [XLColumn(Header = "       Talla(cm)       ", Order = 4)]
        public int Talla { get; init; }

        [XLColumn(Header = "    Fecha de Diagnóstico    ", Order = 5)]
        public DateTime? FechaDiagnostico { get; init; }

        [XLColumn(Header = "    Fecha de Fractalidad    ", Order = 6)]
        public DateTime? FechaFractalidad { get; init; }

        [XLColumn(Header = "      Fármaco      ", Order = 7)]
        public string Farmaco { get; init; }

        [XLColumn(Header = "      Epilepsia      ", Order = 8)]
        public string? Epilepsia { get; init; }

        [XLColumn(Header = "      Mutación      ", Order = 9)]
        public string? Mutacion { get; init; }

        [XLColumn(Header = "    Enfermedad Rara    ", Order = 10)]
        public string EnfermRaras { get; init; } 
        
        [XLColumn(Header = "    Descripción Enfermedad Rara    ", Order = 11)]
        public string DescripEnferRaras { get; init; }

        [XLColumn(Header = "    Fecha Creación    ", Order = 12)]
        public DateTime FechaCreac { get; init; }
        
        [XLColumn(Header = "    Fecha Última Modificación    ", Order = 13)]
        public DateTime FechaUltMod { get; init; } 

        [XLColumn(Header = "    Médico Creador    ", Order = 14)]
        public string NombreMedicoCreador { get; init; } 

        [XLColumn(Header = "    Médico Última Modificación    ", Order = 15)]
        public string NombreMedicoUltMod { get; init; } 
    }

    

}
