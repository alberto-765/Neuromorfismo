using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.Shared.Service;

namespace WebMedicina.Shared.Dto {
    public class FiltradoPacientesDto {

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.DateTime, ErrorMessage = "El formato debe ser dd/MM/yyyy")]
        [CustomValidation(typeof(ValidacionesRegistro), "ValidateFechaNacimiento")]
        public DateTime? FechaNac { get; set; } = ValidacionesRegistro.ObtenerFechaMaxNacimiento();

        [Required(ErrorMessage = "El campo género es obligatorio")]
        [ValidacionLista("M", "H")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Debes especificar una talla para el paciente.")]
        [Range(50, 200, ErrorMessage = "La talla debe ser entre 50 y 200 cm.")]
        public decimal Talla { get; set; }

        public DateTime? FechaDiagnostico { get; set; }

        public DateTime? FechaFractalidad { get; set; }

        public int? IdFarmaco { get; set; }

        public int? IdEpilepsia { get; set; }

        public int? IdMutacion { get; set; }

        public string EnfermRaras { get; set; }

        public string DescripEnferRaras { get; set; }

        [DataType(DataType.Date, ErrorMessage = "El formato debe ser dd/MM/yyyy")]
        public DateOnly FechaCreac { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [DataType(DataType.Date, ErrorMessage = "El formato debe ser dd/MM/yyyy")]
        public DateOnly FechaUltMod { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public string? MedicoUltMod { get; set; }

        public string? MedicoCreador { get; set; }
    }
}
