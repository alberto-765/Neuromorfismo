using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.FrontEnd.Dto {
    public class FiltroPacienteDto {

        public string Sexo { get; set; } = String.Empty;

        public decimal Talla { get; set; } = 50;

        public IEnumerable<string> Farmacos { get; set; } = new HashSet<string>();

        public IEnumerable<string> TipoEpilepsias { get; set; } = new HashSet<string>();

        public IEnumerable<string> TipoMutacion { get; set; } = new HashSet<string>();

        public bool EnfermRaras { get; set; }

        public string Medico { get; set; } = string.Empty; // Filtrado por los pacientes de un medico
    }
}
