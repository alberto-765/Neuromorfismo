using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.Shared.Dto {
    public class FarmacosDto :ICloneable, IEquatable<FarmacosDto> {
        public int Indice { get; set; }
        public int IdFarmaco { get; set; }

        [Required(ErrorMessage = "El nombre es un campo obligatorio")]
        [MaxLength(50, ErrorMessage = "La longitud máxima son 50 caracteres")]
        [RegularExpression(@"^[^!@#$%^&*(),.?"":{}|<>]*$", ErrorMessage = "El nombre no puede contener caracteres espciales")]
        public string Nombre { get; set; } = string.Empty;

        public DateOnly FechaCreac { get; set; }

        public DateOnly FechaUltMod { get; set; }

        public FarmacosDto() { }
        public FarmacosDto(FarmacosDto e) {
            this.FechaUltMod = e.FechaUltMod;
            this.FechaCreac = e.FechaCreac;
            this.IdFarmaco = e.IdFarmaco;
            this.Nombre = e.Nombre;
        }

        public object Clone() {
            return new FarmacosDto(this);
        }

   
        // Comparar clases        
        public bool Equals(FarmacosDto? other) {
            return other != null && (ReferenceEquals(other, this) || (other.Nombre == this.Nombre && other.FechaUltMod == this.FechaUltMod && other.FechaCreac == this.FechaCreac && other.IdFarmaco == this.IdFarmaco));
        }
    }
}
