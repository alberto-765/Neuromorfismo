using System.ComponentModel;

namespace Neuromorfismo.Shared.Dto.Tipos {

    public class MutacionesDto : BaseTipoDto, ICloneable, IEquatable<MutacionesDto>{
        public int IdMutacion { get; init; }

        public object Clone() {
            return this.MemberwiseClone();
        }

        // Comparar clases        
        public bool Equals(MutacionesDto? other) {
            return other != null && (ReferenceEquals(other, this) || (other.Nombre == this.Nombre && other.FechaUltMod == this.FechaUltMod && other.FechaCreac == this.FechaCreac && other.IdMutacion == this.IdMutacion));
        }

        public override int GetHashCode() {
            return IdMutacion.GetHashCode() ^ Nombre.GetHashCode() ^ FechaCreac.GetHashCode() ^ FechaUltMod.GetHashCode();
        }
        public override bool Equals(object? obj) {
            return Equals(obj as MutacionesDto);
        }
    }
}
