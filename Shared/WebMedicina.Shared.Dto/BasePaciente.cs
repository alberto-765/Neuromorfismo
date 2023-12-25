using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.Shared.Service;

namespace WebMedicina.Shared.Dto {
    public class BasePaciente {
        public int IdPaciente { get; set; }
        public virtual string NumHistoria { get; set; } = string.Empty;
        public virtual DateTime? FechaNac { get; set; } 
        public virtual string Sexo { get; set; } = string.Empty;
        public virtual int Talla { get; set; } = 50;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime? FechaDiagnostico { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime? FechaFractalidad { get; set; } 
        public virtual string Farmaco { get; set; } = string.Empty;
        public virtual bool EnfermRaras { get; set; }
        public virtual string DescripEnferRaras { get; set; } = null!;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly FechaCreac { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateOnly FechaUltMod { get; set; }
        public virtual string NombreMedicoUltMod { get; set; } = string.Empty;
        public virtual string NombreMedicoCreador { get; set; } = string.Empty;
        public virtual int MedicoUltMod { get; set; } 
        public virtual int MedicoCreador { get; set; } 
        public virtual string? IdMutacion { get; set; } 
        public virtual string? Epilepsia { get; set; }
    }
}
