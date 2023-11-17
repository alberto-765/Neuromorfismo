using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.Shared.Dto {
    public class MedicosPacientesDto : ICloneable, IEquatable<MedicosPacientesDto> {

        public int Paciente {  get; set; }
        public string Medico { get; set; }


        // Cloneable
        public MedicosPacientesDto() { }
        public MedicosPacientesDto(MedicosPacientesDto e) {
            this.Paciente = e.Paciente;
            this.Medico = e.Medico;
        }

        public object Clone() {
            return new MedicosPacientesDto(this);
        }

        // Comparar clases        
        public bool Equals(MedicosPacientesDto? other) {
            return other != null && (ReferenceEquals(other, this) || (other.Paciente == this.Paciente && other.Medico == this.Medico));
        }
    }
}
