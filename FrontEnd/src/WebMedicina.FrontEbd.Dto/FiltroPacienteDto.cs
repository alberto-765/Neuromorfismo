using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.FrontEnd.Dto {
    public class FiltroPacienteDto {

        public string Sexo { get; set; } = String.Empty;

        public decimal Talla { get; set; }

        public IEnumerable<string> Farmacos { get; set; } = new HashSet<string>();

        public IEnumerable<int> TipoEpilepsias { get; set; } = new HashSet<int>();

        public IEnumerable<int> TipoMutacion { get; set; } = new HashSet<int>();

        public string EnfermRaras { get; set; } = "No";

        public string MedicoUltMod { get; set; } = string.Empty;

        public string MedicoCreador { get; set; } = string.Empty;
    }
}
