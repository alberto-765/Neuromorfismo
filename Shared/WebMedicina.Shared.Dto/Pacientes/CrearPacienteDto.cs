using System.ComponentModel.DataAnnotations;
using WebMedicina.Shared.Dto.LineaTemporal;
using WebMedicina.Shared.Service;

namespace WebMedicina.Shared.Dto.Pacientes {
    public class CrearPacienteDto : BasePaciente{
        [Required(ErrorMessage = "El número de historia es obligatorio.")]
        [MaxLength(12, ErrorMessage = "El número de historia debe contener 12 dígitos.")]
        [MinLength(12, ErrorMessage = "El número de historia debe contener 12 dígitos.")]
        [RegularExpression(@"^AN\d{10}$", ErrorMessage = "El formato debe ser ANXXXXXXXXXX")]
        public override string NumHistoria { get; set; } = null!;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [CustomValidation(typeof(ValidacionesRegistro), "ValidateFechaNacPaciente")]
        public override DateTime? FechaNac { get; set; } = ValidacionesRegistro.ObtenerFechaMaxNacimiento();

        [Required(ErrorMessage = "El campo género es obligatorio.")]
        [ValidacionLista("M", "H")]
        public override string Sexo { get; set; } = null!;

        [Required(ErrorMessage = "Debes especificar una talla para el paciente.")]
        [Range(50, 200, ErrorMessage = "La talla debe ser entre 50 y 200 cm.")]
        public override int Talla { get; set; } = 50;

        // Medicos que tienen permisos sobre el paciente
        public Dictionary<int, string>? MedicosPacientes { get; set; }

        public SortedList<short, EvolucionLTDto>? Evoluciones { get; set; }
    }
}
