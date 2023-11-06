using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.Shared.Dto {
    public class MutacionesDto : ICloneable, IEquatable<MutacionesDto>{
        public int IdMutacion { get; set; }

        [Required(ErrorMessage = "El nombre es un campo obligatorio")]
        [MaxLength(50, ErrorMessage = "La longitud máxima son 50 caracteres")]
        [RegularExpression(@"^[^!@#$%^&*(),.?"":{}|<>]$", ErrorMessage = "El nombre no puede contener caracteres espciales")]
        public string Nombre { get; set; } = string.Empty;

        public DateOnly FechaCreac { get; set; }

        public DateOnly FechaUltMod { get; set; }



        public MutacionesDto() { }
        public MutacionesDto(MutacionesDto e) {
            this.FechaUltMod = e.FechaUltMod;
            this.FechaCreac = e.FechaCreac;
            this.IdMutacion = e.IdMutacion;
            this.Nombre = e.Nombre;
        }

        public object Clone() {
            return new MutacionesDto(this);
        }

        // Comparar clases        
        public bool Equals(MutacionesDto? other) {
            return other != null && (ReferenceEquals(other, this) || (other.Nombre == this.Nombre && other.FechaUltMod == this.FechaUltMod && other.FechaCreac == this.FechaCreac && other.IdMutacion == this.IdMutacion));
        }
    }
}
