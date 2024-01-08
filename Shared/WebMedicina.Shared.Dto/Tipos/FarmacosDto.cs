using System.ComponentModel;

namespace WebMedicina.Shared.Dto.Tipos {
    public class FarmacosDto : BaseTipoDto, ICloneable, IEquatable<FarmacosDto> {
        [ReadOnly(true)]
        public int IdFarmaco { get; set; }

        public object Clone() {
            return this.MemberwiseClone();
        }

   
        // Comparar clases        
        public bool Equals(FarmacosDto? other) {
            return other != null && (ReferenceEquals(other, this) || (other.Nombre == this.Nombre && other.FechaUltMod == this.FechaUltMod && other.FechaCreac == this.FechaCreac && other.IdFarmaco == this.IdFarmaco));
        }
        //public override bool Equals(object? obj) {
        //    return Equals(obj as EpilepsiasDto);
        //}
    }
}
